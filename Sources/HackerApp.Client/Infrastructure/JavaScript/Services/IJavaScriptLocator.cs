using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Infrastructure.JavaScript.Services
{
    public interface IJavaScriptLocator
    {
        string LocateJsFilePath(ComponentBase component);
    }
}