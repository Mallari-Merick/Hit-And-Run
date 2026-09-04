using HarmonyLib;
using Verse;
using Verse.AI;

[HarmonyPatch(typeof(Pawn_PathFollower), "StartPath")]
public static class Debug_StartPath_Patch
{
    static void Prefix()
    {
        Log.Message("[HitAndRun] StartPath was called!");
    }

    static void Postfix(Pawn_PathFollower __instance)
    {
        Log.Message($"[HitAndRun] StartPath finished, Moving={__instance.Moving}");
    }
}