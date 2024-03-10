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
    public class LoginPage
    {
     private static IWebDriver driver = Driver.WebDriver;
     private readonly WebDriverWait wait = Driver.WDWait(driver);
     BaseClass baseClass = new BaseClass();
          //Get environment variable fom the YML file. Since tests run in parallel on different browsers

          public void NavigateToLoginPage(string webUrl)
          {
               // Open the URL
               driver.Navigate().GoToUrl(webUrl); 
          }

          public void EnterLoginDetails(string username, string password)
          {

               // Find and fill in the username and password input fields and press Enter
               IWebElement usernameInput = wait.Until(ExpectedConditions.ElementExists(By.Name("username"))); 
               IWebElement passwordInput = wait.Until(ExpectedConditions.ElementExists(By.Name("password"))); 

               usernameInput.SendKeys(username); 
               passwordInput.SendKeys(password + Keys.Enter); 

          }

          public bool VerifyLogIn()
          {
               //If the login was successfull, check that the dashboard element exists.
               try
               {
                    //Find dashboard element
                    IWebElement dashboardElement = wait.Until(ExpectedConditions.ElementExists(By.ClassName("oxd-topbar-header-breadcrumb-module")));
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