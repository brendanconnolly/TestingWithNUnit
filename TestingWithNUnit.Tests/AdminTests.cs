using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestfulBooker.UI.Data;
using RestfulBooker.UI.Pages;

namespace TestingWithNUnit.Tests
{


    public class AdminTests:UITest
    {
        private AdminPage adminPage { get; set; }

        [SetUp]
        public void SetupTest()
        {
            
            adminPage = new AdminPage(driver);
            adminPage.Login();
        }

        
        [TearDown]
        public void CleanUpTests()
        {
            adminPage.LogOut();
        }

        [Test]
        [Sequential]
        public void AddRoom([Values("9","999")] string roomNumber,
            [Values("100","1000")]string price,
            [Values] bool accessible, 
            [Values] RoomType roomType)
        {
            var originalRoomsCount = adminPage.GetRooms().Count;
            var room = new Room()
            {
                Number = "9",
                Type = RoomType.Family,
                Price = "88",
                Accessible = true,
                HasWifi = true,
                HasView = true
            };

            adminPage.AddRoom(room);

            var rooms = adminPage.GetRooms();
            var createdRoom = rooms.First(r => r.Number == room.Number);
            
            Assert.That(rooms.Count, Is.GreaterThan(originalRoomsCount));
            Assert.That(createdRoom.Price, Is.EqualTo(createdRoom.Price));
            Assert.That(createdRoom.Accessible, Is.True);
            Assert.That(createdRoom.HasWifi, Is.True);
            Assert.That(createdRoom.HasView, Is.True);
            Assert.That(createdRoom.HasRadio, Is.False);
            Assert.That(createdRoom.HasRefreshments, Is.False);
            Assert.That(createdRoom.HasSafe, Is.False);
            Assert.That(createdRoom.HasTelevision, Is.False);
        }
        
        [Test]
        public void AddSuite(string roomNumber,string price,bool accessible, 
            RoomType roomType)
        {
            
            var originalRoomsCount = adminPage.GetRooms().Count;


            var room = new Room()
            {
                Number = "100",
                Type = RoomType.Suite,
                Price = "500",
                Accessible = true,
                HasWifi = true,
                HasView = true
            };

            adminPage.AddRoom(room);

            var rooms = adminPage.GetRooms();
            var createdRoom = rooms.First(r => r.Number == room.Number);

            Assert.That(rooms.Count, Is.GreaterThan(originalRoomsCount));
            Assert.That(createdRoom.Price, Is.EqualTo(room.Price));
            Assert.That(createdRoom.Accessible, Is.True);
            Assert.That(createdRoom.HasWifi, Is.True);
            Assert.That(createdRoom.HasView, Is.True);
            Assert.That(createdRoom.HasRadio, Is.False);
            Assert.That(createdRoom.HasRefreshments, Is.False);
            Assert.That(createdRoom.HasSafe, Is.False);
            Assert.That(createdRoom.HasTelevision, Is.False);
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
            
            var originalRoomsCount = adminPage.GetRooms().Count;
            
            adminPage.AddRoom(room);

            var rooms = adminPage.GetRooms();
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