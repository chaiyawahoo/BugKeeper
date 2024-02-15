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
			newEntry.SpritePath = "res://art/bugdex/bugs/" + (string)bug["sprite"] + ".tres";
            ResourceLoader.LoadThreadedRequest(newEntry.SpritePath);
			newEntry.SetMeta("index", index++);
			GD.Print(newEntry.BugName);
			BugDexEntries.Add(newEntry);
		}
    }

    public override void _Ready()
    {
		bugInfo = GetNode<TabContainer>("%BugInfo");
		bugSelector = GetNode<VBoxContainer>("%BugSelector");
		Texture2D bugTexture = (Texture2D)ResourceLoader.LoadThreadedGet("res://art/bugdex/textures/ActiveDay.tres");
		ButtonGroup bugButtons = new();
        foreach (BugDexEntry entry in BugDexEntries)
		{
			Button bugButton = new()
			{
				Text = entry.BugName,
				Alignment = HorizontalAlignment.Right,
				ToggleMode = true,
				ButtonGroup = bugButtons
			};
			entry.Sprite = (Texture2D)ResourceLoader.LoadThreadedGet(entry.SpritePath);
            GD.Print(entry.Sprite.ResourceName);
			bugButton.Connect("pressed", Callable.From(() => bugInfo.CurrentTab = (int)entry.GetMeta("index")));
			if ((int)entry.GetMeta("index") == 0)
			{
				bugButton.ButtonPressed = true;
			}
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
