using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Licenta.Services.DTOs.Image;

public class ImagePostDTO
{
    public string Name { get; set; }
    public IFormFile File { get; set; }
    public List<IFormFile> Files { get; set; }
}
