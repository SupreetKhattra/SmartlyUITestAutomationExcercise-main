namespace specflowTesting1.Utilities
{
    public class BaseClass
    {

        public static IWebDriver driver; 
        public static WebDriverWait wait; 

        public void TakeScreenshot(string screenshotName)
        {
            
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(screenshotName);
            
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            
            // Generate a unique file name for the screenshot
            string screenshotFileName = $"{screenshotName}_{DateTime.Now:yyyyMMddHHmmssfff}.png";

            // Define the directory to save the screenshot
            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            string screenshotDirectory = Path.Combine(projectDirectory, "Screenshots");

            // Ensure the directory exists
           Directory.CreateDirectory(screenshotDirectory);
            string screenshotPath = Path.Combine(screenshotDirectory, screenshotFileName);
            screenshot.SaveAsFile(screenshotPath);
        }

        public void InitializeDrivers()
        {
          driver = Driver.WebDriver;
          wait = Driver.WDWait(driver);
        }
        public void Logout()
        {
          Console.WriteLine("Logging out");
          try
          {
              //Logout            
            IWebElement userdropDown = wait.Until(ExpectedConditions.ElementExists(By.XPath("//p[@class='oxd-userdropdown-name']")));
            userdropDown.Click();
            IWebElement logoutOption = wait.Until(ExpectedConditions.ElementExists(By.XPath("//a[text()='Logout']")));
            logoutOption.Click();
            Console.WriteLine("Logged out successfully!");
          }
          catch
          {
            TakeScreenshot("logout - Exception");
            Console.WriteLine("Failed to log out");
          }

        }
        public static void CloseDrivers()
        {
          Console.WriteLine("Quitting driver");
          Driver.QuitWebDriver();
        }

        public void Navigate(string url)
        {
          // Open the URL
          driver.Navigate().GoToUrl(url); 
        }
 
    }

}

