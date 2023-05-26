﻿using Licenta.Services.DTOs.Job;
using System.Collections.Generic;

namespace Licenta.Services.DTOs.Student;

public class StudentViewDTO
{
    public long Id { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string PersonalEmail { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Degree { get; set; }
    public string Skill { get; set; }
    public string Description { get; set; }
    public List<string> FileNames { get; set; }
    public List<JobViewDTO> Jobs { get; set; }

}
