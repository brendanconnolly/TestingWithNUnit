using System.Collections.Generic;
using RestfulBooker.UI.Data;

namespace TestingWithNUnit.Tests
{
    
    public class ExternalRoomData
    {
        public Room RoomData { get; set; }
        public string TestName { get; set; }
        public string Description { get; set; }
        public List<string> Categories { get; set; } = new List<string>();
        public bool IsExplicit { get; set; } = false;
        public bool IsIgnored { get; set; } = false;
        public string IgnoreReason { get; set; }
    }
}