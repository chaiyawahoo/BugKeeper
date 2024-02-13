using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class BugDex : Control
{
	public PackedScene BugDexEntryNode;
	public List<BugDexEntry> BugDexEntries;

	private TabContainer bugInfo;
	private VBoxContainer bugSelector;
	public override void _EnterTree()
	{
		BugDexEntryNode = ResourceLoader.Load<PackedScene>("res://scenes/bugDexEntry.tscn");
		BugDexEntries = new List<BugDexEntry>();
		for (int i = 0; i < 16; i++)
		{
            BugDexEntry newEntryNode = BugDexEntryNode.Instantiate<BugDexEntry>();
			newEntryNode.BugName = (BugSchedule)i + " Bug";
			newEntryNode.Name = newEntryNode.BugName;
			newEntryNode.Species = "Buggus Specius (" + (BugSchedule)i + ")";
			newEntryNode.Description = "This bug's schedule can be described with a word: " + (BugSchedule)i;
			newEntryNode.ActiveSchedule = (BugSchedule)i;
			ResourceLoader.LoadThreadedRequest("res://art/bugdex/textures/ActiveDay.tres");
			newEntryNode.SetMeta("index", i);
            BugDexEntries.Add(newEntryNode);
        }
    }

    public override void _Ready()
    {
		bugInfo = GetNode<TabContainer>("%BugInfo");
		bugSelector = GetNode<VBoxContainer>("%BugSelector");
        foreach (BugDexEntry entry in BugDexEntries)
		{
			Button bugButton = new()
			{
				Text = entry.BugName
			};
			entry.Sprite = (Texture2D)ResourceLoader.LoadThreadedGet("res://art/bugdex/textures/ActiveDay.tres");
			bugButton.Connect("pressed", Callable.From(() => bugInfo.CurrentTab = (int)entry.GetMeta("index")));
			bugSelector.AddChild(bugButton);
			bugInfo.AddChild(entry);
		}
    }

    public override void _Process(double delta)
	{
	}

	private void SelectBug(int index)
	{
		bugInfo.CurrentTab = index;
	}
}