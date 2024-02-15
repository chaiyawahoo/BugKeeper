using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using Array = Godot.Collections.Array;

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

        string bugJson = FileAccess.GetFileAsString("res://bugs.json");
		Dictionary bugData = (Dictionary)Json.ParseString(bugJson);
		Dictionary bugTypes = (Dictionary)bugData["bugs"];
		Array ants = (Array)bugTypes["ants"];
		Array spiders = (Array)bugTypes["spiders"];
		Array bugArray = new Array();
		bugArray += ants + spiders;
        ResourceLoader.LoadThreadedRequest("res://art/bugdex/textures/ActiveDay.tres");
		int index = 0;
        foreach (Dictionary bug in bugArray)
		{
			BugDexEntry newEntry = BugDexEntryNode.Instantiate<BugDexEntry>();
			newEntry.BugName = newEntry.Name = (string)bug["name"];
			newEntry.Species = (string)bug["species"];
			newEntry.Description = (string)bug["description"];
			newEntry.ActiveSchedule = Enum.Parse<BugSchedule>((string)bug["schedule"]);
			newEntry.SetMeta("index", index++);
			GD.Print(newEntry.BugName);
			BugDexEntries.Add(newEntry);
		}
		//for (int i = 0; i < 16; i++)
		//{
  //          BugDexEntry newEntryNode = BugDexEntryNode.Instantiate<BugDexEntry>();
		//	newEntryNode.BugName = (BugSchedule)i + " Bug";
		//	newEntryNode.Name = newEntryNode.BugName;
		//	newEntryNode.Species = "Buggus Specius (" + (BugSchedule)i + ")";
		//	newEntryNode.Description = "This bug's schedule can be described with a word: " + (BugSchedule)i;
		//	newEntryNode.ActiveSchedule = (BugSchedule)i;
		//	ResourceLoader.LoadThreadedRequest("res://art/bugdex/textures/ActiveDay.tres");
		//	newEntryNode.SetMeta("index", i);
  //          BugDexEntries.Add(newEntryNode);
  //      }
    }

    public override void _Ready()
    {
		bugInfo = GetNode<TabContainer>("%BugInfo");
		bugSelector = GetNode<VBoxContainer>("%BugSelector");
		Texture2D bugTexture = (Texture2D)ResourceLoader.LoadThreadedGet("res://art/bugdex/textures/ActiveDay.tres");
        foreach (BugDexEntry entry in BugDexEntries)
		{
			Button bugButton = new()
			{
				Text = entry.BugName
			};
			entry.Sprite = bugTexture;
            GD.Print(entry.Sprite.ResourceName);
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