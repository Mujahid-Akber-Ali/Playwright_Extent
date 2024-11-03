using AventStack.ExtentReports;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System.Globalization;

namespace Playwright_Extent
{
    public class Tests : PageTest
    {

        [SetUp]
        public async Task setup()
        {
            ContextOptions();

            await Context.Tracing.StartAsync(new()
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });

            ExtentReport.LogReport("Basic Extent");

        }

        [TearDown]
        public async Task teardown()
        {
            Thread.Sleep(3000);
            // Stop tracing and export it into a zip archive.
            await Context.Tracing.StopAsync(new()
            {
                Path = @"trace/" + TestContext.CurrentContext.Test.MethodName + "_" + DateTime.Now.ToString("yyyymmddhhmmss").ToString() + ".zip"
            });

            ExtentReport.extentReports.Flush();

            await Context.CloseAsync();
            await Browser.CloseAsync();


            //  driver.Close();
        }

        [Test]
        public async Task Test1()
        {
            ExtentReport.exParentTest = ExtentReport.extentReports.CreateTest("Basic Extent");

            ExtentReport.exChildTest = ExtentReport.exParentTest.CreateNode("Orange HRM");


            await Page.GotoAsync("https://opensource-demo.orangehrmlive.com/web/index.php/auth/login");
            await ExtentReport.TakeScreenshot(Page, Status.Pass, "Open Url");


            await Page.FillAsync("#app > div.orangehrm-login-layout > div > div.orangehrm-login-container > div > div.orangehrm-login-slot > div.orangehrm-login-form > form > div:nth-child(2) > div > div:nth-child(2) > input", "Admin");
            await ExtentReport.TakeScreenshot(Page, Status.Pass, "Enter Username");


            await Page.FillAsync("#app > div.orangehrm-login-layout > div > div.orangehrm-login-container > div > div.orangehrm-login-slot > div.orangehrm-login-form > form > div:nth-child(3) > div > div:nth-child(2) > input", "admin123");
            await ExtentReport.TakeScreenshot(Page, Status.Pass, "Enter Password");


            await Page.ClickAsync("#app > div.orangehrm-login-layout > div > div.orangehrm-login-container > div > div.orangehrm-login-slot > div.orangehrm-login-form > form > div.oxd-form-actions.orangehrm-login-action > button");
            await ExtentReport.TakeScreenshot(Page, Status.Pass, "Click Login");
        }


        //public async Task Fill(string locator, String data, string name)
        //{
        //        await Page.FillAsync(locator, data);
        //        await ExtentReport.TakeScreenshot(Page, Status.Pass, name);

        //}


        public override BrowserNewContextOptions ContextOptions()
        {
            return new BrowserNewContextOptions()
            {
                RecordVideoDir = @"videos/" + TestContext.CurrentContext.Test.MethodName + "_" + DateTime.Now.ToString("yyyymmddhhmmss").ToString(),
                //StorageStatePath = @"state\pagetest_state.json",
                ViewportSize = new ViewportSize
                {
                    Height = 780,
                    Width = 1380
                },
                RecordVideoSize = new RecordVideoSize
                {
                    Height = 780,
                    Width = 1380
                }
            };

        }

    }
}