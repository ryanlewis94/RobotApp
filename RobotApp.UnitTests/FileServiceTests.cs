using RobotApp.Services.FileService;

namespace RobotApp.UnitTests
{
    [TestFixture]
    public class FileServiceTests
    {
        [Test]
        public void ReadFileAsync_EmptyFilePath()
        {
            var fileService = new FileService();

            Exception? ex = Assert.ThrowsAsync<ArgumentNullException>(() => fileService.ReadFileAsync(string.Empty));
            Assert.That(ex!.Message, Is.EqualTo("filePath cannot be empty. Please provide a valid filePath to read from. (Parameter 'filePath')"));
        }
    }
}
