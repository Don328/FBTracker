using FBTracker.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FBTracker.Client.Areas.Teams.TeamSchedule;
public partial class ScheduledGameDisplay : ComponentBase
{
    [Parameter]
    [EditorRequired]
    public ScheduledGame ScheduleData { get; set; } = default!;
}
