using Licenta.Core.Enums;

namespace Licenta.Core.Entities;

public class StudentJob
{
    public long StudentId { get; set; }
    public virtual Student Student { get; set; }

    public long JobId { get; set; }
    public virtual Job Job { get; set; }
    public StudentJobStatusEnum JobStatus { get; set; }
    public StudentJobRatingEnum JobRating { get; set; }
}
