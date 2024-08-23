using System;
using System.Collections.Generic;

namespace iFlight.Models;

public partial class Plane
{
    public string RegistrationCode { get; set; } = null!;

    public short? MaxLoad { get; set; }

    public virtual ICollection<Slot> Slots { get; set; } = new List<Slot>();
}
