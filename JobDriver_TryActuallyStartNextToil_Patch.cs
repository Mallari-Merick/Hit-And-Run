using System.Collections.Generic;
using HarmonyLib;
using Verse;
using Verse.AI;

namespace HitAndRun
{
    [HarmonyPatch(typeof(JobDriver),"TryActuallyStartNextToil")]
    public static class JobDriver_TryActuallyStartNextToil_Patch
    {
        static readonly Dictionary<Pawn,Stance> swapped = new Dictionary<Pawn, Stance>();

        static void Prefix(JobDriver __instance)
        {
            //Permit pawn to move
            Pawn pawn = __instance.pawn;

            if(pawn.stances.curStance is Stance_Cooldown cooldown && cooldown.verb != null)
            {
                swapped[pawn] = pawn.stances.curStance;
                pawn.stances.curStance = new Stance_Mobile();
            }
        }

        static void Postfix(JobDriver __instance)
        {
            Pawn pawn = __instance.pawn;
            if(swapped.TryGetValue(pawn, out Stance real))
            {
                pawn.stances.curStance = real;
                swapped.Remove(pawn);
            }
        }
    }
    
}