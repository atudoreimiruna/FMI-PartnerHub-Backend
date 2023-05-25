﻿using Licenta.Core.Enums;
using Licenta.Services.DTOs.Blob;
using Licenta.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        List<BlobDTO>? files = await _imageManager.ListAsync();

        return StatusCode(StatusCodes.Status200OK, files);
    }

    [HttpPost("{entity}/{id}")]
    public async Task<IActionResult> Upload([FromRoute] FileEntityEnum entity, [FromRoute] long id, IFormFile file)
    {
        BlobResponseDTO? response = await _imageManager.UploadAsync(entity, id, file);

        if (response.Error == true)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, response.Status);
        }
        else
        {
            return StatusCode(StatusCodes.Status200OK, response);
        }
    }

    [HttpGet("{filename}")]
    public async Task<IActionResult> Download([FromRoute] string filename)
    {
        BlobDTO? file = await _imageManager.DownloadAsync(filename);

        if (file == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"File {filename} could not be downloaded.");
        }
        else
        {
            return File(file.Content, file.ContentType, file.Name);
        }
    }

    [HttpDelete("filename")]
    public async Task<IActionResult> Delete([FromRoute] string filename)
    {
        BlobResponseDTO response = await _imageManager.DeleteAsync(filename);

        if (response.Error == true)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, response.Status);
        }
        else
        {
            return StatusCode(StatusCodes.Status200OK, response.Status);
        }
    }
}

