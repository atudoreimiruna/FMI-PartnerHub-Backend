using System;

namespace Licenta.Services.QueryParameters;

public class EventParameters : BaseParameters
{
    public string Title { get; set; }
    public string Type { get; set; }
    public string Location { get; set; }
    public string Time { get; set; }
    public string PartnerName { get; set; }
    public DateTime LastUpdated { get; set; }
}
