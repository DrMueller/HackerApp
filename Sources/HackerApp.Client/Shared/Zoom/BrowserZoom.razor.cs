using HackerApp.Client.Infrastructure.JavaScript.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace HackerApp.Client.Shared.Zoom
{
    public partial class BrowserZoom
    {
        private IJSObjectReference? _module;

        [Inject]
        private IJavaScriptLocator JsLocator { get; set; } = default!;

        [Inject]
        private IJSRuntime JsRuntime { get; set; } = default!;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var jsFilePath = JsLocator.LocateJsFilePath(this);
                _module ??= await JsRuntime.InvokeAsync<IJSObjectReference>("import", jsFilePath);
            }
        }

        private async Task ZoomInAsync()
        {
            if (_module != null)
            {
                await _module.InvokeVoidAsync("zoom", true);
            }
        }

        private async Task ZoomOutAsync()
        {
            if (_module != null)
            {
                await _module.InvokeVoidAsync("zoom", false);
            }
        }
    }
}