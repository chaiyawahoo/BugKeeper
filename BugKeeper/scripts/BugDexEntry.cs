using Godot;
using Godot.Collections;
using System;

public enum BugCaughtStatus
{
    Unseen,
    Seen,
    Caught
}

public enum BugSchedule
{
    Never = 0,

    Morning = 1,
    Day = 2,
    Evening = 4,
    Night = 8,

    Diurnal = Morning | Day | Evening,
    Nocturnal = Evening | Night | Morning,

    Matutinal = Night | Morning | Day,
    Vespertinal = Day | Evening | Night,

    Crepuscular = Morning | Evening,
    Biphasic = Day | Night,

    Early = Morning | Day,
    Late = Evening | Night,

    Always = Morning | Day | Evening | Night,
    Cathemeral = Always
}

public partial class BugDexEntry : Control
{
    [ExportGroup("Bug Information")]
    [Export]
    public string BugName;
    [Export]
    public string Species;
    [Export]
    public string Description;

    [ExportGroup("")]
    [Export]
    public Sprite2D Sprite;
    [Export]
    public BugSchedule ActiveSchedule;
    [Export]
    public BugCaughtStatus CaughtStatus;

    public int TimesCaught;

    private Node bugScheduleIcons;


    public override void _Ready()
    {
        TimesCaught = 0;
        bugScheduleIcons = FindChild("BugSchedule");

        UpdateScheduleIcons();
    }

    public void See()
    {
        CaughtStatus = BugCaughtStatus.Seen;
    }

    public void Catch()
    {
        CaughtStatus = BugCaughtStatus.Caught;
        TimesCaught++;
    }

    private void ToggleScheduleIcon(BugSchedule schedule, bool active)
    {
        Control activeIcon = (Control)bugScheduleIcons.FindChild("Active" + schedule.ToString());
        Control inactiveIcon = (Control)bugScheduleIcons.FindChild("Inactive" + schedule.ToString());
        if (active)
        {
            activeIcon.Visible = true;
            inactiveIcon.Visible = false;
        }
        else
        {
            inactiveIcon.Visible = true;
            activeIcon.Visible = false;
        }
    }

    private void UpdateScheduleIcons()
    {
        for (int i = 0; i < 4; i++)
        {
            BugSchedule schedule = (BugSchedule)(1 << i);
            bool isActive = (ActiveSchedule & schedule) == schedule;
            ToggleScheduleIcon(schedule, isActive);
        }
    }
}