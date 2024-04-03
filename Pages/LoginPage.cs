namespace specflowTesting1.Pages
{
    public class LoginPage
    {
     private static IWebDriver driver = Driver.WebDriver;
     private readonly WebDriverWait wait = Driver.WDWait(driver);
     BaseClass baseClass = new BaseClass();

     // Define constants for element locators
     private const string UsernameInputName = "username";
     private const string PasswordInputName = "password";
     private const string DashboardElementClassName = "oxd-topbar-header-breadcrumb-module";

          public void NavigateToLoginPage(string webUrl)
          {
               driver.Navigate().GoToUrl(webUrl); 
          }

          public void EnterLoginDetails(string username, string password)
          {

               // Find and fill in the username and password input fields and press Enter
               IWebElement usernameInput = wait.Until(ExpectedConditions.ElementExists(By.Name(UsernameInputName))); 
               IWebElement passwordInput = wait.Until(ExpectedConditions.ElementExists(By.Name(PasswordInputName))); 

               usernameInput.SendKeys(username); 
               passwordInput.SendKeys(password + Keys.Enter); 

          }

          public bool VerifyLogIn()
          {
               //If the login was successfull, check that the dashboard element exists.
               try
               {
                    //Find dashboard element
                    IWebElement dashboardElement = wait.Until(ExpectedConditions.ElementExists(By.ClassName(DashboardElementClassName)));
                    Assert.IsNotNull(dashboardElement, "Dashboard page did not load successfully"); 

                    //Create a IsLoggedIn scenario context and set it to true.
                    Console.WriteLine("Logged In Successfully");
                    return true;
               }
               catch
               {
                    //If the dashboard Element is not found (or does not exist)
                    Console.WriteLine("Log In Unsuccessful");
                    baseClass.TakeScreenshot("CheckUserIsLoggedIn_Exception");
                    throw new Exception("Dashboard page did not load successfully");

               }         
          }

     }
}