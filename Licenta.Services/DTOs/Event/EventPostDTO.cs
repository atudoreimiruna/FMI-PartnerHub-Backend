using System.Collections.Generic;
using System;

namespace Licenta.Services.DTOs.Event;

public class EventPostDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public string Location { get; set; }
    public DateTime Date { get; set; }
    public string Time { get; set; }
    public long PartnerId { get; set; }
}
