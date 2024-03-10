using System;
using System.IO;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using NUnit.Framework;
using System.Runtime.CompilerServices;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Interactions;
using System.Runtime.InteropServices;
using OpenQA.Selenium.DevTools.V119.CSS;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System.Collections.ObjectModel;
using OpenQA.Selenium.DevTools.V119.Network;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.DevTools.V121.FedCm;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using System.Threading.Tasks;
using System.Drawing;
using WebDriverManager.Services.Impl;
//using specflowTesting1.Drivers;
using specflowTesting1.Utilities;

namespace specflowTesting1.Pages
{
    public class PIMPage
    {

     private static IWebDriver driver = Driver.WebDriver;
     private readonly WebDriverWait wait = Driver.WDWait(driver);
     //private readonly ScenarioContext scenarioContext;
     BaseClass baseClass = new BaseClass();

        public void ClickEmployeeList()
       {
          // Wait for the EmployeeList option to be visible and click it
          IWebElement employeeListItem = wait.Until(ExpectedConditions.ElementExists(By.XPath("//a[text()='Employee List']")));
          employeeListItem.Click();
       }

       public void CheckEmployeeListPage()
       {
          try
          {
               // Wait until the URL contains "viewEmployeeList"
               wait.Until(driver => driver.Url.Contains("viewEmployeeList"));
               Console.WriteLine("Employee List page is loaded");
          }
          catch (WebDriverTimeoutException)
          {
               // Handle timeout exception
               baseClass.TakeScreenshot("VerifyEmployeeListPage_Exception");
               throw new Exception("Employee List page did not load successfully");
          }
       }

       public void ClickAddEmployeeButton()
        {
            // Find the button element by its class name
            IWebElement addButton = wait.Until(ExpectedConditions.ElementExists(By.XPath("//i[@class='oxd-icon bi-plus oxd-button-icon']")));
            addButton.Click();
        }

        public void EnterTheEmployeeFirstnameAndLastname(string firstname, string lastname)
        {
          //Enter the employee’s name
          IWebElement firstName_element = wait.Until(ExpectedConditions.ElementExists(By.Name("firstName")));
          IWebElement lastName_element = wait.Until(ExpectedConditions.ElementExists(By.Name("lastName")));

          firstName_element.SendKeys(firstname);
          lastName_element.SendKeys(lastname);
         }

     public void ToggleCreateLoginDetailsButton()
        {
            // Find the checkbox element by its class name inside span and click it
            IWebElement checkbox = driver.FindElement(By.XPath("//span[contains(@class, 'oxd-switch-input--active')]"));
            checkbox.Click();
        }

        public void ClickSaveEmployeeButton()
        {
            IWebElement saveButton = driver.FindElement(By.CssSelector("button[type='submit']"));
            saveButton.Click();
        }

