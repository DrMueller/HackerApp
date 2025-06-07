using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Infrastructure.JavaScript.Services.Implementation
{
    [UsedImplicitly]
    internal class JavaScriptLocator : IJavaScriptLocator
    {
        public string LocateJsFilePath(ComponentBase component)
        {
            return LocateJsFilePath(component.GetType());
        }

        public string LocateJsFilePath<T>() where T : ComponentBase
        {
            return LocateJsFilePath(typeof(T));
        }

        private static string LocateJsFilePath(Type type)
        {
            var assemblyFullName = type.Assembly.FullName;
            var assemblyName = type.Assembly.FullName!.Substring(0, assemblyFullName!.IndexOf(','));
            var relativeNamespace = type.FullName!.Replace(assemblyName, string.Empty);

            var path = relativeNamespace.Replace(".", "/");
            path += ".razor.js";

            return path;
        }
    }
}