using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace RestfulBooker.UI
{
    public static class WebDriverExtensions
    {
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds=5)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }
        
        public static void SetCheckBox(this IWebElement element, bool value)
        {
            if (element.Selected != value)
            {
                element.Click();
            }
        }
        
        public static void WaitFor(this IWebDriver driver, Func<IWebDriver,bool> condition, int timeoutInSeconds=5)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                var result=wait.Until(condition.Invoke);
            }
        }
        
        
    }
}