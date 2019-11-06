using System.Collections.Generic;

namespace ScreenshotService.RequestModels
{
    public class TakeSceenshotsPostResponse
    {
        public string Status { get; set; }
        public List<string> FailedURLs { get; set; }
    }
}
