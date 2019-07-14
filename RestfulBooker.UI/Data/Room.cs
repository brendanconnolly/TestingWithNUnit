namespace RestfulBooker.UI.Data
{
    public enum RoomType
    {
        Single,
        Twin,
        Double,
        Family,
        Suite
    }
    public class Room
    {
        public string Number { get; set; }

        public RoomType Type { get; set; }

        public bool Accessible { get; set; }

        public string Price { get; set; }

        public bool HasWifi { get; set; }

        public bool HasRefreshments { get; set; }

        public bool HasTelevision { get; set; }

        public bool HasSafe { get; set; }

        public bool HasRadio { get; set; }

        public bool HasView { get; set; }
        
    }
}