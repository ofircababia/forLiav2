using System;
using System.Collections.Generic;

namespace iFlight.Models;

public partial class AirField
{
    public string Icao { get; set; } = null!;

    public string? FieldName { get; set; }
    public decimal? Longitude { get; set; }
    public decimal? Latitude { get; set; }

    public virtual ICollection<Slot> SlotDepartingStripNavigations { get; set; } = new List<Slot>();

    public virtual ICollection<Slot> SlotIntermediateLandingStripNavigations { get; set; } = new List<Slot>();

    public virtual ICollection<Slot> SlotLandingStripNavigations { get; set; } = new List<Slot>();
}
