using TestTaskJob;

namespace Application.Services.WriteFile;

public interface IWriteFileService
{
    Task<ResulObjectDto<string>> LoadFromFile(
        string fileName, 
        Dictionary<string, List<string>> locationToPlatforms, 
        CancellationToken cancellationToken = default);
}