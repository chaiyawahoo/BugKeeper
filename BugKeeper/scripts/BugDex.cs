using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class BugDex : Node
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
			newEntryNode.ActiveSchedule = (BugSchedule)i;
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