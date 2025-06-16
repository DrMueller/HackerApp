using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Shared.Navigation.Components
{
    public partial class Redirector
    {
        [Inject]
        public NavigationManager Navigator { get; set; } = null!;

        [Parameter]
        [EditorRequired]
        public required string RedirectPath { get; set; }

        protected override void OnInitialized()
        {
            Navigator.NavigateTo(RedirectPath, true);
        }
    }
}