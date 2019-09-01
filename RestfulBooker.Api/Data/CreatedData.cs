namespace RestfulBooker.Api.Data
{
    public class CreatedBooking
    {
        public int BookingId { get; set; }

        public Booking Booking { get; set; }
    }
}