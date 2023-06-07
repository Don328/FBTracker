using FBTracker.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTracker.Shared.Models;
public class Team
{
    public int Id { get; set; }
    public int Season { get; set; }
    public string Locale { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Abrev { get; set; } = string.Empty;
    public Conference Conference { get; set; }
    public Region Region { get; set; }
    public string Division { get => $"{Conference}-{Region}"; }
}
