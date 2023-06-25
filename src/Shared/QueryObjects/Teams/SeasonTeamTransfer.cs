using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTracker.Shared.QueryObjects.Teams;
public class SeasonTeamTransfer
{
    public int LoadFromSeason { get; set; }
    public int LoadToSeason { get; set; }
}
