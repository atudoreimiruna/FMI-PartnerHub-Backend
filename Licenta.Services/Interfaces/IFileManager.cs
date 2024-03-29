﻿using Licenta.Core.Enums;
using Licenta.Services.DTOs.Blob;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.Services.Interfaces;

public interface IFileManager
{
    Task<BlobResponseDTO> UploadAsync(FileEntityEnum entity, long id, IFormFile blob);
    Task<BlobDTO> DownloadAsync(string blobFilename);
    Task<BlobResponseDTO> DeleteAsync(string blobFilename);
    Task<List<BlobDTO>> ListAsync();
}
