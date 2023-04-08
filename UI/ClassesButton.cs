using BTD_Mod_Helper.Api.Components;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Api;
using UnityEngine;
using BTD_Mod_Helper.Extensions;
using BTD_Mod_Helper.UI.Menus;
using Random = System.Random;
using ClassesMenuUI;
using MelonLoader;
using TaskScheduler = BTD_Mod_Helper.Api.TaskScheduler;
using Il2CppAssets.Scripts.Unity.Menu;
using Il2CppAssets.Scripts.Unity.UI_New;
using Il2CppAssets.Scripts.Simulation.Towers.Filters;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using System;
using ClassesRemastered;

public static class ClassesButton
{
    private static ModHelperPanel panel;
    public static ModHelperButton image;
    public static Sprite Icon = ModContent.GetSprite<ClassesRemasteredMain>("None");
    private static void OpenClassesUI()
    {
        MenuManager.instance.buttonClickSound.Play("ClickSounds");
        ModGameMenu.Open<ClassesUI>();
    }

    public static void CreatePanel(GameObject screen)
    {
        //Random rnd = new Random();
        //int Randomnumber = rnd.Next(1, 5);
        /*switch (Randomnumber)
        {
            case 1:
                Icon = VanillaSprites.PrimaryBtn2;
                break;
            case 2:
                Icon = VanillaSprites.MilitaryBtn2;
                break;
            case 3:
                Icon = VanillaSprites.MagicBtn2;
                break;
            case 4:
                Icon = VanillaSprites.SupportBtn;
                break;
        }*/
        panel = screen.AddModHelperPanel(new Info("ClassesButton")
        {
            Anchor = new Vector2(1, 0),
            Pivot = new Vector2(1, 0)
        });

        var animator = panel.AddComponent<Animator>();
        animator.runtimeAnimatorController = Animations.PopupAnim;
        animator.speed = .75f;

        image = panel.AddButton(new Info("ClassesMenuButton", 0, 0, 400, 400, new Vector2(1, 0), new Vector2(0.5f, 0)), VanillaSprites.WoodenRoundButton, new Action(OpenClassesUI));
        image.AddText(new Info("Text", 0, -175, 1000, 200), "Classes", 70f);


        var mainMenuTransform = screen.transform.Cast<RectTransform>();
        var matchLocalPosition = image.transform.gameObject.AddComponent<MatchLocalPosition>();
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
        panel.GetComponent<Animator>().Play("PopupSlideOut");
        TaskScheduler.ScheduleTask(() => panel.SetActive(false), ScheduleType.WaitForFrames, 13);
    }

    public static void Show()
    {
        Init();
        panel.SetActive(true);
        panel.GetComponent<Animator>().Play("PopupSlideIn");
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
        image.Image.SetSprite(ClassesRemasteredMain.activeclass.Icon);
    }
}