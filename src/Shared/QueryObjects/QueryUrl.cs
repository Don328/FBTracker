using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTracker.Shared.QueryObjects;
public record QueryUrl(
    Uri Uri,
    string Controller,
    string Route)
{
    public override string ToString()
        => Uri.ToString() + Controller + "/" + Route;
}
