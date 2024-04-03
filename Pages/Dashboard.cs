namespace specflowTesting1.Pages
{
    public class DashboardPage
    {
     private static IWebDriver driver = Driver.WebDriver;
     private readonly WebDriverWait wait = Driver.WDWait(driver);
     private const string PimCssSelector = "a.oxd-main-menu-item[href='/web/index.php/pim/viewPimModule']";

        public void ClickOnPIM()
          {
               IWebElement PIM = driver.FindElement(By.CssSelector(PimCssSelector));
               PIM.Click();
          }
       }
    }