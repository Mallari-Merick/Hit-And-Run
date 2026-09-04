using System.Diagnostics;
using HarmonyLib;
using Verse;
using Verse.AI;

[HarmonyPatch(typeof(Pawn_PathFollower), "StopDead")]
public static class Debug_StopDead_Patch
{
    static void Prefix()
    {
        Log.Message ("[HitAndRun] StopDead called! Stack trace:\n" + new StackTrace().ToString());
    }
}