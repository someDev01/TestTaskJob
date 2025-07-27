using TestTaskJob;

namespace Application.Services.WriteFile;

public class WriteFileService: IWriteFileService
{
    private bool _dataLoaded = false;
    public async Task<ResulObjectDto<string>> LoadFromFile(
        string fileName, 
        Dictionary<string, List<string>> locationToPlatforms, 
        CancellationToken cancellationToken = default)
    {
        if (_dataLoaded)
        {
            return new ResulObjectDto<string>
            {
                IsSuccess = false,
                Message = "Данные уже загружены!",
                Data = null,
            };
        }
        try
        {
            if(!File.Exists(fileName))
            {
                return new ResulObjectDto<string>
                {
                    IsSuccess = false,
                    Message = $"По указаному пути: {fileName} файла не существует",
                };
            }

            string[] lines = await File.ReadAllLinesAsync(fileName, cancellationToken);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || !line.Contains(':')) continue;

                string[] divider = line.Split(':');

                string platformWithoutSpace = divider[0].Trim();
                string[] locations = divider[1].Split(',');

                foreach( string location in locations)
                {
                    string locationWithoutSpace = location.Trim();

                    if(!locationToPlatforms.ContainsKey(locationWithoutSpace))
                    {
                        locationToPlatforms[locationWithoutSpace] = new List<string>();
                    }

                    locationToPlatforms[locationWithoutSpace].Add(platformWithoutSpace);
                }
            }
            
            _dataLoaded = true;
            
            Console.WriteLine($"данные файла: {string.Join("\n", lines)}");
            return new ResulObjectDto<string>
            {
                IsSuccess = true,
                Message = "Данные успешно перезаписались",
                Data = $"данные файла: {string.Join("\n", lines)}"
            };
        }
        
        catch(Exception ex)
        {
            return new ResulObjectDto<string>
            {
                IsSuccess = false,
                Message = $"Ошибка при чтении файла: {ex.Message}",
                Data = $"Файл: {fileName}"
            };
        }
    }
}