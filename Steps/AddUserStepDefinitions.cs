namespace specflowTesting1.Steps
{
    [Binding]
    public class AddUserStepDefinitions
    {

     private readonly ScenarioContext scenarioContext;
     LoginPage loginPage = new LoginPage();
     BaseClass baseClass = new BaseClass();
     DashboardPage dashboardPage = new DashboardPage();
     PIMPage pIMPage = new PIMPage();
     MyInfoPage myInfoPage = new MyInfoPage();
//Get environment variable fom the YML file. Since tests run in parallel on different browsers
     private string firstname = Environment.GetEnvironmentVariable("FIRSTNAME") ?? "Aaa111";
     private string lastname = Environment.GetEnvironmentVariable("LASTNAME") ?? "Aaa111";
     private string username = Environment.GetEnvironmentVariable("USERNAME") ?? "johndoe567_";
     private string password = Environment.GetEnvironmentVariable("PASSWORD") ?? "password111";

     public AddUserStepDefinitions(ScenarioContext scenarioContext)
     {
     this.scenarioContext = scenarioContext;
     }

 
     [Given(@"the user is on the login page '(.*)'")]
       public void UserIsOnTheLoginPage(string webUrl)
       {
          loginPage.NavigateToLoginPage(webUrl);
       }

     [Given(@"the user enters the username '(.*)' and password '(.*)'")]
       public void UserEntersTheUsernameAndPassword(string username, string password)
       {
          loginPage.EnterLoginDetails(username, password);
       }

       [Then(@"the user should be logged in successfully")]
       public void CheckUserIsLoggedIn()
       {
          bool successfullyLoggedIn = loginPage.VerifyLogIn();
          
          scenarioContext["IsLoggedIn"] = successfullyLoggedIn;
       }

       [Given(@"the user clicks on PIM")]
       public void UserClicksOnPIM()
       {
      if (scenarioContext.ContainsKey("IsLoggedIn") && (bool)scenarioContext["IsLoggedIn"])
          {
               dashboardPage.ClickOnPIM();
          }
          else
          {
//If the log in was not successful, throw an xception with the relevant message               Console.WriteLine("Cannot find or click on PIM button");
               baseClass.TakeScreenshot("ClickOnPIM_Exception");
               throw new Exception("User is not logged in. Cannot click on PIM.");
          }
       }

       [When(@"the user clicks EmployeeList")]
       public void UserClicksEmployeeList()
       {
          pIMPage.ClickEmployeeList();
       }
   
       [Then(@"Verify the EmployeeList Page is Loaded")]
       public void VerifyEmployeeListPage()
       {
         pIMPage.CheckEmployeeListPage();
       }

     [Given (@"the user clicks the Add button")]
     public void UserClicksAddEmployeeButton()
          {
               pIMPage.ClickAddEmployeeButton();
          }

     [When(@"Enters the employee first name and last name")]
     public void WhenEnterstheemployeefirstnameFirstNameandlastnameLastName()
     {
          pIMPage.EnterTheEmployeeFirstnameAndLastname(firstname,lastname);
     }

     [When (@"the user toggles create login details button")]
     public void UserTogglesCreateLoginDetailsButton()
     {
          pIMPage.ToggleCreateLoginDetailsButton();
     }

     [When (@"the user clicks the Save button")]
     public void UserClicksSaveEmployeeButton()
     {
          pIMPage.ClickSaveEmployeeButton();
     }

    
     [Then(@"validate the 'Required' and 'Passwords do not match' fields are highlighted")]
     public void ThenvalidatetheRequiredandPasswordsdonotmatchfieldsarehighlighted()
     {
          Thread.Sleep(1000);
          pIMPage.ValidateTheRequiredFieldAreHighlighted("Required", "Required", "Passwords do not match");
     }

     [When(@"user fills mandatory fields username and password")]
     public void Whenuserfillsmandatoryfieldsusernameandpassword()
     {
          pIMPage.FillMandatoryFieldsUsernameAndPassword(username, password);
     }

     [When(@"validate errors are gone")]
     public void Thenvalidateerrorsaregone()
          {
              pIMPage.ValidatErrorsAreGone();
          }

     [Then(@"the snackbar shows '(.*)'")]
     public void Thentheemployeeisaddedsuccessfullytothelist(string snackbar_message)
     {
          pIMPage.EmployeeIsAddedSuccessfullyToTheList(snackbar_message);
     }

     [Then(@"verify the employee first name is '(.*)' in the search table")]
     public void ThenchecktheemployeenameAaaaainthesearchtable(string displayOption)
     {
          pIMPage.CheckEmployeeNameInTheSearchTable(displayOption, firstname);
     }

     [Then(@"verify the '(.*)' coloumn is in '(.*)' order")]
     public void ThenverifytheFirstNamecoloumnisinascendingorder(string columnHeader, string columnOrder)
     {
  
          pIMPage.VerifyFirstNameColoumnIsInAscendingOrder(columnHeader, columnOrder);

     }
 
     [When(@"the user changes the '(.*)' coloumn to '(.*)' order")]
     public void WhentheuserchangestheLastNamecoloumntodescendingorder(string headerName, string columnOrder)
     {
          pIMPage.ChangeTheLastNameColoumnToDescendingOrder(headerName, columnOrder);

     }

[Given(@"the user enters the first name and last name")]
public void WhentheuserentersthefirstnameAaaaaandlastnameAaaaaaa()
{
	pIMPage.EnterFirstNameAndLastName(firstname, lastname);
}

[When(@"the user clicks Search button")]
public void WhentheuserclicksSearchbutton()
{
     pIMPage.ClickSearchButton();
}

[When(@"double click on the employee name in the search table")]
public void WhendoubleclickontheemployeenameAaaaainthesearchtable()
{
	pIMPage.DoubleClickOnTheEmployeeName(firstname);    
}

[Then(@"Verify the first name and last name is same as the selected name")]
public void ThenVerifythefirstnameAaaaaandlastnameAaaaaaaissameastheselectedname()
{
     myInfoPage.VerifyThefirstNameAndLastNameIsSameAsTheSelectedName(firstname, lastname);
}

[When(@"the user clicks add button in the Attachments")]
public void WhentheuserclicksaddbuttonintheAttachments()
{
	myInfoPage.ClickAddButtonInTheAttachments();

}

[When(@"clicks Browse button attached the file '(.*)'")]
public void WhenclicksBrowsebuttonattachedthefile(string fileName)
{
     myInfoPage.ClickBrowseButtonAndAttachTheFile(fileName);
}

[When(@"clicks the last Save button")]
public void WhenClickSavebutton()
{
     myInfoPage.ClickSavebutton();
}

[Then(@"Verify the file '(.*)' is '(.*)' in the Attachment table")]
public void ThenVerifythefilenameisdisplayedintheAttachmenttable(string fileName, string displayStatus)
{
     myInfoPage.VerifyTheFilenameIsDisplayedInTheAttachmentTable(fileName, displayStatus);
}

[When(@"the user Deletes the attached file '(.*)'")]
public void WhentheuserDeletestheattachedfile(string fileName)
{
    myInfoPage.DeleteTheAttachedFile(fileName);
}


[When(@"the user deletes the employee")]
public void Whentheuserdeletestheemployee()
{
     pIMPage.DeleteTheEmployee();
}

    }
}