using System.Linq;
using DataAccess.Models;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public class ScreenshotRepository : IScreenshotRepository
    {
        private ScreenshotServiceContext _dbContext;

        public ScreenshotRepository(IConfiguration configArg)
        {
            _dbContext = new ScreenshotServiceContext(configArg["ConnectionString"]);
        }

        public void CreateScreenshot(string fileNameArg, byte[] imageArg, string urlArg)
        {
            var newScreenshot = new Screenshot()
            {
                FileName = fileNameArg,
                Image = imageArg,
                Url = urlArg
            };

            _dbContext.Screenshot.Add(newScreenshot);
            _dbContext.SaveChanges();
        }

        public byte[] GetScreenshot(string fileNameArg)
        {
            var screenshot = _dbContext.Screenshot.Where(s => s.FileName == fileNameArg);
            if(screenshot.Count() == 0)
            {
                return null;
            }

            return screenshot.First().Image;
        }
    }
}
