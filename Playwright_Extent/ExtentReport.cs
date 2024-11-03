using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playwright_Extent
{
    public class ExtentReport
    {
        public static ExtentReports extentReports;
        public static ExtentTest exParentTest;
        public static ExtentTest exChildTest;
        public static string dirpath;
        public static string pathWithFileNameExtension;

        public static void LogReport(string testcase)
        {
            extentReports = new ExtentReports();
            dirpath = @"..\..\TestExecutionReports\" + "_" + testcase;

            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(dirpath);
            htmlReporter.Config.Theme = Theme.Standard;
            extentReports.AttachReporter(htmlReporter);
        }

        public static async Task TakeScreenshot(IPage page, Status status, string stepDetail)
        {
            string path = @"C:\Users\mt\source\repos\Playwright_Extent\Playwright_Extent\bin\Screenshot\" + "TestExecLog_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            pathWithFileNameExtension = @path + ".png";
            await page.Locator("body").ScreenshotAsync(new LocatorScreenshotOptions { Path = pathWithFileNameExtension });
            TestContext.AddTestAttachment(pathWithFileNameExtension);
            exChildTest.Log(status, stepDetail, MediaEntityBuilder
                .CreateScreenCaptureFromPath(path + ".png").Build());
        }


    }
}
