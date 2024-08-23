using iFlight.Models;

namespace Web_Api.DTO
{
    public class PilotDTO
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
        public int? Age1 { get; set; }
        public List<InterestedInFlightTypeDto> InterestedInFlightTypes { get; set; } = new List<InterestedInFlightTypeDto>();
    }
}
