using Godot;
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

    Always = Morning | Day | Evening | Night,
    Cathemeral = Always
}

public partial class BugDexEntry : Control
{
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
    private static AtlasTexture[] activeIconTextures;
    private static AtlasTexture[] inactiveIconTextures;

    BugDexEntry()
    {
        if (!texturesLoaded)
        {
            activeIconTextures = new AtlasTexture[4];
            inactiveIconTextures = new AtlasTexture[4];
            for (int i = 0; i < 4; i++)
            {
                BugSchedule schedule = (BugSchedule)(1 << i);
                activeIconTextures[i] = ResourceLoader.Load<AtlasTexture>("res://art/bugdex/textures/Active" + schedule.ToString() + ".tres");
                inactiveIconTextures[i] = ResourceLoader.Load<AtlasTexture>("res://art/bugdex/textures/Inactive" + schedule.ToString() + ".tres");
            }
            texturesLoaded = true;
        }
    }

    public override void _Ready()
    {
        TimesCaught = 0;
        bugScheduleIcons = new Control();
        bugScheduleIcons.Name = "BugSchedule";
        bugScheduleIcons.Size = new Vector2I(256, 64);
        AddChild(bugScheduleIcons, forceReadableName: true);

        for (int i = 0; i < 4; i++)
        {
            BugSchedule schedule = (BugSchedule)(1 << i);
            bool isActive = (ActiveSchedule & schedule) == schedule;
            AtlasTexture iconTexture = isActive ? activeIconTextures[i] : inactiveIconTextures[i];
            TextureRect scheduleIcon = new TextureRect();
            scheduleIcon.Texture = iconTexture;
            scheduleIcon.ExpandMode = TextureRect.ExpandModeEnum.FitWidthProportional;
            scheduleIcon.Size = new Vector2I(64, 64);
            scheduleIcon.Position = new Vector2I(i * 64, 0);
            scheduleIcon.Name = schedule.ToString() + "Schedule";
            bugScheduleIcons.AddChild(scheduleIcon, forceReadableName: true);
        }
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