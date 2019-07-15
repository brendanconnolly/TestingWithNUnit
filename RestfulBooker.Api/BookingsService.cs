using System;
using System.Collections.Generic;
using RestfulBooker.Api.Data;
using RestSharp;

namespace RestfulBooker.Api
{
    public class BookingsService
    {
        private IRestClient _client;
        private const string ServiceEndPoint="booking/";
        
        public BookingsService(IRestClient client)
        {
            _client = client;
        }

        public string GetBookingIds()
        {
            var request= new RestRequest(ServiceEndPoint);
            //request.AddParameter("roomid", "0 OR 1=1", ParameterType.QueryString);

            var response = _client.Get<BookingGroup>(request);
            return response.Content;
        }
        
        public List<Booking> GetBookingsByRoom(int roomId)
        {
            var request= new RestRequest(ServiceEndPoint);
            request.AddParameter("roomid", roomId, ParameterType.QueryString);
                
            var response = _client.Get<BookingGroup>(request);
            return response.Data.Bookings;
        }
        
        public Booking GetBooking(int id)
        { 
            var path = $"{ServiceEndPoint}/{id}";
            var request= new RestRequest(path);
            var response = _client.Get<Booking>(request);
            return response.Data;

        }
    }
}