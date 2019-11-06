using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class ScreenshotInMemoryRepository : IScreenshotRepository
    {
        private List<Screenshot> _screenshots;

        public ScreenshotInMemoryRepository()
        {
            _screenshots = new List<Screenshot>();
        }

        public void CreateScreenshot(string fileNameArg, byte[] imageArg, string urlArg)
        {
            var newScreenshot = new Screenshot()
            {
                FileName = fileNameArg,
                Image = imageArg,
                Url = urlArg,
                Cerated = DateTime.Now
            };

            _screenshots.Add(newScreenshot);
        }

        public byte[] GetScreenshot(string fileNameArg)
        {
            var screenshot = _screenshots.Where(s => s.FileName == fileNameArg);

            if (screenshot.Count() == 0)
            {
                return null;
            }

            return screenshot.First().Image;
        }
    }
}
