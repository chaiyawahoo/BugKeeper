using Godot;
using System;

public partial class BugDexEntry : Node
{
	public enum CaughtStatus 
	{
		Unseen,
		Seen,
		Caught
	}

	public string BugSpecies { get; private set; }
	public string BugName { get; private set; }
	public Sprite2D BugSprite { get; private set; }
	public CaughtStatus BugCaught { get; private set; }

    public override void _EnterTree() {
		// TODO: Access Bug JSON
        BugCaught = CaughtStatus.Unseen;
    }

    public override void _Ready()
	{
	}

	
	public override void _Process(double delta)
	{
	}
}
