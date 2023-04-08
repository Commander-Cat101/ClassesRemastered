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

[assembly: MelonInfo(typeof(ClassesRemastered.ClassesRemasteredMain), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
[assembly: MelonPriority(99)]

namespace ClassesRemastered;

public class ClassesRemasteredMain : BloonsTD6Mod
{
    public static ClassBase[] addonclasses = { };
    public static ClassBase[] classes = {};
    public static ClassBase? activeclass = new None();
    public override void OnApplicationStart()
    {
        ModHelper.Msg<ClassesRemasteredMain>("ClassesRemastered loaded!");
        foreach (MelonMod mod in RegisteredMelons)
        {
            foreach (Type type in mod.MelonAssembly.Assembly.GetTypes())
            {
                if (type.BaseType == typeof(ClassBase))
                {
                    ClassBase Class = (ClassBase)Activator.CreateInstance(type);
                    string Name = mod.MelonAssembly.Assembly.ToString();
                    string[] SplitName = Name.Split(",", 2);
                    MelonLogger.Msg("Loaded Class: " + Class.Name + " from " + SplitName[0]);
                    if (SplitName[0] == "ClassesRemastered")
                    {
                        //classes = classes.Append(Class).ToArray();
                        classes = classes.Append(Class).ToArray();
                    }
                    else
                    {
                        addonclasses = classes.Append(Class).ToArray();
                    }
                }
            }
        }
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