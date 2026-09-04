using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using Verse;

namespace HitAndRun
{
    [HarmonyPatch(typeof(Pawn_RotationTracker), "UpdateRotation")]
    static class Pawn_RotationTracker_UpdateRotation_Patch
    {
        static readonly Dictionary<Pawn, Stance> swapped = new Dictionary<Pawn, Stance>();
        static readonly FieldInfo pawnField = AccessTools.Field(typeof(Pawn_RotationTracker), "pawn");
        static void Prefix(Pawn_RotationTracker __instance)
        {
            //Rotate the pawn's facing direction when moving only
            Pawn pawn = (Pawn)pawnField.GetValue(__instance);
            if(pawn.stances.curStance is Stance_Cooldown cooldown && cooldown.verb != null && pawn.pather.Moving)
            {
                swapped[pawn] = pawn.stances.curStance;
                pawn.stances.curStance = new Stance_Mobile();
            }
        }
        static void Postfix(Pawn_RotationTracker __instance)
        {
            Pawn pawn = (Pawn)pawnField.GetValue(__instance);
            if(swapped.TryGetValue(pawn, out Stance real))
            {
                pawn.stances.curStance = real;
                swapped.Remove(pawn);
            }
        }
    }
}