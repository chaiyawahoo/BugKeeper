﻿using Godot;
using Godot.Collections;
using System;

public enum BugCaughtStatus
{
    Unseen,
    Seen,
    Caught
}

public enum BugSchedule
{
    Never = 0,

    Morning = 1,
    Day = 2,
    Evening = 4,
    Night = 8,

    Diurnal = Morning | Day | Evening,
    Nocturnal = Evening | Night | Morning,

    Matutinal = Night | Morning | Day,
    Vespertinal = Day | Evening | Night,

    Crepuscular = Morning | Evening,
    Biphasic = Day | Night,

    Early = Morning | Day,
    Late = Evening | Night,

    Party = Morning | Night,
    Worker = Day | Evening,

    Always = Morning | Day | Evening | Night,
    Cathemeral = Always
}

public partial class BugDexEntry : Control
{
    private const string BUGDEX_TEXTURES_PATH = "res://art/bugdex/textures/";

    [ExportGroup("Bug Information")]
    [Export]
    public string BugName;
    [Export]
    public string Species;
    [Export]
    public string Description;

    [ExportGroup("")]
    [Export]
    public Sprite2D Sprite;
    [Export]
    public BugSchedule ActiveSchedule;
    [Export]
    public BugCaughtStatus CaughtStatus;

    public int TimesCaught;

    private Control bugScheduleIcons;

    private static bool texturesLoaded = false;
    private static bool texturesRequested = false;
    private static AtlasTexture[] activeIconTextures;
    private static AtlasTexture[] inactiveIconTextures;

    BugDexEntry()
    {
        if (!texturesRequested)
        {
            activeIconTextures = new AtlasTexture[4];
            inactiveIconTextures = new AtlasTexture[4];
            for (int i = 0; i < 4; i++)
            {
                BugSchedule schedule = (BugSchedule)(1 << i);
                ResourceLoader.LoadThreadedRequest(BUGDEX_TEXTURES_PATH + "Active" + schedule + ".tres");
                ResourceLoader.LoadThreadedRequest(BUGDEX_TEXTURES_PATH + "Inactive" + schedule + ".tres");
            }
            texturesRequested = true;
        }
    }

    public override void _Ready()
    {
        TimesCaught = 0;
        bugScheduleIcons = new Control
        {
            Name = "BugSchedule",
            Size = new Vector2I(256, 64)
        };
        AddChild(bugScheduleIcons, forceReadableName: true);

        for (int i = 0; i < 4; i++)
        {
            BugSchedule schedule = (BugSchedule)(1 << i);
            bool isActive = (ActiveSchedule & schedule) == schedule;
            if (!texturesLoaded)
            {
                activeIconTextures[i] = (AtlasTexture)ResourceLoader.LoadThreadedGet(BUGDEX_TEXTURES_PATH + "Active" + schedule + ".tres");
                inactiveIconTextures[i] = (AtlasTexture)ResourceLoader.LoadThreadedGet(BUGDEX_TEXTURES_PATH + "Inactive" + schedule + ".tres");
            }
            AtlasTexture iconTexture = isActive ? activeIconTextures[i] : inactiveIconTextures[i];
            MarginContainer iconMargin = new MarginContainer()
            {
                Size = new Vector2I(64, 64),
                Position = new Vector2I(i * 64, 0)
            };
            TextureRect scheduleIcon = new()
            {
                Texture = iconTexture,
                ExpandMode = TextureRect.ExpandModeEnum.FitWidthProportional,
                Name = schedule.ToString() + "Schedule"
            };
            iconMargin.AddChild(scheduleIcon, forceReadableName: true);
            bugScheduleIcons.AddChild(iconMargin);
        }

        if (!texturesLoaded) texturesLoaded = true;
    }

    public void See()
    {
        CaughtStatus = BugCaughtStatus.Seen;
    }

    public void Catch()
    {
        CaughtStatus = BugCaughtStatus.Caught;
        TimesCaught++;
    }
}