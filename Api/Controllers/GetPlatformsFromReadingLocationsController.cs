using Application.Services.Platforms;
using Application.Services.WriteFile;
using Microsoft.AspNetCore.Mvc;

namespace TestTaskJob.Controllers;

[ApiController]
[Route("[controller]")]
public class GetPlatformsFromReadingLocationsController(
    IWriteFileService writeFileService,
    IPlatformService platformService): ControllerBase
{
    private const string FILEPATH = @"...";
    private static readonly Dictionary<string, List<string>> locationToPlatforms = new();
    
    [HttpPost("read/file")]
    public async Task<IActionResult> WriteLocationsFromFileAsync()
    {
        var result = await writeFileService.LoadFromFile(FILEPATH, locationToPlatforms);
        if(!result.IsSuccess) 
            return BadRequest(result.Message);
        
        return Ok(result.Message);
    }

    [HttpGet("platforms/from/locations")]
    public IActionResult GetPlatformsFromReadingLocations(
        string location)
    {
        var result = platformService.GetPlatformsFromLocation(location, locationToPlatforms);
        if(!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(new {message = result.Message, platforms = result.Data});
    }
}

