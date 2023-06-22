using Blazored.Modal.Services;
using FBTracker.Shared.GloblaConstants;
using System.Reflection;

namespace FBTracker.Client.Modals;

internal static class SeasonSelectService
{
    internal static async Task<int?> Show(IModalService modal)
    {
        var seasonSelect = modal.Show<SeasonSelect>();
        var result = await seasonSelect.Result;

        if (result.Confirmed &&
            result.Data is not null)
        {
            var season = Int32.Parse(result.Data.ToString() ?? "");
            if (season >= StateConstants.seasonMin &&
                season <= StateConstants.seasonMax)
            {
                return season;
            }
        }

        return null;
    }
}
