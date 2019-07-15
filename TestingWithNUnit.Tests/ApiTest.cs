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
            
            Booking response = bookingsApi.Bookings.GetBooking(1);
            Assert.That(response, Is.Not.Null);
        }
    }
}