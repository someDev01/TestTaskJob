using TestTaskJob;

namespace Application.Services.Platforms;

public class PlatformService:IPlatformService
{
    public ResulObjectDto<List<string>> GetPlatformsFromLocation(string location, Dictionary<string, List<string>> locationToPlatforms)
    {
        List<string> result = new();

        var arrayLocation = location.
            Trim('/')
            .Split("/");

        for(int i = arrayLocation.Length; i >= 1; i--)
        {
            string creatingLocation = "/";

            for(int j = 0; j<i; j++)
            {
                creatingLocation += arrayLocation[j];
                if(j != i-1) creatingLocation += "/";
            }

            if (locationToPlatforms.ContainsKey(creatingLocation))
            {
                List<string> platforms = locationToPlatforms[creatingLocation];
                result.AddRange(platforms);
            }
        }

        if (result.Count == 0)
        {
            return new ResulObjectDto<List<string>>
            {
                IsSuccess = false,
                Message = $"Для региона/регионов: '{location}' не найдено платформ!"
            };
        }

        return new ResulObjectDto<List<string>>
        {
            IsSuccess = true,
            Message = $"По регионам/региону: '{location}' найдены платформы!",
            Data = result
        };
    }
}