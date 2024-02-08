using Godot;
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
    public BugSchedule Schedule;
    [Export]
    public BugCaughtStatus CaughtStatus;
    public int TimesCaught;

    //public BugDexEntry() {
    //    BugName = "name";
    //    Species = "species";
    //    Description = "description";
    //    Sprite = new Sprite2D();
    //    Schedule = BugSchedule.Never;
    //    CaughtStatus = BugCaughtStatus.Unseen;
    //    TimesCaught = 0;
    //}

    //public BugDexEntry(string name, string species, string description, string spritePath, string schedule) {
    //    BugName = name;
    //    Species = species;
    //    Description = description;
    //    Sprite = ResourceLoader.Load<Sprite2D>(spritePath);
    //    Schedule = Enum.Parse<BugSchedule>(schedule);
    //    CaughtStatus = BugCaughtStatus.Unseen;
    //    TimesCaught = 0;
    //}

    public void See() {
        CaughtStatus = BugCaughtStatus.Seen;
    }

    public void Catch() {
        CaughtStatus = BugCaughtStatus.Caught;
        TimesCaught++;
    }
}