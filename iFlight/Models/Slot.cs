using System;
using System.Collections.Generic;

namespace iFlight.Models;

public partial class Slot
{
    public int FlightNumber { get; set; }

    public DateTime? FlightDate { get; set; }

    public decimal? StartHobbs { get; set; }

    public decimal? EndHobbs { get; set; }

    public decimal? Tach { get; set; }

    public TimeSpan? DepartTime { get; set; }

    public TimeSpan? LandingTime { get; set; }

    public byte? FuelAmount { get; set; }

    public byte? NumOfPassengers { get; set; }

    public string? DepartingStrip { get; set; }

    public string? LandingStrip { get; set; }

    public string? IntermediateLandingStrip { get; set; }

    public short? PilotLicenseNumber { get; set; }

    public short? InstructorLicenseNumber { get; set; }

    public string? RegistrationCode { get; set; }
    public decimal? TotalHobbs { get; set; }

    public virtual AirField? DepartingStripNavigation { get; set; }

    public virtual FlightInstructor? InstructorLicenseNumberNavigation { get; set; }

    public virtual AirField? IntermediateLandingStripNavigation { get; set; }

    public virtual AirField? LandingStripNavigation { get; set; }

    //public virtual Pilot? PilotLicenseNumberNavigation { get; set; }

    public virtual Plane? RegistrationCodeNavigation { get; set; }
}
