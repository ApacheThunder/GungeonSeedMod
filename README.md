Dungeon Seed Mod - v 1.0 by Apache Thunder.

Features:

* Ability to set custom seeds for seeded runs! Note that certain player actions/choices on a run can impact certain elements of the run.
But theoretically, if you made the same choices as the player who made the seed, you'd get the same results.

Command syntax:

To set a custom seed use "dungeonseed setseed SEED1 SEED2". SEED1 is the main Gungeon seed and expects a value between 1 and 1000000000.
SEED2 is the RandomIntForCurrentRun seed. This one expects a value between 0 and 1000. Note that setting SEED1 to zero will disable seeded runs.

For best results start a custom seed or new random seed from the Breach with a character already selected.

To start a new seed, use "dungeonseed newseed" to generate a random seed to run on. Write down the seed you see in the console if you want to share it!

To check the current seed you are using use "dungeonseed checkseed".
To reset/disable seeded runs, use "dungeonseed reset".

Compiling and versions of Enter the Gungeon required:

This source code uses C#. Visual Studio 2015 or newer recommended.
This mod is intended for post AG&D versions of Enter the Gungeon. Please ensure your game is up to date before attempting to use this mod.
