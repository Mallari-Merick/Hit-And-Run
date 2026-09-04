using HarmonyLib;
using Verse;
using Verse.AI;

[HarmonyPatch(typeof(Pawn_JobTracker), "StartJob")]
public static class Debug_StartJob_Patch
{
    static void Prefix(Job newJob)
    {
        Log.Message($"[HitAndRun] StartJob called with Job={newJob}");
    }
}