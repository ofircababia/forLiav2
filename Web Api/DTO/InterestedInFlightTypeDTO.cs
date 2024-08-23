namespace Web_Api.DTO
{
    public class InterestedInFlightTypeDto
    {
        public string FlightTypeName { get; set; } = null!;
        public short LicenseNumber { get; set; }
        public bool? Nothing { get; set; }
        public FlightTypeDto FlightTypeNavigation { get; set; } = null!;

    }
}
