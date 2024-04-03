namespace specflowTesting1.Pages
{
    public class MyInfoPage
    {
     private static IWebDriver driver = Driver.WebDriver;
     private readonly WebDriverWait wait = Driver.WDWait(driver);
    BaseClass baseClass = new BaseClass();
     // Define constants for element locators
    private const string EmployeeNameXPath = "//div[@class='orangehrm-edit-employee-name']";
    private const string AddButtonXPath = "//i[@class='oxd-icon bi-plus oxd-button-icon']";
    private const string SaveButtonCssSelector = "button[type='submit']";
     private const string TrashButtonXPath = "//i[@class='oxd-icon bi-trash']";
     private const string ConfirmTrashButtonXPath = "//i[@class='oxd-icon bi-trash oxd-button-icon']";
public void VerifyThefirstNameAndLastNameIsSameAsTheSelectedName(string firstname, string lastname)
{
//Check the epmployee full name is showing on the user profile page.
	string fullName = firstname + ' ' + lastname;
     
     IWebElement employeeFirstName = wait.Until(driver =>
     {
          IWebElement element = driver.FindElement(By.XPath(EmployeeNameXPath));

          if (element.Text.Contains(fullName))
          {
               Console.WriteLine($"The employee name matches - Pass");
               return element;
          }

          return null;
     });

}

public void ClickAddButtonInTheAttachments()
{
     // Scroll to the bottom of the page
     IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
     jsExecutor.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");

     //Find the plus icon in the Add buon and click it.
     IWebElement addButton = driver.FindElement(By.XPath(AddButtonXPath));
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
     ReadOnlyCollection<IWebElement> saveButtons = driver.FindElements(By.CssSelector(SaveButtonCssSelector));

     //Count the number of buttons found
     int saveButton_counts = saveButtons.Count;
     //check there is more than one save button, our save button is the bottom one.

     if (saveButton_counts > 2)
     {
          // Double click on the last button
          IWebElement lastSaveButton = saveButtons[saveButton_counts - 1];
          // Create an instance of Actions class
          Actions actions = new Actions(driver);
           // Double-click on the div element
          actions.DoubleClick(lastSaveButton).Perform();

          Console.WriteLine($"The attachment is Saved - Pass");
          break; 
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
          IWebElement trashButton = wait.Until(ExpectedConditions.ElementExists(By.XPath(TrashButtonXPath)));
          trashButton.Click();


          IWebElement confirmTrashButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(ConfirmTrashButtonXPath)));
          confirmTrashButton.Click();
     }
     catch
     {
          baseClass.TakeScreenshot("DeleteTheAttachedFile_Exception");
          throw new Exception("Attachment File could not be deleted - Fail!");
     }
}
}
}