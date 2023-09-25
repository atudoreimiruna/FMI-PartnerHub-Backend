using Licenta.Core.Enums;

namespace Licenta.Services.DTOs.Student;

public class StudentJobPutDTO
{
    public long Id { get; set; }
    public long JobId { get; set;}
    public StudentJobStatusEnum JobStatus { get; set; }
    public StudentJobRatingEnum JobRating { get; set; }
}
