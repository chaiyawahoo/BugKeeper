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