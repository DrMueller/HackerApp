using HackerApp.Client.Infrastructure.InformationHandling.Dtos;
using HackerApp.Client.Infrastructure.InformationHandling.Models;

namespace HackerApp.Client.Infrastructure.InformationHandling.Extensions
{
    public static class InformationEntriesDtoExtensions
    {
        public static async Task<InformationEntries> MapToInformationEntriesAsync(this Task<InformationEntriesDto> dtoTask)
        {
#pragma warning disable VSTHRD003 // Avoid awaiting foreign Tasks
            var dto = await dtoTask;
#pragma warning restore VSTHRD003 // Avoid awaiting foreign Tasks
            return dto.MapToInformationEntries();
        }

        private static InformationEntries MapToInformationEntries(this InformationEntriesDto dto)
        {
            var infoEntries = dto.ErrorMessages.Aggregate(InformationEntries.Empty, (current, errorMessage) => current.AddError(errorMessage));
            infoEntries = dto.WarningMessages.Aggregate(infoEntries, (current, warningMessage) => current.AddWarning(warningMessage));
            infoEntries = dto.InfoMessages.Aggregate(infoEntries, (current, infoMessage) => current.AddInformation(infoMessage));

            return infoEntries;
        }
    }
}