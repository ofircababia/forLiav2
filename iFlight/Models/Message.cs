using iFlight.Models;
using System;
using System.Collections.Generic;

namespace Check.Models;

public partial class Message
{
    public int MessageNumber { get; set; }

    public string? MessageText { get; set; }

    public short? LicenseNumber { get; set; }

    public byte? StartHour { get; set; }

    public byte? StartMinute { get; set; }

    public byte? EndHour { get; set; }

    public byte? EndMinute { get; set; }
    public DateTime FlightDate { get; set; }

    public int? Day { get; set; }
    public int? Month { get; set; }
    public int? Year { get; set; }

    public virtual Pilot? LicenseNumberNavigation { get; set; }
}
