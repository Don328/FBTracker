using FBTracker.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTracker.Shared.QueryObjects.Teams;
public class DivisionRivalsQuery
{
    public int TeamId { get; set; }
    public Conference Conference { get; set; }
    public Region Region { get; set; }
}
