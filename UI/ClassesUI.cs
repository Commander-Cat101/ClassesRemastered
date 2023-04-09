using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Components;
using BTD_Mod_Helper.Extensions;
using UnityEngine;
using BTD_Mod_Helper.Api.Enums;
using TaskScheduler = BTD_Mod_Helper.Api.TaskScheduler;
using Il2CppAssets.Scripts.Unity.UI_New.ChallengeEditor;
using Il2CppAssets.Scripts.Utils;
using Il2CppAssets.Scripts.Unity.UI_New;
using Il2CppAssets.Scripts.Unity.Menu;
using Il2CppTMPro;
using ClassesRemastered;
using Il2CppNinjaKiwi.Common;
using Il2CppAssets.Scripts.Unity.UI_New.Quests;
using Il2Cpp;

namespace ClassesMenuUI;
public class ClassesUI : ModGameMenu<ExtraSettingsScreen>
{
    ModHelperPanel MainPanel;
    ModHelperScrollPanel ScrollPanel;
    ModHelperPanel Panel;

    ModHelperText TitleText;
    ModHelperText DescriptionText;
    ModHelperScrollPanel EffectsPanel;
    public override bool OnMenuOpened(Il2CppSystem.Object data)
    {
        CommonForegroundScreen.instance.heading.SetActive(true);
        CommonForegroundHeader.SetText("Classes");
        var panelTransform = GameMenu.gameObject.GetComponentInChildrenByName<RectTransform>("Panel");
        var panel = panelTransform.gameObject;
        panel.DestroyAllChildren();
        MainPanel = panel.AddModHelperPanel(new Info("ClassesMenu", 3600, 1900));
        CreateLeftMenu(MainPanel);
        CreateRightMenu(MainPanel);
        return false;
    }
    private void CreateLeftMenu(ModHelperPanel ClassesMenu)
    {
        ScrollPanel = ClassesMenu.AddScrollPanel(new Info("LeftScrollMenu", -900, 0, 1600, 1900), RectTransform.Axis.Vertical, VanillaSprites.MainBGPanelBlue, 50, 50);
        GetScrollContent();
    }
    private void CreateRightMenu(ModHelperPanel ClassesMenu)
    {
        Panel = ClassesMenu.AddPanel(new Info("RightMenu", 900, 0, 1600, 1900), VanillaSprites.MainBGPanelBlue);
        var TitlePanel = Panel.AddPanel(new Info("ClassName", 0, 775, 1450, 200), VanillaSprites.BlueInsertPanelRound);
        TitleText = TitlePanel.AddText(new Info("ClassNameText", 0, 0, 1450, 200), ClassesRemasteredMain.activeclass.Name, 150);

        var DescriptionPanel = Panel.AddPanel(new Info("ClassDescription", 0, 300, 1450, 700), VanillaSprites.BlueInsertPanelRound);
        DescriptionText = DescriptionPanel.AddText(new Info("ClassDescriptionText", 0, 0, 1350, 700), ClassesRemasteredMain.activeclass.Description, 80);

        EffectsPanel = Panel.AddScrollPanel(new Info("ClassEffect", 0, -500, 1450, 775), RectTransform.Axis.Vertical,VanillaSprites.BlueInsertPanelRound, 100, 100);
        GenerateEffects(ClassesRemasteredMain.activeclass.EffectsHeight);
    }
    void GenerateEffects(int Height = 800)
    {
        
        string Effects = "- Pros \n" + ClassesRemasteredMain.activeclass.Pros + "\n\n- Cons \n" + ClassesRemasteredMain.activeclass.Cons;
        if (EffectsPanel != null)
        {
            EffectsPanel.AddScrollContent(ModHelperText.Create(new Info("ClassEffectsText", 0, 0, 1400, Height), Effects, 80, TextAlignmentOptions.TopLeft));
        }
    }
    void GetScrollContent()
    {
        ScrollPanel.ScrollContent.transform.DestroyAllChildren();
        foreach (var Class in ClassesRemasteredMain.classes)
        {
            try
            {
                if (Class.ShowInSelector == true)
                {
                    ScrollPanel.AddScrollContent(CreateClass(Class));
                }
            }
            catch { }
        }
        foreach (var Class in ClassesRemasteredMain.addonclasses)
        {
            try
            {
                if (Class.ShowInSelector == true)
                {
                    ScrollPanel.AddScrollContent(CreateClass(Class));
                }
            }
            catch { }
        }
    }
    void ReloadRightPanel()
    {
        EffectsPanel.ScrollContent.transform.DestroyAllChildren();
        TitleText.SetText(ClassesRemasteredMain.activeclass.Name);
        DescriptionText.SetText(ClassesRemasteredMain.activeclass.Description);
        GenerateEffects(ClassesRemasteredMain.activeclass.EffectsHeight);
    }
    public ModHelperPanel CreateClass(ClassBase Class)
    {
        var panel = ModHelperPanel.Create(new Info("ClassContent" + Class.Name, 0, 0, 1500, 700), VanillaSprites.MainBGPanelGrey);
        panel.AddText(new Info("ClassName", -300, 250, 800, 100), Class.Name, 80, TextAlignmentOptions.TopLeft);
        var desc = panel.AddText(new Info("ClassDiscription", -350, -100, 700, 450), Class.Description, 60, TextAlignmentOptions.TopLeft);
        desc.transform.GetComponent<NK_TextMeshProUGUI>().enableAutoSizing = true;

        var button = panel.AddButton(new Info("ClassImage", 500, -100, 400, 400), VanillaSprites.WoodenRoundButton, new System.Action(() =>
        {
            SetClass(Class);
            GetScrollContent();
            ReloadRightPanel();
            ClassesButton.SetIcon();
        }));
        button.Image.SetSprite(Class.Icon);
        if (ClassesRemasteredMain.activeclass.Name == Class.Name)
        {
            button.AddImage(new Info("SelectedTick", 150, 150, 150, 150), VanillaSprites.SelectedTick);
        }

        return panel;
    }
    private void SetClass(ClassBase Class)
    {
        ClassesRemasteredMain.activeclass = Class;
    }
}
