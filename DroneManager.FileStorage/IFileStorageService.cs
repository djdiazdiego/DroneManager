using DroneManager.Core.Models;
using Microsoft.AspNetCore.Http;

namespace DroneManager.FileStorage;

public interface IFileStorageService
{
    Task<StorageFileData> UploadFile(IFormFile file, CancellationToken cancellationToken = default);
    Task<StorageFileData> GetFile(string id, CancellationToken cancellationToken = default);
    Task<bool> DeleteFile(string id, CancellationToken cancellationToken = default);
    Task<byte[]> DownloadFile(string id, CancellationToken cancellationToken = default);
}
