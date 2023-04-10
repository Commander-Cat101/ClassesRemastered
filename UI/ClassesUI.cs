using System;
using System.Linq;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Components;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2Cpp;
using Il2CppAssets.Scripts.Unity.UI_New;
using Il2CppAssets.Scripts.Unity.UI_New.ChallengeEditor;
using Il2CppNinjaKiwi.Common;
using Il2CppTMPro;
using UnityEngine;

namespace ClassesRemastered.UI;
public class ClassesUI : ModGameMenu<ExtraSettingsScreen>
{
    private ModHelperScrollPanel? _scrollPanel;

    ModHelperText _titleText;
    ModHelperText _descriptionText;
    ModHelperScrollPanel _effectsPanel;
    public override bool OnMenuOpened(Il2CppSystem.Object data)
    {
        CommonForegroundScreen.instance.heading.SetActive(true);
        CommonForegroundHeader.SetText("Classes");
        var panelTransform = GameMenu.gameObject.GetComponentInChildrenByName<RectTransform>("Panel");
        var panel = panelTransform.gameObject;
        panel.DestroyAllChildren();
        var MainPanel = panel.AddModHelperPanel(new Info("ClassesMenu", 3600, 1900));
        CreateLeftMenu(MainPanel);
        CreateRightMenu(MainPanel);
        return false;
    }
    private void CreateLeftMenu(ModHelperPanel classesMenu)
    {
        _scrollPanel = classesMenu.AddScrollPanel(new Info("LeftScrollMenu", -900, 0, 1600, 1900), RectTransform.Axis.Vertical, VanillaSprites.MainBGPanelBlue, 50, 50);
        GetScrollContent();
    }
    private void CreateRightMenu(ModHelperPanel classesMenu)
    {
        var Panel = classesMenu.AddPanel(new Info("RightMenu", 900, 0, 1600, 1900), VanillaSprites.MainBGPanelBlue);
        var TitlePanel = Panel.AddPanel(new Info("ClassName", 0, 775, 1450, 200), VanillaSprites.BlueInsertPanelRound);
        _titleText = TitlePanel.AddText(new Info("ClassNameText", 0, 0, 1450, 200), ClassesRemasteredMain.Activeclass.Name, 150);

        var DescriptionPanel = Panel.AddPanel(new Info("ClassDescription", 0, 300, 1450, 700), VanillaSprites.BlueInsertPanelRound);
        _descriptionText = DescriptionPanel.AddText(new Info("ClassDescriptionText", 0, 0, 1350, 700), ClassesRemasteredMain.Activeclass.Description, 80);

        _effectsPanel = Panel.AddScrollPanel(new Info("ClassEffect", 0, -500, 1450, 775), RectTransform.Axis.Vertical,VanillaSprites.BlueInsertPanelRound, 100, 100);
        GenerateEffects(ClassesRemasteredMain.Activeclass.EffectsHeight);
    }
    void GenerateEffects(int height = 800)
    {
        string Effects = "- Pros \n" + ClassesRemasteredMain.Activeclass.Pros + "\n\n- Cons \n" + ClassesRemasteredMain.Activeclass.Cons;
        if (_effectsPanel != null)
        {
            _effectsPanel.AddScrollContent(ModHelperText.Create(new Info("ClassEffectsText", 0, 0, 1400, height), Effects, 80, TextAlignmentOptions.TopLeft));
        }
    }
    void GetScrollContent()
    {
        _scrollPanel.ScrollContent.transform.DestroyAllChildren();
        foreach (var @class in GetContent<BaseClass>().OrderByDescending(@class => @class.mod == mod))
        {
            try
            {
                if (@class.ShowInSelector)
                {
                    _scrollPanel.AddScrollContent(CreateClass(@class));
                }
            }
            catch(Exception e)
            {
                ModHelper.Error<ClassesRemasteredMain>(e);
            }
        }
    }
    void ReloadRightPanel()
    {
        _effectsPanel.ScrollContent.transform.DestroyAllChildren();
        _titleText.SetText(ClassesRemasteredMain.Activeclass.Name);
        _descriptionText.SetText(ClassesRemasteredMain.Activeclass.Description);
        GenerateEffects(ClassesRemasteredMain.Activeclass.EffectsHeight);
    }

    private ModHelperPanel CreateClass(BaseClass @class)
    {
        var panel = ModHelperPanel.Create(new Info("ClassContent" + @class.Name, 0, 0, 1500, 700), VanillaSprites.MainBGPanelGrey);
        panel.AddText(new Info("ClassName", -300, 250, 800, 100), @class.Name, 80, TextAlignmentOptions.TopLeft);
        var desc = panel.AddText(new Info("ClassDescription", -350, -100, 700, 450), @class.Description, 60, TextAlignmentOptions.TopLeft);
        desc.transform.GetComponent<NK_TextMeshProUGUI>().enableAutoSizing = true;

        var button = panel.AddButton(new Info("ClassImage", 500, -100, 400, 400), VanillaSprites.WoodenRoundButton, new System.Action(() =>
        {
            SetClass(@class);
            GetScrollContent();
            ReloadRightPanel();
            ClassesButton.SetIcon();
        }));
        button.Image.SetSprite(@class.Icon);
        if (ClassesRemasteredMain.Activeclass == @class)
        {
            button.AddImage(new Info("SelectedTick", 150, 150, 150, 150), VanillaSprites.SelectedTick);
        }

        return panel;
    }
    private static void SetClass(BaseClass @class)
    {
        ClassesRemasteredMain.Activeclass = @class;
    }
}
