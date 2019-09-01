using System;
using NUnit.Framework;
using RestfulBooker.Api;
using RestfulBooker.Api.Data;

namespace TestingWithNUnit.Tests
{
    public class ApiTest
    {
        [Test]
        public void GetBookingsTest()
        {
            var bookingsApi = new RestfulBookerClient("admin", "password", "https://automationintesting.online");

            var response = bookingsApi.Bookings.GetBooking(1);
            Assert.That(response, Is.Not.Null);
        }

        [Test]
        public void CreateBooking()
        {
            var bookingsApi = new RestfulBookerClient("admin", "password", "https://automationintesting.online");
            var booking = new Booking()
            {
                roomid = 1,
                firstname = "Johnny",
                lastname = "Five",
                totalprice = 123,
                bookingdates = new BookingDates(DateTime.Today, DateTime.Today.AddDays(3)),
                additionalneeds = "Sound Proof Walls"
            };

            var response = bookingsApi.Bookings.CreateBooking(booking);
            Assert.That(response, Is.Not.Null);
        }
    }
}