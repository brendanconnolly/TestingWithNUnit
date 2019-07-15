using System;
using System.Dynamic;
using RestSharp;
using RestSharp.Authenticators;

namespace RestfulBooker.Api
{
    public class  RestfulBookerClient
    {
        private readonly BookingsService _bookings;

        public RestfulBookerClient(string username, string password, string baseUrl="https://restful-booker.herokuapp.com")
        {
            var client = new RestClient(baseUrl) {Authenticator = new HttpBasicAuthenticator(username, password)};
            client.AddDefaultHeader("Accept", "application/json");
            _bookings= new BookingsService(client);
        }

        public BookingsService Bookings => _bookings;
    }
}