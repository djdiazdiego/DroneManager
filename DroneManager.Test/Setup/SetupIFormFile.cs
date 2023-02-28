using Microsoft.AspNetCore.Http;
using Moq;

namespace DroneManager.Test.Setup
{
    public class SetupIFormFile
    {
        private readonly IFormFile _file;

        public SetupIFormFile(string fileName = "file.jpg", string content = "File Content", string contentType = "image")
        {
            var fileMock = new Mock<IFormFile>();

            using var ms = new MemoryStream();
            using var writer = new StreamWriter(ms);

            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.ContentType).Returns(contentType);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            _file = fileMock.Object;
        }

        public IFormFile GetFile() => _file;
    }
}
