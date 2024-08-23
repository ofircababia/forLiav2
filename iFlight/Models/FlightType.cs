using System;
using System.Collections.Generic;

namespace iFlight.Models;

public partial class FlightType
{
    public string FlightType1 { get; set; } = null!;

    public virtual ICollection<InterestedInFlightType> InterestedInFlightTypes { get; set; } = new List<InterestedInFlightType>();
}
