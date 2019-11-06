using DataAccess.Models;

namespace DataAccess
{
    public interface IScreenshotRepository
    {
        byte[] GetScreenshot(string fileNameArg);
        void CreateScreenshot(string fileNameArg, byte[] imageArg, string urlArg);
    }
}
