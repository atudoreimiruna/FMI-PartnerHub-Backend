namespace Licenta.Core.Entities;

public class File
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Uri { get; set; }
    public long? PostId { get; set; }
    public Post Post { get; set; }
}
