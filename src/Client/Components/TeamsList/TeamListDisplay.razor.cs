using FBTracker.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FBTracker.Client.Components.TeamsList;
public partial class TeamListDisplay : ComponentBase
{
    [Parameter]
    [EditorRequired]
    public Team Team { get; set; } = default!;
}
