
namespace MidasShopSolution.Api.Services.Common;
public class FileStorageService : IStorageService
{
    private readonly string _userContentFolder;
    private const string USER_CONTENT_FOLDER_NAME = "user-content";
    // public FileStorageService(IWebHostEnviroment webHostEnviroment)
    // {
    //     _userContentFolder = Path.Combine(webHostEnviroment.webRootPath, USER_CONTENT_FOLDER_NAME);
    // }
    public string GetFileUrl(string fileName)
    {
        return $"/{USER_CONTENT_FOLDER_NAME}/{fileName}";
    }
    public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
    {
        var filePath = Path.Combine(_userContentFolder, fileName);
        using var output = new FileStream(filePath, FileMode.Create);
        await mediaBinaryStream.CopyToAsync(output);
    }

    public async Task DeleteFileAsync(string fileName)
    {
        var filePath = Path.Combine(_userContentFolder, fileName);
        if (File.Exists(filePath))
        {
            await Task.Run(() => File.Delete(filePath));
        }
    }
}
