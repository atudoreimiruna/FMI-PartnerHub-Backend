using Licenta.Core.Enums;

namespace Licenta.Services.DTOs.Student;

public class StudentJobViewDTO
{
    public long StudentId { get; set; }
    public long JobId { get; set; }
    public StudentJobStatusEnum JobStatus { get; set; }
    public StudentJobRatingEnum JobRating { get; set; }
}