        private IWebElement FindFieldGroup(string fieldName)
     {
        // Find the element with a given text
        IWebElement fieldGroup = wait.Until(ExpectedConditions.ElementExists(By.XPath($"//div[label[text()='{fieldName}']]")));
        //Return the parent of that element
        return fieldGroup.FindElement(By.XPath("./parent::div"));
     }
     private string FindErrorElement(string fieldName)
     {
        // Find the parent element with the given field name
        //IWebElement fieldGroup = FindFieldGroup(fieldName);

          int attempt = 0;
          int maxAttempts = 10;

          while (attempt <= maxAttempts)
          {
               // Locate the span element within the parent element to find the error Message
               try
               {
                    // Find the parent element with the given field name
                    IWebElement fieldGroup = FindFieldGroup(fieldName);
                    IWebElement errorMessage = fieldGroup.FindElement(By.XPath(".//span[contains(@class, 'oxd-input-field-error-message')]"));
                    //IWebElement errorMessage = wait.Until(driver => fieldGroup.FindElement(By.XPath(".//span[contains(@class, 'oxd-input-field-error-message')]")));
                    return errorMessage.Text;
               }
               catch
               {
                    Console.WriteLine($"Couldn't retreive error message for {fieldName}, trying again!, Attempt {attempt}");
                    attempt++;
               }
          }

          return string.Empty;
     }     
     private IWebElement FindInputElement(string fieldName)
     {
        // Find the parent element containing the label and span
        IWebElement fieldGroup = FindFieldGroup(fieldName);
     
        // Locate the span element within the parent element
        return fieldGroup.FindElement(By.XPath(".//input[contains(@class, 'oxd-input')]"));
     }
        public void ValidateTheRequiredFieldAreHighlighted(string UsernameErrorMessage, string PasswordErrorMessage, string ConfirmPasswordErrorMessage)
     {
               //Find the error messages of each of the following fields
               string username_error = FindErrorElement("Username");
               string password_error = FindErrorElement("Password");
               string confirmPassword_error = FindErrorElement("Confirm Password");

               //All three messages should be the same.
               if (username_error.Equals(UsernameErrorMessage) && password_error == PasswordErrorMessage && confirmPassword_error == ConfirmPasswordErrorMessage)
               {
                    Console.WriteLine("All three Fields are Required - Pass ");
               }
               else
               {
                    Console.WriteLine("All three Field are not Required - Fail ");
                    baseClass.TakeScreenshot("ValidateTheRequiredFieldAreHighlighted_Exception");
                    throw new Exception("The required fields not showing required!  Fail");
             
               }

               //Sometimes the ID field throws an error if the id already exists.
               //Check the ID error field is blank as well or fail the test.
               string EmployeeIDError = FindErrorElement("Employee Id");
               if (EmployeeIDError.Equals(""))
               {
                    Console.WriteLine("Employee ID is good! - Pass ");
               }
               else if (EmployeeIDError.Equals("Employee Id already exists"))
               {
                    Console.WriteLine("Employee ID is good! - Pass ");
                    baseClass.TakeScreenshot("The Employee ID is already exists_Exception");
                    throw new Exception("The Employee ID is already exists, user cannot be added!  Fail");
               }
               else
               {
                    baseClass.TakeScreenshot("The Employee ID field is throwing an error!_Exception");
                    throw new Exception("The Employee ID field is throwing an error! -  Fail");
               }

     }
      public void FillMandatoryFieldsUsernameAndPassword(string username, string password)
     {
          //Find the inpu text fields by looking for the tags above the text input
          IWebElement username_field = FindInputElement("Username");
          IWebElement password_field = FindInputElement("Password");
          IWebElement confirmPassword_field = FindInputElement("Confirm Password");

          //Enter username and password
          username_field.SendKeys(username);
          password_field.SendKeys(password);
          confirmPassword_field.SendKeys(password);
     }
     public void ValidatErrorsAreGone()
          {
               //The website can be slow to repsond sometimes and the required fields take longer than it should to disappear.
               int attempt = 0;
               int maxAttempts = 20;

               //Atempt up to 20 times to check the error messages are gone.
               while (attempt < maxAttempts)
               {
                    //Find the error messages.
                    string username_error = FindErrorElement("Username");
                    string password_error = FindErrorElement("Password");
                    string confirmPassword_error = FindErrorElement("Confirm Password");

                    //Check all error messages are blank
                    if (username_error.Equals("") && password_error.Equals("") && confirmPassword_error.Equals(""))
                    {
                         Console.WriteLine("All three Fields are good - Pass ");
                         break;
                    }
                    else
                    {
                         attempt++;
                    }
                    
               }

               // if the attempts reached max amount of attempts that means the errors messages didn't disappear.
               if (attempt == maxAttempts)
               {
                    Console.WriteLine("One of the three fields is still showing an error - Fail ");
                    baseClass.TakeScreenshot("ValidateErrorsAreGone_Exception");
                    throw new Exception("The required fields are not entered properly!  Fail");
               }

          }

        public void EmployeeIsAddedSuccessfullyToTheList(string snackbar_message)
     {
          //Wait until snackbar shows up. 
          IWebElement snackBar = wait.Until(ExpectedConditions.ElementExists(By.ClassName("oxd-text--toast-message")));
          Assert.IsNotNull(snackBar, "SnackBar message 'Successfully Updated' not found. Test failed.");
           
          
          
          // We should check the text of the snackbar to confirm it worked.
          int attempt = 0;
          int maxAttempts = 10;
          string snackbar_text = "";

          while (attempt < maxAttempts)
          {
               snackbar_text = snackBar.Text;
               Console.WriteLine("Snack bar message: " + snackbar_text);
               
               if (snackbar_text == snackbar_message)
               {
                    break;
               }
               attempt++;
          }

          if (snackbar_text != snackbar_message)
          {
               baseClass.TakeScreenshot("EmployeeIsAddedSuccessfullyToTheList_Exception");
               throw new Exception($"The snackbar text did not show {snackbar_message}, actual text was {snackbar_text}-  Fail");
          }

     }
    public void CheckEmployeeNameInTheSearchTable(string displayOption, string firstname)
     {
          
          if (displayOption == "displayed")
          {
               try
               {
                    // Scroll to the bottom of the page
                    IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
                    jsExecutor.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");

                    //Wait until the table cell containing the employeename is visible and exists
                    IWebElement userElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//div[contains(text(), '{firstname}')]")));
                    userElement = wait.Until(ExpectedConditions.ElementExists(By.XPath($"//div[contains(text(), '{firstname}')]")));
                    
                    Assert.IsNotNull(userElement, $"User '{firstname}' not found");
                    Console.WriteLine($"Employee Name '{firstname}' found on the list! - Pass");
               }
               catch
               {
                    baseClass.TakeScreenshot("CheckEmployeeNameInTheSearchTable_Exception");
                    throw new Exception("Employee Name not found in the list!");
               }
          }
          else if (displayOption == "not displayed")
          {
               bool employeeNameAfterDeletion = driver.FindElements(By.XPath($"//div[contains(text(), '{firstname}')]")).Count > 0;
               if (employeeNameAfterDeletion)
               {
                    Assert.Fail($"Employee name '{firstname}' exists after deletion - Fail");
               }
               else
               {
                    Console.WriteLine($"Employee name '{firstname}' does not exist after deletion - Pass");
               }
          }

     }

