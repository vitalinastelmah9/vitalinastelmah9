using System;

// Plays the SHIPPING rule files - GalleryRng, RelicCatalog, HallCatalog,
// ReelStripGenerator, SpinResolver, compiled straight out of Assets/Scripts/Generated -
// so the design document's C.5 and C.11 claims are measured rather than asserted.
public static class Sim
{
    private const int Sessions = 20000;

    public static int Main()
    {
        int fallbacks = 0;
        int unreachable = 0;
        var relicSetCounts = new System.Collections.Generic.Dictionary<string, int>();
        var stripCounts = new System.Collections.Generic.Dictionary<string, int>();
        int sameAsPrevious = 0;
        string previousSignature = null;

        // How long a hall takes to complete when it is actually played, and what the
        // worst case looks like: the run must never be able to end on its own.
        long totalSpinsToComplete = 0;
        int worstSpinsToComplete = 0;
        int completed = 0;
        long totalGrants = 0;
        int worstSpinsBeforeFirstGrant = int.MaxValue;
        int degenerateReelSets = 0;

        for (int s = 0; s < Sessions; s++)
        {
            int hall = s % HallCatalog.Count;
            int attempt = (s / HallCatalog.Count) + 1;
            int seed = (hall * 7919) ^ (attempt * 104729);
            var rng = new GalleryRng(seed);
            var session = new ReelStripGenerator();
            session.Build(rng);

            if (session.UsedFallback)
            {
                fallbacks++;
            }
            if (!session.IsReachable())
            {
                unreachable++;
            }

            string relicSig = session.Relic(0) + "/" + session.Relic(1) + "/" + session.Relic(2);
            relicSetCounts.TryGetValue(relicSig, out int rc);
            relicSetCounts[relicSig] = rc + 1;

            string stripSig = StripSignature(session);
            stripCounts.TryGetValue(stripSig, out int sc);
            stripCounts[stripSig] = sc + 1;

            string signature = relicSig + "|" + stripSig + "|" + session.DecorSlot(0) + session.DecorSlot(1) + session.DecorSlot(2);
            if (signature == previousSignature)
            {
                sameAsPrevious++;
            }
            previousSignature = signature;

            // Every relic of the set must physically exist on at least one reel,
            // otherwise the hall could never be completed however long you played.
            if (!AllRelicsPresent(session))
            {
                degenerateReelSets++;
            }

            // Play the hall out with the shipping resolver and the shipping vault rules.
            var play = PlayOut(session, rng.Clone());
            if (play.Completed)
            {
                completed++;
                totalSpinsToComplete += play.Spins;
                if (play.Spins > worstSpinsToComplete)
                {
                    worstSpinsToComplete = play.Spins;
                }
            }
            totalGrants += play.Grants;
            if (play.SpinsBeforeFirstGrant < worstSpinsBeforeFirstGrant)
            {
                worstSpinsBeforeFirstGrant = play.SpinsBeforeFirstGrant;
            }
        }

        Console.WriteLine("sessions                  " + Sessions);
        Console.WriteLine("fallback layouts used     " + fallbacks);
        Console.WriteLine("unreachable after build   " + unreachable);
        Console.WriteLine("reel sets missing a relic " + degenerateReelSets);
        Console.WriteLine("distinct relic sets       " + relicSetCounts.Count + " ordered (10 combinations of 5 collectibles)");
        Console.WriteLine("distinct strip sets       " + stripCounts.Count);
        Console.WriteLine("identical to previous     " + sameAsPrevious);
        Console.WriteLine("halls completed           " + completed + " / " + Sessions);
        Console.WriteLine("mean spins to complete    " + (completed == 0 ? 0 : totalSpinsToComplete / (double)completed).ToString("0.00"));
        Console.WriteLine("worst spins to complete   " + worstSpinsToComplete);
        Console.WriteLine("grants handed out (total) " + totalGrants);
        Console.WriteLine("fewest spins before a grant " + (worstSpinsBeforeFirstGrant == int.MaxValue ? -1 : worstSpinsBeforeFirstGrant)
            + "  (-1 = no session ever needed one)");

        // The clock a passive run survives: nothing in this game can end a run, so the
        // only number worth reporting is how long the opening pouch lasts with the
        // WORST case - the top bet, every spin a loss, no refill counted.
        double cycle = GalleryEconomy.SpinCycleSeconds;
        Console.WriteLine("spin cycle (s)            " + cycle.ToString("0.00"));
        Console.WriteLine("worst passive run (s)     " + (GalleryEconomy.StartingBalance / 100 * cycle).ToString("0.0") + "  (top bet, zero wins, before the first free grant)");
        Console.WriteLine("default-bet run (s)       " + (GalleryEconomy.StartingBalance / 50 * cycle).ToString("0.0"));

        bool ok = fallbacks == 0 && unreachable == 0 && degenerateReelSets == 0
            && sameAsPrevious == 0 && completed == Sessions;
        Console.WriteLine(ok ? "VERDICT ok" : "VERDICT PROBLEM");
        return ok ? 0 : 1;
    }

