using TestTaskJob;

namespace Application.Services.Platforms;

public interface IPlatformService
{
    ResulObjectDto<List<string>> GetPlatformsFromLocation(
        string location,
        Dictionary<string, List<string>> locationToPlatforms);
}