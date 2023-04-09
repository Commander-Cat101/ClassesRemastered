using BTD_Mod_Helper.Extensions;
using ClassesRemastered;
using HarmonyLib;
using Il2CppAssets.Scripts.Simulation;
using Il2CppAssets.Scripts.Simulation.Tracking;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesRemastered
{
    
}
[HarmonyPatch(typeof(AnalyticsTrackerSimManager), nameof(AnalyticsTrackerSimManager.OnCashEarned))]
public class NoCash
{
    [HarmonyPrefix]
    public static bool Prefix(ref double cash, ref Simulation.CashSource source)
    {
        if (source != Simulation.CashSource.CoopTransferedCash && source != Simulation.CashSource.TowerSold)
        {
            float multiplier = ClassesRemasteredMain.activeclass.CashMultiplier;
            multiplier -= 1f;

            cash *= multiplier;
            InGame.instance.AddCash(cash);
        }
        return true;
    }
}