using System.Collections.Generic;
using HarmonyLib;
using Verse;

namespace HitAndRun

{
    [HarmonyPatch(typeof(Stance_Busy), "StanceTick")]
    public static class Stance_Busy_StanceTick_Patch
    {
        // Pauses the cooldown timer while moving and resume after a number amount of time has passed
        const int MaxPauseTicks = 180;
        static readonly Dictionary<Stance_Busy, int> pausedTicksSoFar = new Dictionary<Stance_Busy, int>();
        static bool Prefix(Stance_Busy __instance)
        {
            if(__instance is Stance_Cooldown cooldown && cooldown.verb != null)
            {
                Pawn pawn = __instance.stanceTracker.pawn;

                if(pawn.pather != null && pawn.pather.Moving)
                {
                     //Increments count (Checking if cooldown state is beginning to count down.)
                    pausedTicksSoFar.TryGetValue(__instance, out int count);
                    count++;
                    pausedTicksSoFar[__instance] = count;

                    if(count >= MaxPauseTicks)
                    {
                        Log.Message("[HitAndRun] Budget Exhausted! Continuing tick countdown.");
                        return true; //Budget exhausted, cooldown is ticking normally now.
                    }
                    return false;
                }
            }
            return true;
        }
    }
}