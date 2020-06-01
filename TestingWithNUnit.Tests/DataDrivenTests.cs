using System;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using RestfulBooker.UI.Data;
using RestfulBooker.UI.Pages;


namespace TestingWithNUnit.Tests
{
    [TestFixture(1)]
    
    public class DataDrivenTests:UITest
    {
        public DataDrivenTests(int customerId)
        {
            CustomerId = customerId;
        }

        public int CustomerId { get;}
        
        public AdminPage adminPage { get; set; }

        [SetUp]
        public void SetupTest()
        {
            
            adminPage = new AdminPage(driver);
            adminPage.Login();
        }

        
        [TearDown]
        public void CleanUpTests()
        {
            
            //TestContext.CurrentContext.Result is best used in teardown
            TestContext.WriteLine($"{TestContext.CurrentContext.Result.Outcome.Status}");
            
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed)
            {
                TestContext.WriteLine($"{TestContext.CurrentContext.Result.Outcome.Label}");
                TestContext.WriteLine($"{TestContext.CurrentContext.Result.Outcome.Site}");
            }

            adminPage.LogOut();
        }
        

        
        
        
        
        
        
        
        [TestCaseSource(typeof(TestData),nameof(TestData.RoomInfo))]
        public void AddRoom(
            string roomNumber, string Price,RoomType roomType)
        {
            var originalRoomsCount = adminPage.GetRooms().Count;
            var room = new Room()
            {
                Number = roomNumber,
                Type = roomType,
                Price = Price,
                Accessible = true,
                HasWifi = true,
                HasView = true
            };

            adminPage.AddRoom(room);

            var rooms = adminPage.GetRooms();
            var createdRoom = rooms.First(r => r.Number == room.Number);
            
            Assert.That(rooms.Count, Is.GreaterThan(originalRoomsCount));
            Assert.That(createdRoom.Price, Is.EqualTo(room.Price));
            Assert.That(createdRoom.Accessible, Is.EqualTo(room.Accessible));
        }
        
        
        
        
        
        
        
        
      
        
        [Test]
        public void AddRoomWithValueSource(
            [Values("9","999")]string roomNumber,
            [ValueSource(typeof(TestData),nameof(TestData.CurrencyStrings))] string Price,
            [Values]bool accessible,[Values] RoomType roomType)
        {
            var originalRoomsCount = adminPage.GetRooms().Count;
            var room = new Room()
            {
                Number = roomNumber,
                Type = RoomType.Family,
                Price = Price,
                Accessible = accessible,
                HasWifi = true,
                HasView = true
            };
            adminPage.AddRoom(room);

            var rooms = adminPage.GetRooms();
            var createdRoom = rooms.First(r => r.Number == room.Number);

            Assert.Multiple(() =>
            {
                Assert.That(rooms.Count, Is.GreaterThan(originalRoomsCount));
                Assert.That(createdRoom.Price, Is.EqualTo(room.Price));
                Assert.That(createdRoom.Accessible, Is.EqualTo(room.Accessible));
            });
        }

        
        
        
        
        
        [TestCaseSource(typeof(TestData),nameof(TestData.RoomsFromJsonFile))]
        public void AddRoomWithUsingRoomObject(Room room)
        {
            var originalRoomsCount = adminPage.GetRooms().Count;

            adminPage.AddRoom(room);
            
            
            // Current Contexts Test Information
            TestContext.WriteLine(TestContext.CurrentContext.Test.Name);

            
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

        
        
        //[TestCase("9","999",false,RoomType.Family,TestName = "")]
        //[TestCase("10","500",true,RoomType.Double,Description = "")]
        public void AddRoomWithTestCases(
            string roomNumber, string Price, bool accessible,RoomType roomType)
        {
            var originalRoomsCount = adminPage.GetRooms().Count;
            var room = new Room()
            {
                Number = roomNumber,
                Type = RoomType.Family,
                Price = Price,
                Accessible = accessible,
                HasWifi = true,
                HasView = true
            };

            adminPage.AddRoom(room);

            var rooms = adminPage.GetRooms();
            var createdRoom = rooms.First(r => r.Number == room.Number);
            
            Assert.That(rooms.Count, Is.GreaterThan(originalRoomsCount));
            Assert.That(createdRoom.Price, Is.EqualTo(room.Price));
            Assert.That(createdRoom.Accessible, Is.EqualTo(room.Accessible));
        }
        
        
        
        

        
        
        //[TestCase(2,2,ExpectedResult = 4)] 
        public int AddNumbersUsingExpectedResult(int number, int anotherNumber)
        {
            var answer = number + anotherNumber;
            return answer;
        }
        
        
        [Test]
        public void AddRoom(
            [Values("9","999")]string roomNumber,
            [Values("100","1000")] string Price,
            [Values]bool accessible,[Values] RoomType roomType)
        {
            var originalRoomsCount = adminPage.GetRooms().Count;
            
            var room = new Room()
            {
                Number = roomNumber,
                Type = RoomType.Family,
                Price = Price,
                Accessible = accessible,
                HasWifi = true,
                HasView = true
            };

            adminPage.AddRoom(room);

            var rooms = adminPage.GetRooms();
            var createdRoom = rooms.First(r => r.Number == room.Number);
            
            Assert.That(rooms.Count, Is.GreaterThan(originalRoomsCount));
            Assert.That(createdRoom.Price, Is.EqualTo(room.Price));
            Assert.That(createdRoom.Accessible, Is.EqualTo(room.Accessible));
        }

        //[Test]
        //[Pairwise]
        public void AddRoomWithValues(
            [Values("9","999")]string roomNumber,
            [Values("100","1000")] string Price,
            [Values]bool accessible,[Values] RoomType roomType)
        {
            var originalRoomsCount = adminPage.GetRooms().Count;
            var room = new Room()
            {
                Number = roomNumber,
                Type = RoomType.Family,
                Price = Price,
                Accessible = accessible,
                HasWifi = true,
                HasView = true
            };

            adminPage.AddRoom(room);

            var rooms = adminPage.GetRooms();
            var createdRoom = rooms.First(r => r.Number == room.Number);
            
            Assert.That(rooms.Count, Is.GreaterThan(originalRoomsCount));
            Assert.That(createdRoom.Price, Is.EqualTo(room.Price));
            Assert.That(createdRoom.Accessible, Is.EqualTo(room.Accessible));
        }

        
        //[Test]
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
        
        
        //[TestCaseSource(typeof(TestData),nameof(TestData.RoomData))]
        public void AddRoomUsingTestCaseSource(string roomNumber, string price, RoomType roomType)
        {
            var room = new Room()
            {
                Number = roomNumber,
                Type = roomType,
                Price = price,
                Accessible = true,
                HasWifi = true,
                HasView = true
            };
 
            var originalRoomsCount = adminPage.GetRooms().Count;
            
            adminPage.AddRoom(room);

            var rooms = adminPage.GetRooms();
            var createdRoom = rooms.First(r => r.Number == room.Number);

            Assert.Multiple(() =>
            {
                Assert.That(rooms.Count, Is.GreaterThan(originalRoomsCount));
                Assert.That(createdRoom.Price, Is.EqualTo(room.Price));
                Assert.That(createdRoom.Type, Is.EqualTo(room.Type));
            });
        }

    }
    
    
    public class SqrtTests
    {
        [DatapointSource]
        public double[] values = new double[] { 0.0, 1.0, -1.0, 42.0 };

        [Theory]
        public void SquareRootDefinition(double num)
        {
            Assume.That(num >= 0.0);

            double sqrt = Math.Sqrt(num);

            Assert.That(sqrt >= 0.0);
            Assert.That(sqrt * sqrt, 
                Is.EqualTo(num).Within(0.000001));
        }
    }
}
