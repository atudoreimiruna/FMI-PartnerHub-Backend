using Licenta.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Licenta.Core.Entities;

public class StudentJob
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; }
    public long StudentId { get; set; }
    public virtual Student Student { get; set; }

    public long JobId { get; set; }
    public virtual Job Job { get; set; }
    public StudentJobStatusEnum JobStatus { get; set; }
    public StudentJobRatingEnum JobRating { get; set; }
}
