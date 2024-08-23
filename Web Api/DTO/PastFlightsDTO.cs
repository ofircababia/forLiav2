namespace Web_Api.DTO
{
    public class PastFlightsDTO
    {
        public DateTime flightDate { get; set; }
        public string DepartingStrip { get; set; }
        public string LandingStrip { get; set; }
        public string IntermediateLandingStrip { get; set; }
        public string PlaneCode { get; set; }
        public decimal? TotalHobbs { get; set; }
    }
}
