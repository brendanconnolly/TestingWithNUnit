using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RestfulBooker.UI.Data;

namespace RestfulBooker.UI.Pages
{
    public class AdminPage
    {
        private readonly IWebDriver _driver;
        private readonly string adminUrl = "https://automationintesting.online/#/admin";

        private By _logoutButtonSelector=By.CssSelector("a[class='nav-link'][href$='admin']");
        public AdminPage(IWebDriver driver)
        {
            _driver = driver;
            _driver.Navigate().GoToUrl(adminUrl);
            HandleWelcomeMessage();

        }

        public void HandleWelcomeMessage()
        {
            By nextButtonSelector = By.Id("next");
            By closeButtonSelector = By.Id("closeModal");
            
            var buttonElements = _driver.FindElements(nextButtonSelector).ToList();
            buttonElements.AddRange(_driver.FindElements(closeButtonSelector).ToList());


            if (buttonElements.Count > 0)
            {
                buttonElements.ForEach(e=>e.Click());
                HandleWelcomeMessage();
            }
        }
        public void Login(string username = "admin", string password = "password")
        {
            By usernameSelector=By.CssSelector("[data-testid='username']"); 
            By passwordSelector=By.CssSelector("[data-testid='password']");
            By loginButtonSelector=By.CssSelector("[data-testid='submit']");

            var logoutVisible = _driver.FindElements(_logoutButtonSelector).Count > 0;

            if (!logoutVisible)
            {
                WebDriverWait webdriverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                var usernameElement = webdriverWait.Until(x => x.FindElement(usernameSelector));

                usernameElement.SendKeys(username);
                _driver.FindElement(passwordSelector).SendKeys(password);
                _driver.FindElement(loginButtonSelector).Click();
            }

        }

        public void LogOut()
        {
            //the logout button doesn't seem to be working '
            By logoutButtonSelector=By.CssSelector("a[href$='admin']");
            
            WebDriverWait webdriverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            var logoutButtonElement= webdriverWait.Until(x => x.FindElement(logoutButtonSelector));
            logoutButtonElement.Click();
        }

        public void AddRoom(Room room)
        {
            By roomNumberSelector = By.Id("roomNumber");
            By roomTypeSelector = By.Id("type");
            By roomAccessibleSelector = By.Id("accessible");
            By roomPriceSelector = By.Id("roomPrice");
            By createButtonSelector = By.Id("createRoom");
            
            By wifiSelector = By.Id("wifiCheckbox");
            By tvSelector = By.Id("tvCheckbox");
            By radioSelector = By.Id("radioCheckbox");
            By refreshmentsSelector = By.Id("refreshCheckbox");
            By safeSelector = By.Id("safeCheckbox");
            By viewsSelector = By.Id("viewsCheckbox");
            
            By roomRowsSelector = By.CssSelector("[data-type='room']");
            
            var roomRowCount = _driver.FindElements(roomRowsSelector).Count;
            
            _driver.FindElement(roomNumberSelector,10).SendKeys(room.Number);
            
            var roomTypeElement = _driver.FindElement(roomTypeSelector);
            new SelectElement(roomTypeElement).SelectByText(room.Type.ToString());
            
            var roomAccessibleElement = _driver.FindElement(roomAccessibleSelector);
            new SelectElement(roomAccessibleElement).SelectByText(room.Accessible.ToString().ToLowerInvariant());
            
            _driver.FindElement(roomPriceSelector).SendKeys(room.Price ?? "");
            
            _driver.FindElement(wifiSelector).SetCheckBox(room.HasWifi);
            _driver.FindElement(tvSelector).SetCheckBox(room.HasTelevision);
            _driver.FindElement(radioSelector).SetCheckBox(room.HasRadio);
            _driver.FindElement(refreshmentsSelector).SetCheckBox(room.HasRefreshments);
            _driver.FindElement(safeSelector).SetCheckBox(room.HasSafe);
            _driver.FindElement(viewsSelector).SetCheckBox(room.HasView);
            
            var createButtonElement=_driver.FindElement(createButtonSelector);
            createButtonElement.Submit();
            
            _driver.WaitFor(drv=>drv.FindElements(roomRowsSelector).Count>roomRowCount,10);
        }

        public IReadOnlyList<Room> GetRooms()
        {
            var roomsListSelector = By.CssSelector("[data-type='room']");
            _driver.WaitFor(drv=>drv.FindElement(roomsListSelector).Displayed==true);
            
            var roomRowElements = _driver.FindElements(roomsListSelector).ToList();
            List<Room> rooms = roomRowElements.Select(RowElementToRoom).ToList();

            return rooms.AsReadOnly();
        }

        private List<string> ParseRoomDetailsString(string roomDetails)
        {
            var trimmed = roomDetails.TrimStart("details".ToCharArray());
            var parsed = trimmed.Split(',', StringSplitOptions.RemoveEmptyEntries);
            
            return parsed.Select(x => x.ToLower().Trim()).ToList();
        }

        private Room RowElementToRoom(IWebElement element)
        {
            By roomNumberSelector = By.CssSelector("[id^='roomNumber']");
            By roomTypeSelector = By.CssSelector("[id^='type']");
            By roomPriceSelector = By.CssSelector("[id^='roomPrice']");
            By roomAccessibleSelector = By.CssSelector("[id^='accessible']");
            By roomDetailsSelector = By.CssSelector("[id^='details']");
            

            var roomNumber = element.FindElement(roomNumberSelector).Text;
            
            var roomTypeString = element.FindElement(roomTypeSelector).Text;
            var roomType = Enum.Parse<RoomType>(roomTypeString);
            
            var roomAccessibleString = element.FindElement(roomAccessibleSelector).Text;
            var roomAccessible = bool.Parse(roomAccessibleString);
            
            var roomPrice = element.FindElement(roomPriceSelector).Text;
            
            var roomDetailsString=  element.FindElement(roomDetailsSelector).Text;
            var roomDetailsList = ParseRoomDetailsString(roomDetailsString);
            
            return new Room()
            {
                Number = roomNumber,
                Type = roomType,
                Price = roomPrice,
                Accessible = roomAccessible,
                HasWifi = roomDetailsList.Contains("wifi"),
                HasRadio = roomDetailsList.Contains("radio"),
                HasRefreshments = roomDetailsList.Contains("refreshments"),
                HasSafe = roomDetailsList.Contains("safe"),
                HasTelevision = roomDetailsList.Contains("tv"),
                HasView = roomDetailsList.Contains("views")
            };

        }
        
    }
}