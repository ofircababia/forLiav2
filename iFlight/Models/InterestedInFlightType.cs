using System;
using System.Collections.Generic;

namespace iFlight.Models;

public partial class InterestedInFlightType
{
    public string FlightType { get; set; } = null!;

    public short LicenseNumber { get; set; }

    public bool? Nothing { get; set; }

    public virtual FlightType FlightTypeNavigation { get; set; } = null!;

    public virtual Pilot LicenseNumberNavigation { get; set; } = null!;

}
