using Licenta.Services.DTOs.Image;
using System.Threading.Tasks;

namespace Licenta.Services.Interfaces;

public interface IImageManager
{
    Task<ImageViewDTO> UploadImage(byte[] file, long fileSize, long postId);
}
