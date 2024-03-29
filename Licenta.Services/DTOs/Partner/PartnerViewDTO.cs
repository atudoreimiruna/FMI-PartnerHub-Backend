﻿using Licenta.Services.DTOs.Base;
using Licenta.Services.DTOs.Job;
using System.Collections.Generic;

namespace Licenta.Services.DTOs.Partner;

public class PartnerViewDTO : BaseDto
{
    public string Name { get; set; }
    public string MainDescription { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Social { get; set; }
    public string MainImageUrl { get; set; }
    public string LogoImageUrl { get; set; }
    public string ProfileImageUrl { get; set; }
    public List<JobViewDTO> Jobs { get; set; }
}
