using MelonLoader;
using BTD_Mod_Helper;
using ClassesRemastered;
using HarmonyLib;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Simulation.Objects;
using Il2CppAssets.Scripts.Models;

[assembly: MelonInfo(typeof(ClassesRemastered.ClassesRemasteredMain), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace ClassesRemastered;

[HarmonyPatch]
public partial class ClassesRemasteredMain : BloonsTD6Mod
{
    public static BaseClass? Activeclass { get; set; } = new None();

    public override void OnApplicationStart()
    {
        ModHelper.Msg<ClassesRemasteredMain>("ClassesRemastered loaded!");
    }
    public override void OnTowerUpgraded(Tower tower, string upgradeName, TowerModel newBaseTowerModel)
    {
        Activeclass?.EditTower(tower);
    }
    public override void OnTowerCreated(Tower tower, Entity target, Model modelToUse)
    {
        base.OnTowerCreated(tower, target, modelToUse);
        Activeclass?.EditTower(tower);
    }
}