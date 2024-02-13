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
    public Texture2D Sprite;
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
        GetNode<TextureRect>("%BugSprite").Texture = Sprite;
        GetNode<RichTextLabel>("%BugName").Text = BugName;
        RichTextLabel speciesLabel = GetNode<RichTextLabel>("%BugSpecies");
        speciesLabel.Text = "";
        speciesLabel.PushFontSize(14);
        speciesLabel.PushColor(Colors.LightGray);
        speciesLabel.PushItalics();
        speciesLabel.AddText(Species);
        GetNode<RichTextLabel>("%Description").Text = Description;
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
            TextureRect scheduleIcon = GetNode<TextureRect>("%" + schedule + "Schedule");
            scheduleIcon.Texture = iconTexture;
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