    private static string StripSignature(ReelStripGenerator session)
    {
        var sb = new System.Text.StringBuilder();
        for (int r = 0; r < ReelStripGenerator.Reels; r++)
        {
            for (int i = 0; i < session.StripLength; i++)
            {
                sb.Append(session.Stop(r, 0, i));
            }
            sb.Append('-');
        }
        return sb.ToString();
    }

    private static bool AllRelicsPresent(ReelStripGenerator session)
    {
        for (int i = 0; i < HallCatalog.RelicsPerHall; i++)
        {
            int relic = session.Relic(i);
            bool seen = false;
            for (int r = 0; r < ReelStripGenerator.Reels && !seen; r++)
            {
                for (int stop = 0; stop < session.StripLength; stop++)
                {
                    if (session.Stop(r, 0, stop) == relic)
                    {
                        seen = true;
                        break;
                    }
                }
            }
            if (!seen)
            {
                return false;
            }
        }
        return true;
    }

    private struct Outcome
    {
        public bool Completed;
        public int Spins;
        public int Grants;
        public int SpinsBeforeFirstGrant;
    }

    // Mirrors HallSessionDirector: take the stake, roll three random stops, resolve
    // with the shipping SpinResolver, grant a free refill whenever the pouch cannot
    // cover the cheapest bet. A hard spin cap only exists so a bug cannot hang the run.
    private static Outcome PlayOut(ReelStripGenerator session, GalleryRng rng)
    {
        var resolver = new SpinResolver();
        var found = new bool[HallCatalog.RelicsPerHall];
        var window = new int[9];
        int balance = GalleryEconomy.StartingBalance;
        int bet = GalleryEconomy.BetAt(GalleryEconomy.DefaultBetStep);
        int spins = 0;
        int grants = 0;
        int firstGrantAt = int.MaxValue;

        while (spins < 5000)
        {
            if (balance < GalleryEconomy.MinimumBet)
            {
                balance += GalleryEconomy.DepletionGrant;
                grants++;
                if (firstGrantAt == int.MaxValue)
                {
                    firstGrantAt = spins;
                }
            }
            int stake = balance >= bet ? bet : GalleryEconomy.MinimumBet;
            balance -= stake;
            spins++;

            int c0 = rng.Next(session.StripLength);
            int c1 = rng.Next(session.StripLength);
            int c2 = rng.Next(session.StripLength);
            for (int row = 0; row < ReelStripGenerator.Rows; row++)
            {
                window[(row * 3) + 0] = session.Stop(0, row, c0);
                window[(row * 3) + 1] = session.Stop(1, row, c1);
                window[(row * 3) + 2] = session.Stop(2, row, c2);
            }
            resolver.Resolve(window, stake, session, found);
            balance += resolver.Payout;
            if (resolver.ShardIndex >= 0)
            {
                found[resolver.ShardIndex] = true;
            }

            bool all = true;
            for (int i = 0; i < found.Length; i++)
            {
                if (!found[i])
                {
                    all = false;
                }
            }
            if (all)
            {
                return new Outcome
                {
                    Completed = true,
                    Spins = spins,
                    Grants = grants,
                    SpinsBeforeFirstGrant = firstGrantAt,
                };
            }
        }
        return new Outcome { Completed = false, Spins = spins, Grants = grants, SpinsBeforeFirstGrant = firstGrantAt };
    }
}
