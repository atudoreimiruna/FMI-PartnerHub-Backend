using Licenta.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace Licenta.Api.Controllers;

[Route("api/image")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly IImageManager _imageManager;
    public ImageController(IImageManager imageManager)
    {
        _imageManager = imageManager;
    }

    [HttpPost]
    [Route("upload")]
    public async Task<IActionResult> UploadImageProfile([FromRoute] long postId)
    {
        var file = Request.Form.Files[0];
        byte[] byteFile;

        var memStream = new MemoryStream();
        await file.CopyToAsync(memStream);
        byteFile = memStream.ToArray();
        var fileSize = memStream.Length;

        var returnData = await _imageManager.UploadImage(byteFile, fileSize, postId);
        return Ok(returnData);
    }
}

