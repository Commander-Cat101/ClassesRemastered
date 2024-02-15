using BTD_Mod_Helper.Extensions;
using ClassesRemastered;
using HarmonyLib;
using Il2CppAssets.Scripts.Simulation;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles;
using Il2CppAssets.Scripts.Simulation.Tracking;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[HarmonyPatch(typeof(AnalyticsTrackerSimManager), nameof(AnalyticsTrackerSimManager.OnCashEarned))]
public static class NoCash
{
    [HarmonyPrefix]
    public static bool Prefix(double cash, Simulation.CashSource source)
    {
        if (source != Simulation.CashSource.CoopTransferedCash && source != Simulation.CashSource.TowerSold)
        {
            float multiplier = ClassesRemasteredMain.SelectedClass.CashMultiplier;
            multiplier -= 1f;

            cash *= multiplier;
            InGame.instance.AddCash(cash);
        }
        return true;
    }
}