using System;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Components;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Unity.Menu;
using Il2CppAssets.Scripts.Unity.UI_New;
using UnityEngine;
using TaskScheduler = BTD_Mod_Helper.Api.TaskScheduler;

namespace ClassesRemastered.UI;

public static class ClassesButton
{
    private static ModHelperPanel? _panel;
    private static ModHelperButton? _image;

    private static void OpenClassesUI()
    {
        MenuManager.instance.buttonClickSound.Play("ClickSounds");
        ModGameMenu.Open<ClassesUI>();
    }

    private static void CreatePanel(GameObject screen)
    {
        _panel = screen.AddModHelperPanel(new Info("ClassesButton")
        {
            Anchor = new Vector2(1, 0),
            Pivot = new Vector2(1, 0)
        });

        var animator = _panel.AddComponent<Animator>();
        animator.runtimeAnimatorController = Animations.PopupAnim;
        animator.speed = .75f;

        _image = _panel.AddButton(new Info("ClassesMenuButton", 0, 0, 400, 400, new Vector2(1, 0), new Vector2(0.5f, 0)), VanillaSprites.WoodenRoundButton, new Action(OpenClassesUI));
        _image.AddText(new Info("Text", 0, -175, 1000, 200), "Classes", 70f);


        var mainMenuTransform = screen.transform.Cast<RectTransform>();
        var matchLocalPosition = _image.transform.gameObject.AddComponent<MatchLocalPosition>();
        var bottomGroup = mainMenuTransform.FindChild("Friends");
        matchLocalPosition.transformToCopy = bottomGroup.transform.GetChild(0);

        var rect = mainMenuTransform.rect;
        var aspectRatio = rect.width / rect.height;
        if (aspectRatio < 1.5)
        {
            matchLocalPosition.offset = new Vector3(0, 0);
            matchLocalPosition.scale = new Vector3(1, 3.33f, 1);
        }
        else if (aspectRatio < 1.7)
        {
            matchLocalPosition.offset = new Vector3(-700, 60, 0);
            matchLocalPosition.scale = new Vector3(1, 3f, 1);
        }
        else
        {
            matchLocalPosition.offset = new Vector3(-800, 40);
        }
    }

    private static void Init()
    {
        var screen = CommonForegroundScreen.instance.transform;
        var ModSavePanel = screen.FindChild("ClassesButton");
        if (ModSavePanel == null)
            CreatePanel(screen.gameObject);
    }


    private static void HideButton()
    {
        if (_panel == null) return;
        _panel.GetComponent<Animator>().Play("PopupSlideOut");
        TaskScheduler.ScheduleTask(() => _panel.SetActive(false), ScheduleType.WaitForFrames, 13);
    }

    public static void Show()
    {
        Init();
        if (_panel == null) return;
        _panel.SetActive(true);
        _panel.GetComponent<Animator>().Play("PopupSlideIn");
    }

    public static void Hide()
    {
        var screen = CommonForegroundScreen.instance.transform;
        var ModSavePanel = screen.FindChild("ClassesButton");
        if (ModSavePanel != null)
            HideButton();
    }
    public static void SetIcon()
    {
        _image.Image.SetSprite(ClassesRemasteredMain.Activeclass.Icon);
    }
}