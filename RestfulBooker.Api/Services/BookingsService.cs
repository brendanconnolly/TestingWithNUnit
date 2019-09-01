using System;
using System.Collections.Generic;
using RestfulBooker.Api.Data;
using RestfulBooker.Api.Services;
using RestSharp;
using RestSharp.Serialization.Json;
using RestSharp.Serializers;

namespace RestfulBooker.Api.Services
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

        public CreatedBooking CreateBooking(Booking booking)
        {
            var request = new RestRequest(ServiceEndPoint, Method.POST);
            request.AddJsonBody(booking);
            var response = _client.Execute<CreatedBooking>(request);
            return response.Data;
        }
    }
}