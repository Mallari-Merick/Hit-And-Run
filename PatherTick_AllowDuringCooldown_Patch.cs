using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using Verse.AI;
using Verse;

namespace HitAndRun
{
    [HarmonyPatch(typeof(Pawn_PathFollower), "PatherTick")]
    public static class PatherTick_AllowDuringCooldown_Patch
    {
        // Let's the pawn's position to actually update
        static readonly Dictionary<Pawn, Stance> swapped = new Dictionary<Pawn, Stance>();
        static readonly FieldInfo pawnField = AccessTools.Field(typeof(Pawn_PathFollower), "pawn");
        
        static void Prefix(Pawn_PathFollower __instance)
        {
            Pawn pawn = (Pawn)pawnField.GetValue(__instance);
            
            if(pawn.stances.curStance is Stance_Cooldown cooldown && __instance.Moving && cooldown.verb != null)
            {
                swapped[pawn] = pawn.stances.curStance;
                pawn.stances.curStance = new Stance_Mobile();
                Log.Message($"[HitAndRun] PatherTick check, stance={pawn.stances.curStance}, moving={__instance.Moving}");
            }
        }

        static void Postfix(Pawn_PathFollower __instance)
        {
            Pawn pawn = (Pawn)pawnField.GetValue(__instance);

            if (swapped.TryGetValue(pawn, out Stance real))
            {
                pawn.stances.curStance = real;
                swapped.Remove(pawn);
            }
        }
    }
}