      public void VerifyFirstNameColoumnIsInAscendingOrder(string columnHeader, string columnOrder)
     {
  
          //Using the First Name parent find the sort icon and click on it
          IWebElement sortIcon = FindIconThroughHeaderElement(columnHeader);

          // Check if the sort icon indicates ascending order
          FindSortingOrder(sortIcon, columnOrder, columnHeader);

     }

     private void FindSortingOrder(IWebElement sortIcon, string columnOrder, string headerName)
     {

          //Check the class attribute of the sort icon to determine the sorting order
          string sortIconClass = sortIcon.GetAttribute("class");
          string sortingOrder;
          
          // Check if the sort icon indicates ascending order
          if (sortIconClass.Contains("bi-sort-alpha-down") )
          {
               sortingOrder = "ascending";
          }
          else if (sortIconClass.Contains("bi-sort-alpha-up"))
          {
               sortingOrder = "descending";
          }
          else
          {
               sortingOrder = "";
               baseClass.TakeScreenshot("ColoumnOrderNotFound_Exception");
               throw new Exception($"The '{headerName}' coloumn order could not be found - Fail");
          }
          
          //Check if the column sorting order matches
          if (sortingOrder.Equals(columnOrder))
          {
               Console.WriteLine($"The '{headerName}' coloumn is in '{columnOrder}' order - Pass");
          }
          else
          {
               Console.WriteLine($"The '{headerName}' coloumn is in not in '{columnOrder}' order - Fail");
               baseClass.TakeScreenshot("ColoumnOrderIncorrect_Exception");
               throw new Exception($"The '{headerName}' coloumn is in not in '{columnOrder}' order - Fail");
          }

     }
     
     private IWebElement FindIconThroughHeaderElement(string headerName)
     {

          //find header cell (Parent) wth the given headerName
          IWebElement headerElement = wait.Until(ExpectedConditions.ElementExists(By.XPath($"//div[contains(@class, 'oxd-table-header-cell') and text()='{headerName}']")));
          //Using the header parent cell find and return the sort icon button
          return headerElement.FindElement(By.XPath(".//i[contains(@class, 'oxd-icon-button__icon')]"));

     }   

     public void ChangeTheLastNameColoumnToDescendingOrder(string headerName, string columnOrder)
     {

          //Using the Last Name parent find the sort icon and click on it
          IWebElement sortIcon = FindIconThroughHeaderElement(headerName);
          sortIcon.Click();

          //Find the dropdownMenu and within that find the descending icon
          IWebElement dropdownMenu = wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div.--active[role='dropdown'][data-v-c9cca83c='']")));

          // set the column order as defined by clicking on the relevant option
          if (columnOrder == "ascending")
          {
               IWebElement sortingOption = dropdownMenu.FindElement(By.XPath(".//i[contains(@class, 'bi-sort-alpha-down')]"));
               sortingOption.Click(); 
          }
          else if (columnOrder == "descending")
          {
               IWebElement sortingOption = dropdownMenu.FindElement(By.XPath(".//i[contains(@class, 'bi-sort-alpha-up')]")); 
               sortingOption.Click();
          }
          else
          {
               baseClass.TakeScreenshot("ChangeTheLastNameColoumnToDescendingOrder_Exception");
               throw new Exception($"The '{headerName}' coloumn order could not be determined - Fail");
          }

     }
     public void EnterFirstNameAndLastName(string firstname, string lastname)
    {
     IWebElement employeeName = wait.Until(ExpectedConditions.ElementExists(By.ClassName("oxd-autocomplete-text-input"))).FindElement(By.TagName("input"));
     
     try
     {
          employeeName.SendKeys(firstname + " " + lastname);
     }
     catch
     {
          baseClass.TakeScreenshot("EnterFirstAndLastName_Exception");
          throw new Exception($"The employee name could not be typed in - Fail");
     }
    }

public void ClickSearchButton()
    {
     //Search button contains the 'submit' text.
	IWebElement saveButton = wait.Until(ExpectedConditions.ElementExists(By.CssSelector("button[type='submit']")));
     saveButton.Click();
    }

    public void DoubleClickOnTheEmployeeName(string firstname)
{
	try
     {
          IWebElement employeeFirstName = wait.Until(ExpectedConditions.ElementExists(By.XPath($"//div[contains(text(), '{firstname}')]")));
          // Create an instance of Actions class
          Actions actions = new Actions(driver);

          // Double-click on the div element
          actions.DoubleClick(employeeFirstName).Perform();
     }
     catch
     {
          baseClass.TakeScreenshot("DoubleClickOnTheEmployeeName_Exception");
          throw new Exception($"The employee name could not be clicked twice - Fail");
     }      
}

public void DeleteTheEmployee()
{
     //Currently it is the only trash button so easy to fine. But we should code it better, in case there is more than one file.
     IWebElement trashButton = wait.Until(ExpectedConditions.ElementExists(By.XPath("//i[@class='oxd-icon bi-trash']")));
     trashButton.Click();

     // Wait for the confirmation trash button to be clickable
    IWebElement confirmTrashButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//i[@class='oxd-icon bi-trash oxd-button-icon']")));
    confirmTrashButton.Click();
}   
    }
}