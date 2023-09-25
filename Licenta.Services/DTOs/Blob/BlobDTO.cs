using System;
using System.IO;
using System.Threading.Tasks;

namespace Licenta.Services.DTOs.Blob;

public class BlobDTO
{
    public string Uri { get; set; }
    public string Name { get; set; }
    public string ContentType { get; set; }
    public byte[] Content { get; set; }

}
