using iFlight.Models;

namespace Web_Api.DTO
{
    public class InterestedPilotsDTO
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string PhoneNum { get; set; }
        public int Age { get; set; }
        public ICollection<InterestedInFlightType> InterestedInFlightTypes { get; set; } = new List<InterestedInFlightType>();

    }
}
