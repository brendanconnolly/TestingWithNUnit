using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace Tests
{
    public class Tests
    {
        private IWebDriver _driver;

        [TearDown]
        public void TearDown() => _driver.Dispose();


        [SetUp]
        public void Setup()
        {
            string driverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _driver = new ChromeDriver(driverPath);
        }

        [Test]
        public void Test1()
        {
            _driver.Url = "http://google.com";
            var searchBox = _driver.FindElement(By.Name("q"));
            searchBox.SendKeys("toast");
            searchBox.Submit();

        }
    }
}