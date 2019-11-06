using System.Collections.Generic;


namespace ScreenshotService.RequestModels
{
    public class TakeSceenshotsPostRequest
    {
        public List<ScreenshotPostRequest> ScreenshotRequests { get; set; }
    }

    public class ScreenshotPostRequest
    {
        public string FileName { get; set; }
        public string URL { get; set; }
    }
}
