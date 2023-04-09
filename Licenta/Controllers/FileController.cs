using Licenta.Services.DTOs.Blob;
using Licenta.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Extensions.Msal;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Licenta.Api.Controllers;

[Route("api/image")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IFileManager _imageManager;
    public FileController(IFileManager imageManager)
    {
        _imageManager = imageManager;
    }

    [HttpGet()]
    public async Task<IActionResult> Get()
    {
        // Get all files at the Azure Storage Location and return them
        List<BlobDTO>? files = await _imageManager.ListAsync();

        // Returns an empty array if no files are present at the storage container
        return StatusCode(StatusCodes.Status200OK, files);
    }

    [HttpPost()]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        BlobResponseDTO? response = await _imageManager.UploadAsync(file);

        // Check if we got an error
        if (response.Error == true)
        {
            // We got an error during upload, return an error with details to the client
            return StatusCode(StatusCodes.Status500InternalServerError, response.Status);
        }
        else
        {
            // Return a success message to the client about successfull upload
            return StatusCode(StatusCodes.Status200OK, response);
        }
    }

    [HttpGet("{filename}")]
    public async Task<IActionResult> Download(string filename)
    {
        BlobDTO? file = await _imageManager.DownloadAsync(filename);

        // Check if file was found
        if (file == null)
        {
            // Was not, return error message to client
            return StatusCode(StatusCodes.Status500InternalServerError, $"File {filename} could not be downloaded.");
        }
        else
        {
            // File was found, return it to client
            return File(file.Content, file.ContentType, file.Name);
        }
    }

    [HttpDelete("filename")]
    public async Task<IActionResult> Delete(string filename)
    {
        BlobResponseDTO response = await _imageManager.DeleteAsync(filename);

        // Check if we got an error
        if (response.Error == true)
        {
            // Return an error message to the client
            return StatusCode(StatusCodes.Status500InternalServerError, response.Status);
        }
        else
        {
            // File has been successfully deleted
            return StatusCode(StatusCodes.Status200OK, response.Status);
        }
    }

    //[HttpPost]
    //[Route("upload")]
    //public async Task<IActionResult> UploadImageProfile([FromRoute] long postId)
    //{
    //    var file = Request.Form.Files[0];
    //    byte[] byteFile;

    //    var memStream = new MemoryStream();
    //    await file.CopyToAsync(memStream);
    //    byteFile = memStream.ToArray();
    //    var fileSize = memStream.Length;

    //    var returnData = await _imageManager.UploadImage(byteFile, fileSize, postId);
    //    return Ok(returnData);
    //}
}

