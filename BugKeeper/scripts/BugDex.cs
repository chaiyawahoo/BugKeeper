using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class BugDex : Node
{
	public PackedScene BugDexEntryNode;
	public List<BugDexEntry> BugDexEntries;
	public override void _EnterTree()
	{
		base._EnterTree();
		BugDexEntryNode = ResourceLoader.Load<PackedScene>("res://scenes/bugDexEntry.tscn");
	}

    public override void _Ready()
    {
        base._Ready();
		foreach (var entry in BugDexEntries)
		{
			Node newEntryNode = BugDexEntryNode.Instantiate<Node>();
		}
    }

    public override void _Process(double delta)
	{
		base._Process(delta);
	}
}