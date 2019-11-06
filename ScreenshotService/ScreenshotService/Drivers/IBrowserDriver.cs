using OpenQA.Selenium;

namespace ScreenshotService.Drivers
{
    public interface IBrowserDriver
    {
        Screenshot TakeScreenshot(string urlArg, string fileNameArg);
    }
}
