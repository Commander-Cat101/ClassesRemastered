using MelonLoader;
using BTD_Mod_Helper;
using ClassesRemastered;
using System;
using HarmonyLib;
using System.Linq;
using Harmony;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Simulation.Objects;
using Il2CppAssets.Scripts.Models;
using BTD_Mod_Helper.Api;

[assembly: MelonInfo(typeof(ClassesRemastered.ClassesRemasteredMain), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
[assembly: MelonPriority(99)]

namespace ClassesRemastered;

public class ClassesRemasteredMain : BloonsTD6Mod
{
    public static BaseClass? activeclass = new None();
    public override void OnApplicationStart()
    {
        ModHelper.Msg<ClassesRemasteredMain>("ClassesRemastered loaded!");
    }
    public override void OnTowerUpgraded(Tower tower, string upgradeName, TowerModel newBaseTowerModel)
    {
        activeclass?.EditTower(tower);
    }
    public override void OnTowerCreated(Tower tower, Entity target, Model modelToUse)
    {
        base.OnTowerCreated(tower, target, modelToUse);
        activeclass?.EditTower(tower);
    }
}