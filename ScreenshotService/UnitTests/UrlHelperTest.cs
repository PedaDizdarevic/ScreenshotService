using NUnit.Framework;
using ScreenshotService;

namespace UnitTests
{
    public class UrlHelperTest
    {
        [TestCase("http://www.test.com")]
        [TestCase("www.test.com")]
        [TestCase("test.com")]
        public void CorrectlyFormatUrl(string urlArg)
        {
            Assert.AreEqual("http://www.test.com", UrlHelper.FormatUrl(urlArg));
        }

        [TestCase("http://www.test.com")]
        public void PassValidation(string urlArg)
        {
            Assert.AreEqual("", UrlHelper.ValidateUrl(urlArg));
        }

        [TestCase("http://wwww.test twice.com")]
        public void FailValidation(string urlArg)
        {
            Assert.AreNotEqual("", UrlHelper.ValidateUrl(urlArg));
        }
    }
}