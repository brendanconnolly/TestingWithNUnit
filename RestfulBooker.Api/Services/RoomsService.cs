using RestSharp;

namespace RestfulBooker.Api.Services
{
    public class RoomsService
    {
        private IRestClient _client;
        private const string ServiceEndPoint="room/";
        
        public RoomsService(IRestClient client)
        {
            _client = client;
        }

    }
}