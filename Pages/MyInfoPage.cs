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
    public class MyInfoPage
    {
     private static IWebDriver driver = Driver.WebDriver;
     private readonly WebDriverWait wait = Driver.WDWait(driver);
    BaseClass baseClass = new BaseClass();
        //Get environment variable fom the YML file. Since tests run in parallel on different browsers


public void VerifyThefirstNameAndLastNameIsSameAsTheSelectedName(string firstname, string lastname)
{
     //Check the epmployee full name is showing on the user profile page.
	string fullName = firstname + ' ' + lastname;
     //IWebElement fieldElement = wait.Until(ExpectedConditions.ElementExists(By.XPath($"//div[@class='orangehrm-edit-employee-name']")));
     
     // Use a lambda expression as a custom condition - condition being that the text of the employee contains the full name.
     IWebElement employeeFirstName = wait.Until(driver =>
     {
          // Find the element
          IWebElement element = driver.FindElement(By.XPath("//div[@class='orangehrm-edit-employee-name']"));

          // Check if the text contains the expected value
          if (element.Text.Contains(fullName))
          {
               // Return the element if the condition is met
               Console.WriteLine($"The employee name matches - Pass");
               return element;
          }

          // Return null if the condition is not met (this will make the wait continue)
          return null;
     });

}

public void ClickAddButtonInTheAttachments()
{
	// Scroll to the bottom of the page
     IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
     jsExecutor.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");

     //Find the plus icon in the Add buon and click it.
     IWebElement addButton = driver.FindElement(By.XPath("//i[@class='oxd-icon bi-plus oxd-button-icon']"));
     addButton.Click();
}

    public void ClickBrowseButtonAndAttachTheFile(string fileName)
{
     //get the test file full path, file kept under the current directory.
     string currentDirectory = Directory.GetCurrentDirectory();
     string filePath = Path.Combine(currentDirectory, fileName);

     // Find the input element by class name
     IWebElement fileInput = wait.Until(ExpectedConditions.ElementExists(By.CssSelector(".oxd-file-input")));
     // Attach file by entering full file path.
     fileInput.SendKeys(filePath);
}

public void ClickSavebutton()
{
     // Scroll to the bottom of the page
     IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
     jsExecutor.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");

     int maxAttempts = 100;
     int attempt = 0;

     //Sometimes it takes a while to determine all the elements.
     while (attempt < maxAttempts)
     {
     //Find all the save butons with the type submit.
     ReadOnlyCollection<IWebElement> saveButtons = driver.FindElements(By.CssSelector("button[type='submit']"));
     //Count the number of buttons found
     int saveButton_counts = saveButtons.Count;

     //check there is more than one save button, our save button is the bottom one.
     if (saveButton_counts > 2)
     {
          // Double click on the last button
          IWebElement lastSaveButton = saveButtons[saveButton_counts - 1];
          //lastSaveButton.Click();

          // Create an instance of Actions class
          Actions actions = new Actions(driver);
           // Double-click on the div element
          actions.DoubleClick(lastSaveButton).Perform();

          Console.WriteLine($"The attachment is Saved - Pass");
          break; // exit the loop if successful
     }

     attempt++;
     }

     //if the max attempts are reached and no save buttons found, then the test has failed.
     if (attempt == maxAttempts)
     {
          Console.WriteLine($"The attachment could not be saved! - Fail");
          baseClass.TakeScreenshot("AttachmentNotSaved_Exception");
          throw new Exception($"The attachment could not be saved! - Fail");
     }
}

public void VerifyTheFilenameIsDisplayedInTheAttachmentTable(string fileName, string displayStatus)
{
     // Assert that the user element is not null
     if (displayStatus.Equals("displayed"))
     {
          try
          {
               // Fild the element that contains a cell with text that contains the filename.
               IWebElement attachmentFile = wait.Until(ExpectedConditions.ElementExists(By.XPath($"//div[contains(text(), '{fileName}')]")));
               Assert.IsNotNull(attachmentFile, $"Attachment File not found - Fail!");
               Console.WriteLine($"attached file is {displayStatus}");
          }
          catch
          {
               Console.WriteLine($"attached file is {displayStatus} - Error");
               baseClass.TakeScreenshot("AttachmentFileNotDisplayed_Exception");
               throw new Exception("Attachment file is not displayed! - Error");
          }

     }
     // Assert that the user element does not exist
     else if (displayStatus.Equals("not displayed"))
     {
          try
          {
               
               // Check if there is an element which contains the filename as a text.  
               bool attachmentFileAfterDeletion = driver.FindElements(By.XPath($"//div[contains(text(), '{fileName}')]")).Count > 0;
               // If there isn't any then pass the test.
               Assert.IsFalse(attachmentFileAfterDeletion, $"Attachment File '{fileName}'does not exist after deletion - Pass");
               Console.WriteLine($"attached file is {displayStatus}");
          }
          catch
          {
               Console.WriteLine($"attached file is {displayStatus} - Error");
               baseClass.TakeScreenshot("AttachmentFileIsDisplayed_Exception");
               throw new Exception("Attachment file is displayed! - Error");
          }

     }
     else
     {
          baseClass.TakeScreenshot("VerificationNotUnderstoodForTheFilenameInTheAttachmentTable_Exception");
          throw new Exception("Verification not understood - Error");
     }

}
public void DeleteTheAttachedFile(string fileName)
{
     try
     {
          //Currently it is the only trash button so easy to fine. But we should code it better, in case there is more than one file.
          IWebElement trashButton = wait.Until(ExpectedConditions.ElementExists(By.XPath("//i[@class='oxd-icon bi-trash']")));
          trashButton.Click();

          //((ITakesScreenshot)driver).GetScreenshot().SaveAsFile("trashbutton_clicked.png");

          // Wait for the confirmation trash button to be clickable
          IWebElement confirmTrashButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//i[@class='oxd-icon bi-trash oxd-button-icon']")));
          confirmTrashButton.Click();
          //((ITakesScreenshot)driver).GetScreenshot().SaveAsFile("confirmrashbutton_clicked.png");
     }
     catch
     {
          //((ITakesScreenshot)driver).GetScreenshot().SaveAsFile("trashbutton_error.png");
          baseClass.TakeScreenshot("DeleteTheAttachedFile_Exception");
          throw new Exception("Attachment File could not be deleted - Fail!");
     }
}
}
}