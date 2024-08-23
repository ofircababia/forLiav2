using Check.Models;
using System;
using System.Collections.Generic;

namespace iFlight.Models;

public partial class Pilot
{
    public short LicenseNumber { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? Dob { get; set; }

    public string? PhoneNumber { get; set; }

    public string? ProfilePicture { get; set; }

    public DateTime? MedicalExpiry { get; set; }

    public DateTime? MivhanRama { get; set; }

    public string? Idimage { get; set; }

    public string? LicenseImage { get; set; }

    public string? MedicalImage { get; set; }

    public string? MivhanRamaImage { get; set; }

    public string? LogbookImage { get; set; }

    public string? PilotStatus { get; set; }

    public decimal? HoursToUse { get; set; }

    public string? LicenseType { get; set; }

    public string? TypeRating { get; set; }

    public bool? IsInterestedToBeInList { get; set; }
    public int? ID { get; set; }
    int CalculateAge()
    {
        if (Dob.HasValue)
        {
            int age = DateTime.Now.Year - Dob.Value.Year;
            if (DateTime.Today.Date > DateTime.Today.AddYears(-age)) age--;
            return age;

        }
        else
        {
            return -1;
        }
    }

    public int? Age1 { get { return CalculateAge(); } set { } }

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual FlightInstructor? FlightInstructor { get; set; }

    public virtual ICollection<InterestedInFlightType> InterestedInFlightTypes { get; set; } = new List<InterestedInFlightType>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Slot> Slots { get; set; } = new List<Slot>();
}
