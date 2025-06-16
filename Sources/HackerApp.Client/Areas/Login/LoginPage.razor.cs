using HackerApp.Client.Areas.Login.Dtos;
using HackerApp.Client.Infrastructure.HttpRequests;
using HackerApp.Client.Infrastructure.InformationHandling.Dtos;
using HackerApp.Client.Infrastructure.InformationHandling.Extensions;
using HackerApp.Client.Infrastructure.InformationHandling.Models;
using HackerApp.Client.Infrastructure.JavaScript.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace HackerApp.Client.Areas.Login
{
    public partial class LoginPage
    {
        public const string Path = "/login";
        private IJSObjectReference? _module;

        [Inject]
        public required IHttpClientProxy ClientProxy { get; set; }

        [Inject]
        public required IJavaScriptLocator JsLocator { get; set; }

        [Inject]
        public required IJSRuntime JsRuntime { get; set; }

        private InformationEntries? Infos { get; set; }

        private LoginRequestDto LoginModel { get; } = new();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await AssureJavascriptModuleAsync();
            }
        }

        private async Task AssureJavascriptModuleAsync()
        {
            var jsFilePath = JsLocator.LocateJsFilePath(this);
            _module ??= await JsRuntime.InvokeAsync<IJSObjectReference>("import", jsFilePath);
        }

        private async Task HandleValidSubmitAsync()
        {
            Infos = await ClientProxy
                .PostAsync<InformationEntriesDto, LoginRequestDto>("api/accounts/verifylogin", new LoginRequestDto
                {
                    UserName = LoginModel.UserName,
                    Password = LoginModel.Password
                }).MapToInformationEntriesAsync();

            if (!Infos.ErrorMessages.Any())
            {
                await _module!.InvokeVoidAsync("submitForm");
            }
        }
    }
}