using HackerApp.Client.Shell.ExceptionHandling;

namespace HackerApp.Client.Layout
{
    public partial class MainLayout
    {
        private AppError? AppError { get; set; }

        private AppErrorBoundary? ErrorBoundary { get; set; }

        protected override void OnParametersSet()
        {
            ErrorBoundary?.Recover();
        }

        private void HandleExceptionThrown(Exception arg)
        {
            AppError = new AppError(arg.GetType().Name, arg.Message, arg.StackTrace!);
        }
    }
}