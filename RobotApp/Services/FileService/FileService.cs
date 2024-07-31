using System;
using System.IO;
using System.Threading.Tasks;

namespace RobotApp.Services.FileService
{
    public class FileService : IFileInterface
    {
        public async Task<string[]> ReadFileAsync(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath), $"{nameof(filePath)} cannot be empty. Please provide a valid {nameof(filePath)} to read from.");
            }

            return await File.ReadAllLinesAsync(filePath);
        }
    }
}
