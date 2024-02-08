using Godot;
using System;

public class BugDexEntry {
    public BugDexEntry() {
        Name = "name";
        Species = "species";
        Description = "description";
        Sprite = new Sprite2D();
        ActiveTimes = BugActiveTimes.Never;
        CaughtStatus = BugCaughtStatus.Unseen;
        TimesCaught = 0;
    }

    public BugDexEntry(string name, string species, string description, string spritePath, string activeTimes) {
        Name = name;
        Species = species;
        Description = description;
        Sprite = ResourceLoader.Load<Sprite2D>(spritePath);
        ActiveTimes = Enum.Parse<BugActiveTimes>(activeTimes);
        CaughtStatus = BugCaughtStatus.Unseen;
        TimesCaught = 0;
    }

    public string Name;
    public string Species;
    public string Description;
    public Sprite2D Sprite;
    public BugActiveTimes ActiveTimes;
    public BugCaughtStatus CaughtStatus;
    public int TimesCaught;
}