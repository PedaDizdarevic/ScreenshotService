using DataAccess;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using ScreenshotService.Drivers;
using ScreenshotService.RequestModels;
using System;
using System.Collections.Generic;

namespace ScreenshotService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScreenshotController : ControllerBase
    {
        private IBrowserDriver _browserDriver;
        private IScreenshotRepository _screenshotRepository;
   
        public ScreenshotController(IBrowserDriver driverArg, IScreenshotRepository repositoryArg)
        {
            _browserDriver = driverArg;
            _screenshotRepository = repositoryArg;
        }

        /// <summary>
        /// Get a file object containing the image.
        /// </summary>
        /// <param name="fileNameArg">Filename given when the screnshot was taken.</param>
        [HttpGet("{fileNameArg}")]
        public IActionResult Get(string fileNameArg)
        {
            try
            {
                var image = _screenshotRepository.GetScreenshot(fileNameArg);

                if (image == null)
                {
                    return StatusCode(204);
                }

                return Ok(File(image, "image/x-png"));
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to retrieve screenshot.");
            }

        }

        /// <summary>
        /// Take screenshots for the given URLs.
        /// </summary>
        /// <param name="requestsArg">An object containing a list of screenshot request objects. 
        /// Each screenshot request object contains an url from where the screenshot is to be taken
        /// and a filename with which it is to be saved.</param>
        [HttpPost]
        public IActionResult Post([FromBody] TakeSceenshotsPostRequest requestsArg)
        {
            List<string> failedURLs = new List<string>(); 

            foreach(var screenshotRequest in requestsArg.ScreenshotRequests) 
            {
                var formatedUrl = UrlHelper.FormatUrl(screenshotRequest.URL);

                if (UrlHelper.ValidateUrl(formatedUrl) == "")
                {
                    try
                    {
                        var screenshot = _browserDriver.TakeScreenshot(formatedUrl, screenshotRequest.FileName);
                        _screenshotRepository.CreateScreenshot(screenshotRequest.FileName, screenshot.AsByteArray, formatedUrl);
                    }
                    catch (WebDriverException)
                    {
                        failedURLs.Add(screenshotRequest.URL);
                    }
                    catch (Exception)
                    {
                        return StatusCode(500, "Failed to save screenshots.");
                    }
                }
                else
                {
                    failedURLs.Add(screenshotRequest.URL);
                }
            }

            var response = new TakeSceenshotsPostResponse();
            response.Status = failedURLs.Count == 0 ? "Success" : "Partial success";
            response.FailedURLs = failedURLs;

            return StatusCode(200, response);
        }
    }
}
