using HarmonyLib;
using Verse;

namespace HitAndRun
{
    [StaticConstructorOnStartup]
    public static class HitAndRunMod
    {
        static HitAndRunMod()
        {
            var harmony = new Harmony("Mercs.hitandrun");
            harmony.PatchAll();
        }
    }
}