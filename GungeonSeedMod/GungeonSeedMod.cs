using Dungeonator;
using UnityEngine;

namespace GungeonSeedMod {

    public class GungeonSeedMod : ETGModule {

        public override void Init() { }

        public override void Start() {
            ETGModConsole.Commands.AddGroup("gungeonseed", delegate (string[] e) {
                ETGModConsole.Log("[Gungeon Seed]  The following options are available for setting Gungeon Seeds:");
                ETGModConsole.Log("checkseed\nsetseed\nnewseed\nreset\n\nTo set a custom seed, use 'setseed SEED1 SEED2' where SEED1 is the main Gungeon seed between 1 and 1000000000 and\n SEED2 is the RandomIntForRun seed. Set Seed2 to a value of 0 to 1000.\nNote that setting SEED1 to zero will disable seeded runs.\nTo ensure consistant results, set your seed after selecting a character at the breach but before leaving the breach.\nSetting a seed while in the middle of a run or via quick restart is not recommended.\nTo start a new seed, simply use 'gungeonseed newseed' and write down the displayed seed if you want to share it!.\nUse 'gungeonseed checkseed' to check your current seed.\n\nTo turn off custom seed mode, use 'gungeonseed reset' or set SEED1 argument of setseed to zero.");
            });
            ETGModConsole.Commands.GetGroup("gungeonseed").AddUnit("checkseed", delegate (string[] e) { InitializeSeed(CheckSeed: true); });
            ETGModConsole.Commands.GetGroup("gungeonseed").AddUnit("setseed", SetSeed);
            ETGModConsole.Commands.GetGroup("gungeonseed").AddUnit("newseed", delegate (string[] e) {
                InitializeSeed(BraveRandom.GenerationRandomRange(1, 1000000000), Random.Range(0, 1000), false);
            });
            ETGModConsole.Commands.GetGroup("gungeonseed").AddUnit("reset", delegate (string[] e) {
                InitializeSeed(RandomIntForCurrentRun: Random.Range(0, 1000));
                ETGModConsole.Log("Custom seeds has been disabled. Please start a new run to ensure changes take effect.");
            });

        }

        private static bool SeedArgCount(string[] args, int min, int max) {
            if (args.Length >= min && args.Length <= max) return true;
            if (min == max) {
                ETGModConsole.Log("Error: need exactly " + min + " argument(s)");
            } else {
                ETGModConsole.Log("Error: need between " + min + " and " + max + " argument(s)");
            }
            return false;
        }

        private void SetSeed(string[] args) {
            Dungeon dungeon = GameManager.Instance.Dungeon;

            if (!SeedArgCount(args, 2, 2)) return;
            if (int.Parse(args[0]) > 1000000000) {
                ETGModConsole.Log("Error: Requested seed exceeds normal maximum value of 1000000000");
                return;
            }

            if (int.Parse(args[1]) > 1000) {
                ETGModConsole.Log("Error: Requested RandomIntForRun seed exceeds normal maximum value of 1000");
                return;
            }

            if (int.Parse(args[0]) == 0) { ETGModConsole.Log("Warning: Setting a seed of 0 will disable seeded runs!"); }

            if (dungeon.LevelOverrideType != GameManager.LevelOverrideState.FOYER) {
                ETGModConsole.Log("Warning: Setting a seed while not at the Breach can result in inconsistant results!");
            }

            InitializeSeed(int.Parse(args[0]), int.Parse(args[1]), false);
        }

        private void InitializeSeed(int DungeonSeed = 0, int RandomIntForCurrentRun = 0, bool CheckSeed = false) {
            if (CheckSeed) {
                ETGModConsole.Log("IsSeeded Status: " + GameManager.Instance.IsSeeded.ToString());
                // ETGModConsole.Log("Current Dungeon Seed: " + GameManager.Instance.Dungeon.DungeonSeed);
                ETGModConsole.Log("CurrentRunSeed: " + GameManager.Instance.CurrentRunSeed);
                ETGModConsole.Log("RandomIntForRun: " + GameManager.Instance.RandomIntForCurrentRun);
                ETGModConsole.Log("GetDungeonSeed Test: " + GameManager.Instance.Dungeon.GetDungeonSeed());
                ETGModConsole.Log("If 'GetDungeonSeed Test' returns a random value on multiple checks, seeded runs is not active.");
                return;
            }
            ETGModConsole.Log("Old CurrentRunSeed: " + GameManager.Instance.CurrentRunSeed);
            ETGModConsole.Log("Old RandomIntForCurrentRun: " + GameManager.Instance.RandomIntForCurrentRun);
            // ETGModConsole.Log("Old DungeonSeed: " + GameManager.Instance.Dungeon.DungeonSeed);
            GameManager.Instance.Dungeon.DungeonSeed = DungeonSeed;
            GameManager.Instance.CurrentRunSeed = DungeonSeed;
            GameManager.Instance.RandomIntForCurrentRun = RandomIntForCurrentRun;
            GameManager.Instance.Dungeon.GetDungeonSeed();
            ETGModConsole.Log("New CurrentRunSeed: " + GameManager.Instance.CurrentRunSeed, false);
            ETGModConsole.Log("New RandomIntForCurrentRun: " + GameManager.Instance.RandomIntForCurrentRun);
            // ETGModConsole.Log("New Dungeon Seed: " + GameManager.Instance.Dungeon.DungeonSeed);
            return;
        }

        public override void Exit() { }
    }
}

