using System.Linq;
using System.Runtime.Serialization;
using NUnit.Framework;
using RestfulBooker.UI.Data;
using RestfulBooker.UI.Pages;

namespace TestingWithNUnit.Tests
{


    public class BookerAdminTests:UITest
    {
        private AdminPage _adminPage { get; set; }

        [SetUp]
        public void SetupTest()
        {
            
            _adminPage = new AdminPage(driver);
            _adminPage.Login();
        }

        
        [TearDown]
        public void CleanUpTests()
        {
            _adminPage.LogOut();
        }

        [Test]
        [Pairwise]
        public void AddRoom([Values("9","999")] string roomNumber,
            [Values("100","999")]string price,
            [Values] bool accessible, 
            [Values] RoomType roomType)
        {
            var originalRoomsCount = _adminPage.GetRooms().Count;
            var room = new Room()
            {
                Number = roomNumber,
                Type = roomType,
                Price = price,
                Accessible = accessible,
                HasWifi = true,
                HasView = true
            };

            _adminPage.AddRoom(room);

            var rooms = _adminPage.GetRooms();
            var createdRoom = rooms.First(r => r.Number == room.Number);
            
            Assert.That(rooms.Count, Is.GreaterThan(originalRoomsCount));
            Assert.That(createdRoom.Price, Is.EqualTo(createdRoom.Price));
            Assert.That(createdRoom.Accessible, Is.EqualTo(room.Accessible));
            Assert.That(createdRoom.HasWifi, Is.EqualTo(room.HasWifi));
            Assert.That(createdRoom.HasView, Is.EqualTo(room.HasView));
        }
        
        [Test]
        public void AddSuite()
        {
            
            var originalRoomsCount = _adminPage.GetRooms().Count;


            var room = new Room()
            {
                Number = "100",
                Type = RoomType.Suite,
                Price = "500",
                Accessible = true,
                HasWifi = true,
                HasView = true
            };

            _adminPage.AddRoom(room);

            var rooms = _adminPage.GetRooms();
            var createdRoom = rooms.First(r => r.Number == room.Number);

            Assert.That(rooms.Count, Is.GreaterThan(originalRoomsCount));
            Assert.That(createdRoom.Price, Is.EqualTo(room.Price));
            Assert.That(createdRoom.Accessible, Is.EqualTo(room.Accessible));
            Assert.That(createdRoom.HasWifi, Is.EqualTo(room.HasWifi));
            Assert.That(createdRoom.HasView, Is.EqualTo(room.HasView));
        }

        public static Room[] RoomData()
        {
            return new Room[]{new Room()
            {
                Number = "1",
                Price = "800",
                Type= RoomType.Twin,
                Accessible = false
            }};
        }
        [Test]
        public void AddRoomUsingValueSource([ValueSource("RoomData")] Room room)
        {
            
            var originalRoomsCount = _adminPage.GetRooms().Count;
            
            _adminPage.AddRoom(room);

            var rooms = _adminPage.GetRooms();
            var createdRoom = rooms.First(r => r.Number == room.Number);

            Assert.Multiple(() =>
            {
                Assert.That(rooms.Count, Is.GreaterThan(originalRoomsCount));
                Assert.That(createdRoom.Price, Is.EqualTo(room.Price));
                Assert.That(createdRoom.Accessible, Is.EqualTo(room.Accessible));
                Assert.That(createdRoom.HasWifi, Is.EqualTo(room.HasWifi));
                Assert.That(createdRoom.HasView, Is.EqualTo(room.HasView));
                Assert.That(createdRoom.HasRadio, Is.EqualTo(room.HasRadio));
                Assert.That(createdRoom.HasRefreshments, Is.EqualTo(room.HasRefreshments));
                Assert.That(createdRoom.HasSafe, Is.EqualTo(room.HasSafe));
                Assert.That(createdRoom.HasTelevision, Is.EqualTo(room.HasTelevision));
            });
        }
    }
}