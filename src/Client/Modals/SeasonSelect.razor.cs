using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FBTracker.Client.Modals;
public partial class SeasonSelect : ComponentBase
{
    [CascadingParameter]
    public BlazoredModalInstance This { get; set; } = default!;

    private class FormModel
    {
        public int Season { get; set; }
    }

    private FormModel _model = new();

    private async Task OnSubmit()
    {
        await This.CloseAsync(
            ModalResult.Ok(_model.Season));
    }

    private async Task OnCancel()
    {
        await This.CancelAsync();
    }
}
