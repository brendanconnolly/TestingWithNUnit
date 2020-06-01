using System.IO;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestingWithNUnit.Tests
{

    public class UITest
    {
        public IWebDriver driver { get; set; }
        
        [OneTimeSetUp]
        public void StartDriver()
        {
            string driverPath = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location);

            driver = new ChromeDriver(driverPath);
        }
        
        [OneTimeTearDown]
        public void QuitDriver()
        {
            driver.Quit();
        }
    }
}