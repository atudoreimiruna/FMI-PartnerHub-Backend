namespace Licenta.Core.Entities;

public class StudentPartner
{
    public long StudentId { get; set; }
    public virtual Student Student { get; set; }

    public long PartnerId { get; set; }
    public virtual Partner Partner { get; set; }
}
