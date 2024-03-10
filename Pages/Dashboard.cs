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
    public class DashboardPage
    {
     private static IWebDriver driver = Driver.WebDriver;
     private readonly WebDriverWait wait = Driver.WDWait(driver);
        //Get environment variable fom the YML file. Since tests run in parallel on different browsers

        public void ClickOnPIM()
          {
               IWebElement PIM = driver.FindElement(By.CssSelector("a.oxd-main-menu-item[href='/web/index.php/pim/viewPimModule']"));
               //Click on Pim
               PIM.Click();
          }
       }
    }