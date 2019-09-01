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
    public class AdminTests
    {

        [Test]
        public void AddRoom()
        {
            string driverPath = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location);
            IWebDriver driver = new ChromeDriver(driverPath);


            AdminPage adminPage = new AdminPage(driver);
            adminPage.Login();
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
            driver.Quit();

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


    }
}