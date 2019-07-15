using System;

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

        public int BookingId { get; set; }

        public int RoomId { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int TotalPrice { get; set; }

        public bool DepositPaid { get; set; }

        public BookingDates BookingDates { get; set; }

        public string AdditionalNeeds { get; set; }
    }

    public class BookingDates
    {
        public DateTime checkin { get; set; }
        
        public DateTime checkout { get; set; }
    }
}