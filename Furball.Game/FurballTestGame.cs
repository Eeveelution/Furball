﻿using Furball.Engine;
using Furball.Engine.Engine.Localization;
using Furball.Game.Screens;

namespace Furball.Game;

public class FurballTestGame : FurballGame {
    public FurballTestGame() : base(new ScreenSelector()) {}

    protected override void InitializeLocalizations() {
        LocalizationManager.AddDefaultTranslation(LocalizationStrings.Back, "Back");

        LocalizationManager.AddDefaultTranslation(LocalizationStrings.CatmullTest,          "Catmull Test");
        LocalizationManager.AddDefaultTranslation(LocalizationStrings.TextBoxTest,          "TextBox Test");
        LocalizationManager.AddDefaultTranslation(LocalizationStrings.CircleTest,           "Circle Test");
        LocalizationManager.AddDefaultTranslation(LocalizationStrings.ScrollingStutterTest, "Scrolling Stutter Test");
        LocalizationManager.AddDefaultTranslation(LocalizationStrings.AudioEffectsTest,     "Audio Effects Test");
        LocalizationManager.AddDefaultTranslation(LocalizationStrings.LoadingScreenTest,    "Loading Screen Test");
        LocalizationManager.AddDefaultTranslation(LocalizationStrings.FixedTimeStepTest,    "Fixed Time Step Test");
        LocalizationManager.AddDefaultTranslation(LocalizationStrings.LayoutingTest,        "Layouting Test");

        LocalizationManager.AddDefaultTranslation(LocalizationStrings.ChooseScreen, "Choose Screen");
    }
}
