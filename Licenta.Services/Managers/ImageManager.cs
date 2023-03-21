using AutoMapper;
using Licenta.Core.Entities;
using Licenta.Core.Interfaces;
using Licenta.Services.DTOs.Image;
using Licenta.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Services.Managers;

public class ImageManager : IImageManager
{
    private readonly IRepository<Image> _imageRepository;
    private readonly IMapper _mapper;

    public ImageManager(
        IRepository<Image> imageRepository,
        IMapper mapper)
    {
        _imageRepository = imageRepository;
        _mapper = mapper;
    }

    // TODO: Make the size of the image larger (use Scaleway)
    public async Task<ImageViewDTO> UploadImage(byte[] file, long fileSize, long postId)
    {
        var image = new Image();
        // Upload the file if less than 2 MB
        if (fileSize < 2097152)
        {
            image.Name = $"Image:{Guid.NewGuid()}.img";
            image.Content = file;
            image.PostId = postId;

            await _imageRepository.AddAsync(image);
        }
        else
        {
            throw new Exception("File size is too large");
        }

        var returndata = await _imageRepository
            .AsQueryable()
            .Where(x => x.Name == image.Name)
            .Select(x => new ImageViewDTO()
            {
                Name = x.Name,
                ImageBase64 = String.Format("data:image/png;base64,{0}", Convert.ToBase64String(x.Content))
            }).FirstOrDefaultAsync();

        return returndata;
    }
}
