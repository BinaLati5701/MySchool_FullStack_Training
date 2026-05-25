using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoogleSearchUITests;

[TestClass]
public sealed class SearchRosesTests
{
    private IWebDriver _driver;
    [TestInitialize]
    public void Setup()    
    {
        _driver = new ChromeDriver();
        _driver.Navigate().GoToUrl("https://www.google.com/");
    }
        

    [TestMethod]
    public void SearchRoses()
    {
        //explicit wait to ensure elements are loaded before interacting with them  
        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));        
    
        IWebElement searchBox = _driver.FindElement(By.Name("q"));
        searchBox.SendKeys("Roses");
        searchBox.SendKeys(Keys.Enter);                    
       
        // Verify that the search results page is displayed   
        Assert.IsTrue(_driver.Title.Contains("Roses"));
        
        // Wait for search results to load and verify that they contain the keyword "Roses"
        IReadOnlyCollection<IWebElement> results = wait.Until(driver => driver.FindElements(By.TagName("h3")));
        Assert.IsTrue(results.Count > 0, "No search results were returned.");

        foreach (IWebElement result in results)
        {
            string resultText = result.Text.ToLower();

            Assert.IsTrue(resultText.Contains("rose"), $"Result does not contain keyword: {result.Text}");
        }         
    }

    // [TestCleanup]
    // public void Teardown()
    // {
    //     // Always close browser windows using TestCleanup instead of inside the test method
    //     if (_driver != null)
    //     {
    //         _driver.Quit();        
    //     }
    // }
}
