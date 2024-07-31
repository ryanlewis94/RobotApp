using System.Collections.Generic;
using System.Threading.Tasks;

namespace RobotApp.Services.FileService
{
    public interface IFileInterface
    {
        Task<string[]> ReadFileAsync(string filePath);
    }
}
