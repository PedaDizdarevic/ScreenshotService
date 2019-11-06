using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;

namespace ScreenshotService.Drivers
{
    public class BrowserDriver : IBrowserDriver, IDisposable
    {
        private RemoteWebDriver _webDriver;

        public BrowserDriver()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            _webDriver = new ChromeDriver(chromeOptions);
        }

        public Screenshot TakeScreenshot(string urlArg, string fileNameArg) 
        {
            _webDriver.Navigate().GoToUrl(urlArg);
            return (_webDriver as ITakesScreenshot).GetScreenshot();
        }

        public void Dispose()
        {
            _webDriver.Close();
            _webDriver.Quit();
        }
    }
}
