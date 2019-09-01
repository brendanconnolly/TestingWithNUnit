using System.Collections.Generic;
using Newtonsoft.Json;

namespace RestfulBooker.Api.Data
{
    public class BookingGroup
    {
        [JsonProperty("bookings")]
        public List<Booking> Bookings { get; set; }
    }
}