using ClassesRemastered.UI;
using HarmonyLib;
using Il2Cpp;
using Il2CppAssets.Scripts.Unity.Menu;
using Il2CppAssets.Scripts.Unity.UI_New.Main.DifficultySelect;
using Il2CppAssets.Scripts.Unity.UI_New.Main.MapSelect;

namespace ClassesRemastered;

public partial class ClassesRemasteredMain
{
    [HarmonyPatch(typeof(MenuManager), nameof(MenuManager.OpenMenu))]
    [HarmonyPostfix]
    private static void MenuManager_OpenMenu_Postfix(MenuManager __instance, string menuName)
    {
        if (menuName == "MapSelectScreen")
            ClassesButton.Show();
    }
    
    [HarmonyPatch(typeof(DifficultySelectScreen), nameof(DifficultySelectScreen.Open))]
    [HarmonyPostfix]
    private static void DifficultySelectScreen_Open_Postfix()
    {
        ClassesButton.Hide();
    }
    
    [HarmonyPatch(typeof(DifficultySelectScreen), nameof(DifficultySelectScreen.OpenModeSelectUi))]
    [HarmonyPostfix]
    private static void DifficultySelectScreen_OpenModeSelectUi_Postfix()
    {
        ClassesButton.Hide();
    }
    
    [HarmonyPatch(typeof(ContinueGamePanel), nameof(ContinueGamePanel.ContinueClicked))]
    [HarmonyPostfix]
    private static void ContinueGamePanel_ContinueClicked_Postfix()
    {
        ClassesButton.Hide();
    }
    
    [HarmonyPatch(typeof(MapSelectScreen), nameof(MapSelectScreen.Open))]
    [HarmonyPostfix]
    private static void MapSelectScreen_Open_Postfix()
    {
        ClassesButton.Show();
    }
    
    [HarmonyPatch(typeof(MapSelectScreen), nameof(MapSelectScreen.Close))]
    [HarmonyPostfix]
    private static void MapSelectScreen_Close_Postfix()
    {
        ClassesButton.Hide();
    }
}