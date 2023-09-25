namespace Licenta.Services.DTOs.Student;

public class StudentPutDTO
{
    public long Id { get; set; }
    public string PersonalEmail { get; set; }
    public string Phone { get; set; }
    public string Degree { get; set; }
    public string Skill { get; set; }
    public string Description { get; set; }
}
