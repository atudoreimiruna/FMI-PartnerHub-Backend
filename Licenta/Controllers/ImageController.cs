using Licenta.Core.Entities;
using Licenta.Infrastructure;
using Licenta.Services.DTOs.Image;
using Licenta.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Api.Controllers;

[Route("api/image")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly IImageManager _imageManager;
    private readonly AppDbContext _context;
    public ImageController(IImageManager imageManager, AppDbContext context)
    {
        _imageManager = imageManager;
        _context = context;
    }

    [HttpPost]
    [Route("upload")]
    public async Task<IActionResult> UploadImageProfile([FromForm] ImagePostDTO fileviewmodel)
    {
        if (ModelState.IsValid)
        {
            using (var memoryStream = new MemoryStream())
            {
                await fileviewmodel.File.CopyToAsync(memoryStream);

                // Upload the file if less than 2 MB
                if (memoryStream.Length < 2097152)
                {
                    //create a AppFile mdoel and save the image into database.
                    var file = new Image()
                    {
                        Name = fileviewmodel.Name,
                        Content = memoryStream.ToArray()
                    };

                    _context.Images.Add(file);
                    _context.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("File", "The file is too large.");
                }
            }

            var returndata = _context.Images
                .Where(c => c.Name == fileviewmodel.Name)
                .Select(c => new ImageViewDTO()
                {
                    Name = c.Name,
                    ImageBase64 = String.Format("data:image/png;base64,{0}", Convert.ToBase64String(c.Content))
                }).FirstOrDefault();
            return Ok(returndata);
        }
        return Ok("Invalid");
    }
}

