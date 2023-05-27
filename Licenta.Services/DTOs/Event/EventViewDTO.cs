using Licenta.Services.DTOs.Base;
using System.Collections.Generic;
using System;
using Licenta.Core.Entities;
using Licenta.Services.DTOs.Partner;

namespace Licenta.Services.DTOs.Event;

public class EventViewDTO : BaseDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public string Location { get; set; }
    public DateTime Date { get; set; }
    public string Time { get; set; }
    public PartnerViewDTO Partner { get; set; }
    public List<File> Files { get; set; } 
}
