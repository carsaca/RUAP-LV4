using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RUAP_LV4
{
    [TestClass]
    public class UnitTest1
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        Random random = new Random();
        

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "https://www.google.com/";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            NUnit.Framework.Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void RegisterNewUser()
        {
            driver.Navigate().GoToUrl("http://demowebshop.tricentis.com/");
            driver.FindElement(By.LinkText("Register")).Click();
            driver.FindElement(By.Id("gender-male")).Click();
            driver.FindElement(By.Id("FirstName")).Click();
            driver.FindElement(By.Id("FirstName")).Clear();
            driver.FindElement(By.Id("FirstName")).SendKeys("Marko");
            driver.FindElement(By.Id("LastName")).Click();
            driver.FindElement(By.Id("LastName")).Clear();
            driver.FindElement(By.Id("LastName")).SendKeys("Marić");
            driver.FindElement(By.Id("Email")).Click();
            driver.FindElement(By.Id("Email")).Clear();
            driver.FindElement(By.Id("Email")).SendKeys(string.Format("qa{0:0000}@test.com", random.Next(10000)));
            driver.FindElement(By.XPath("//div[2]/div[2]/div[2]")).Click();
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("123456789");
            driver.FindElement(By.XPath("//form/div/div[2]/div[4]")).Click();
            driver.FindElement(By.Id("ConfirmPassword")).Click();
            driver.FindElement(By.Id("ConfirmPassword")).Click();
            driver.FindElement(By.Id("ConfirmPassword")).Clear();
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys("123456789");
            driver.FindElement(By.XPath("//div[2]/div[3]/div[2]/div")).Click();
            driver.FindElement(By.Id("register-button")).Click();
            driver.FindElement(By.XPath("//input[@value='Continue']")).Click();
        }

        [Test]
        public void SerarchAddToCart()
        {
            driver.Navigate().GoToUrl("http://demowebshop.tricentis.com/");
            driver.FindElement(By.Id("small-searchterms")).Click();
            driver.FindElement(By.Id("small-searchterms")).Clear();
            driver.FindElement(By.Id("small-searchterms")).SendKeys("sunglasses");
            driver.FindElement(By.XPath("//input[@value='Search']")).Click();
            driver.FindElement(By.XPath("//input[@value='Add to cart']")).Click();
            driver.FindElement(By.XPath("//li[@id='topcartlink']/a/span")).Click();
        }

        [Test]
        public void LogInAddToCartEstimateShipping()
        {
            driver.Navigate().GoToUrl("http://demowebshop.tricentis.com/");
            driver.FindElement(By.LinkText("Log in")).Click();
            driver.FindElement(By.Id("Email")).Clear();
            driver.FindElement(By.Id("Email")).SendKeys("markomaric@gmail.com");
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("123456789");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.XPath("(//a[contains(text(),'Electronics')])[3]")).Click();
            driver.FindElement(By.XPath("(//a[contains(text(),'Cell phones')])[4]")).Click();
            driver.FindElement(By.XPath("//input[@value='Add to cart']")).Click();
            driver.FindElement(By.XPath("//li[@id='topcartlink']/a/span")).Click();
            driver.FindElement(By.Id("CountryId")).Click();
            new SelectElement(driver.FindElement(By.Id("CountryId"))).SelectByText("Croatia");
            driver.FindElement(By.XPath("//option[@value='24']")).Click();
            driver.FindElement(By.Id("ZipPostalCode")).Click();
            driver.FindElement(By.Id("ZipPostalCode")).Clear();
            driver.FindElement(By.Id("ZipPostalCode")).SendKeys("31000");
            driver.FindElement(By.XPath("//form/div[2]/div/div[2]/div/div[2]")).Click();
            driver.FindElement(By.Name("estimateshipping")).Click();
        }
        [Test]
        public void LogInLogOut()
        {
            driver.Navigate().GoToUrl("http://demowebshop.tricentis.com/");
            driver.FindElement(By.LinkText("Log in")).Click();
            driver.FindElement(By.Id("Email")).Clear();
            driver.FindElement(By.Id("Email")).SendKeys("markomaric@gmail.com");
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("123456789");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Log out")).Click();
        }

        [Test]
        public void ComapreTwoIteamsAndClear()
        {
            driver.Navigate().GoToUrl("http://demowebshop.tricentis.com/");
            driver.FindElement(By.XPath("(//a[contains(text(),'Computers')])[3]")).Click();
            driver.FindElement(By.XPath("(//a[contains(text(),'Desktops')])[4]")).Click();
            driver.FindElement(By.XPath("//img[@alt='Picture of Build your own expensive computer']")).Click();
            driver.FindElement(By.XPath("//input[@value='Add to compare list']")).Click();
            driver.FindElement(By.XPath("(//a[contains(text(),'Computers')])[3]")).Click();
            driver.FindElement(By.XPath("(//a[contains(text(),'Notebooks')])[4]")).Click();
            driver.FindElement(By.XPath("//img[@alt='Picture of 14.1-inch Laptop']")).Click();
            driver.FindElement(By.XPath("//input[@value='Add to compare list']")).Click();
            driver.FindElement(By.LinkText("Clear list")).Click();
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }


    }
}
