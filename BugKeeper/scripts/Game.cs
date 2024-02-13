using Godot;
using System;

public partial class Game : Node
{
	BugDex bugDex;
	bool bugDexVisible;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		bugDex = GetNode<BugDex>("%BugDex");
		bugDexVisible = bugDex.IsVisibleInTree();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("guide"))
		{
			ToggleBugDex();
		}
	}

	public void ToggleBugDex()
	{
		if (bugDexVisible)
		{
			bugDex.Hide();
		} 
		else
		{
			bugDex.Show();
		}
		bugDexVisible = !bugDexVisible;
    }
}
