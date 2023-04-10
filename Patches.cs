using BTD_Mod_Helper.Extensions;
using HarmonyLib;
using Il2CppAssets.Scripts.Simulation;
using Il2CppAssets.Scripts.Simulation.Tracking;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;

namespace ClassesRemastered;

public partial class ClassesRemasteredMain
{
    [HarmonyPatch(typeof(AnalyticsTrackerSimManager), nameof(AnalyticsTrackerSimManager.OnCashEarned))]
    [HarmonyPrefix]
    public static bool AnalyticsTrackerSimManager_OnCashEarned_Prefix(ref double cash, ref Simulation.CashSource source)
    {
        if (source is Simulation.CashSource.CoopTransferedCash or Simulation.CashSource.TowerSold)
            return true;

        if (Activeclass != null)
        {
            var multiplier = Activeclass.CashMultiplier - 1f;
            
            cash *= multiplier;
        }

        InGame.instance.AddCash(cash);
        return true;
    }
}