using System;
using Newtonsoft.Json;

namespace RestfulBooker.Api.Data
{
    public class Booking
    {
        //        {
        //            "firstname": "Sally",
        //            "lastname": "Brown",
        //            "totalprice": 111,
        //            "depositpaid": true,
        //            "bookingdates": {
        //                "checkin": "2013-02-23",
        //                "checkout": "2014-10-23"
        //            },
        //            "additionalneeds": "Breakfast"
        //        }
        public Booking()
        {
            roomid = 0;
            totalprice = 0;    
        }
        public int BookingId { get; set; }

        [JsonProperty("roomid")]
        public int roomid { get; set; }

        [JsonProperty("firstname")]
        public string firstname { get; set; }

        [JsonProperty("lastname")]
        public string lastname { get; set; }

        [JsonProperty("totalprice")]
        public int totalprice { get; set; }

        [JsonProperty("depositpaid")]
        public bool depositpaid { get; set; }

        [JsonProperty("bookingdates")]
        public BookingDates bookingdates { get; set; }

        [JsonProperty("additionaldates")]
        public string additionalneeds { get; set; }
    }

    public class BookingDates
    {
        public BookingDates()
        {

        }
        public BookingDates(DateTime checkIn, DateTime checkOut)
        {
            this.checkin = checkIn.ToString("yyyy-MM-dd");
            this.checkout = checkOut.ToString("yyyy-MM-dd");
        }
        public string checkin { get; set; }

        public string checkout { get; set; }
    }
}