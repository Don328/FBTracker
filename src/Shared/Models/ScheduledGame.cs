using FBTracker.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTracker.Shared.Models;
public class ScheduledGame
{
    public int Id { get; set; }
    public int Season { get; set; }
    public int Week { get; set; }
    public int HomeTeamId { get; set; }
    public int AwayTeamId { get; set; }
    public int ByeTeamId { get; set; }
    public GameDay GameDay { get; set; }
}
