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

	public string Name;
	public string Species;
	public string Description;
	public Sprite2D Sprite;
	public BugActiveTimes ActiveTimes;
	public BugCaughtStatus CaughtStatus;
	public int TimesCaught;
}
