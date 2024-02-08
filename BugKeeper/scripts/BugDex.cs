using Godot;
using System;

public enum BugCaughtStatus {
	Unseen,
	Seen,
	Caught
}

public enum BugActiveTimes {
	Never = 0,
	Morning = 1,
	Day = 2,
	Evening = 4,
	Night = 8,
	Always = 15
}

public partial class BugDex : Node
{
	public override void _Ready()
	{
		BugDexEntry entry = new BugDexEntry();
		entry.Name = "Big Bug";
		GD.Print(entry.Name);
	}
	
	public override void _Process(double delta)
	{
	}
}

public struct BugDexEntry 
{
	public BugDexEntry()
	{
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
