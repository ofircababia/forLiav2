using System;
using System.Collections.Generic;

namespace iFlight.Models;

public partial class FlightInstructor
{
    public short LicenseNumber { get; set; }

    public short? InstructorLicenseNumber { get; set; }

    public virtual Pilot LicenseNumberNavigation { get; set; } = null!;

    public virtual ICollection<Slot> Slots { get; set; } = new List<Slot>();
}
