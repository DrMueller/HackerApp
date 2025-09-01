using BlazorBootstrap;
using JetBrains.Annotations;

namespace HackerApp.Client.Shared.Modal
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public partial class GenericModal
    {
        private ConfirmDialog _dialog = null!;

        private string ConfirmationText { get; set; } = null!;

        private string ConfirmationTitle { get; set; } = null!;

        public async Task<bool> ShowConfirmationAsync(string title, string text, string yesButtonText, string noButtonText)
        {
            ConfirmationTitle = title;
            ConfirmationText = text;

            var options = new ConfirmDialogOptions
            {
                YesButtonText = yesButtonText,
                YesButtonColor = ButtonColor.Primary,
                NoButtonText = noButtonText,
                NoButtonColor = ButtonColor.Secondary,
                IsVerticallyCentered = true
            };

            try
            {
                var confirmation = await _dialog.ShowAsync(
                    ConfirmationTitle,
                    ConfirmationText,
                    options);

                return confirmation;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
    }
}