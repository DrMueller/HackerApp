using HackerApp.Client.Areas.NewGame.Components;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Shell.ExceptionHandling
{
    [UsedImplicitly]
    public partial class ErrorInfo
    {
        [Parameter]
        [EditorRequired]
        public AppError? AppError { get; set; }

        [Inject]
        public required NavigationManager Navigator { get; set; }

        private void GoHome()
        {
            Navigator.NavigateTo(NewGamePage.Path, true);
        }
    }
}