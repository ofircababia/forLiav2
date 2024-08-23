namespace Web_Api.DTO
{
    public class FutureSlotDTO
    {
        public DateTime? Flightdate { get; set; }
        public TimeSpan? Departingtime { get; set; }
        public TimeSpan? Landingtime { get; set; }
    }
}
