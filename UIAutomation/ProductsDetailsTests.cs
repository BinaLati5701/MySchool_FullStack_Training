using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace UIAutomation
{
    [TestClass]
    public class ProductsDetailsTests
    {
        // Declare driver at the class level so both Setup and Test can use it
        private IWebDriver driver;

        [TestInitialize]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();

            options.AddArgument("--incognito");
            options.AddArgument("--start-maximized");

            // popup / ads protection
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--disable-notifications");

            // Google ads / vignette mitigation
            options.AddArgument("--disable-site-isolation-trials");
            options.AddArgument("--disable-features=OptimizationHints");
            options.AddArgument("--disable-features=InterestFeedContentSuggestions");
            options.AddArgument("--disable-features=NotificationTriggers");

            // removes some automation banners/interference
            options.AddExcludedArgument("enable-automation");
            options.AddAdditionalOption("useAutomationExtension", false);

            // browser permissions
            options.AddUserProfilePreference("profile.default_content_setting_values.notifications", 2);

            options.AddUserProfilePreference("profile.default_content_setting_values.geolocation", 2);

            driver = new ChromeDriver(options);
        }

        //Helper method. Use it after every click if page loads with ads and pop ups which causes url to change the endpoint
        public void HandleGoogleVignette()
        {
            if (driver.Url.Contains("#google_vignette"))
            {
                //driver.Navigate().Back();
                driver.Navigate().Refresh();
            }
        }

        [TestMethod]
        public void VerifyProductDetailsAreVisible()
        {  
            //windows resolution          
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://automationexercise.com");

            // Explicit wait to handle page rendering safely
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            
            // Validate homepage image
            Assert.IsTrue(driver.FindElement(By.CssSelector("img[alt='Website for automation practice']")).Displayed);
           
           //Products button locator on home page
            IWebElement productsButton = driver.FindElement(By.XPath("//a[contains(text(),'Products')]"));
            
            //Action- click products button to navigate to products page
            productsButton.Click();

            // Validate Products Page
            wait.Until(d => d.Url.Contains("/products"));
            Assert.IsTrue(driver.FindElement(By.XPath("//h2[text()='All Products']")).Displayed);
            Assert.IsTrue(driver.FindElement(By.ClassName("features_items")).Displayed);

            //helper method used to handle google vignette 
            HandleGoogleVignette();
            
            // View Product button locator for the first product in the list
            IWebElement viewButton = driver.FindElement(By.XPath("(//a[contains(text(),'View Product')])[1]"));
            
            // Click View Product to navigate to product details page
            viewButton.Click();            

            // Validate Product Details Page
            wait.Until(d => d.Url.Contains("/product_details/1"));
            IWebElement productInfo = driver.FindElement(By.ClassName("product-information"));

            //Verify that detail detail is visible: product name, category, price, availability, condition, brand
            Assert.IsTrue(productInfo.Text.Contains("Blue Top"));
            Assert.IsTrue(productInfo.Text.Contains("Category: Women > Tops"));
            Assert.IsTrue(productInfo.Text.Contains("Rs. 500"));
            Assert.IsTrue(productInfo.Text.Contains("Availability: In Stock"));
            Assert.IsTrue(productInfo.Text.Contains("Condition: New"));
            Assert.IsTrue(productInfo.Text.Contains("Brand: Polo"));
        }

        [TestCleanup]
        public void Teardown()
        {
            // Always close browser windows using TestCleanup instead of inside the test method
            if (driver != null)
            {
                driver.Quit();
            }
        }
    }
}