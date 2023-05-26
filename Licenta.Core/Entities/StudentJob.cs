using Licenta.Core.Entities;

namespace Licenta.Core.Entities;

public class StudentJob
{
    public long StudentId { get; set; }
    public virtual Student Student { get; set; }

    public long JobId { get; set; }
    public virtual Job Job { get; set; }
}
