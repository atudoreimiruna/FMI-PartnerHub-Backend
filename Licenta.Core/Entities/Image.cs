namespace Licenta.Core.Entities;

public class Image
{
    public long Id { get; set; }
    public string Name { get; set; }
    public byte[] Content { get; set; }
    public long? PostId { get; set; }
    public Post Post { get; set; }
}
