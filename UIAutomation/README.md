UI Automation Practice Project
Website Under Test (SUT)

Practice website used for UI Automation learning:

Automation Exercise Test Cases: https://automationexercise.com/test_cases

This project automates Test Case 8: Verify All Products and product detail page

Test Case 8
Steps
Launch browser
Navigate to URL https://automationexercise.com
Verify that home page is visible successfully
Click on Products button
Verify user is navigated to ALL PRODUCTS page successfully
Verify products list is visible
Click on View Product of first product
Verify user is landed to product detail page
Verify that detail is visible:
Product name
Category
Price
Availability
Condition
Brand
Technologies Used
C#
.NET 6
Selenium WebDriver
MSTest Framework
ChromeDriver
Visual Studio
Required NuGet Packages

Install these packages through Visual Studio NuGet Package Manager.

MSTest Packages
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.7.1" />
<PackageReference Include="MSTest.TestAdapter" Version="3.11.1" />
<PackageReference Include="MSTest.TestFramework" Version="3.11.1" />
Selenium Packages
<PackageReference Include="Selenium.WebDriver" Version="4.1.1" />
<PackageReference Include="Selenium.WebDriver.ChromeDriver" Version="148.0.7778.17800" />
Important Notes About This Website

This practice website is intentionally unstable and contains:

Popups
Ads
Slow loading
Random UI behavior
High traffic/load issues

Because of this, tests may fail even if the automation code is correct.

Examples:

Popups may block buttons
Ads may cover elements
Pages may load slowly
Website may temporarily return errors

This is normal for practice environments.

Why XPath Was Used In Some Locators

Normally, CSS Selectors are preferred because they are:

Cleaner
Faster
Easier to read

However, on this specific website:

Popups appear very quickly
Ads interrupt execution
Slow locators may allow popups to appear before the click action happens

Because of this, some XPath locators were used to help Selenium move faster through the page before popups interfere.

Example:

driver.FindElement(By.XPath("(//a[contains(text(),'View Product')])[1]")).Click();

This is not always the best enterprise approach, but for unstable practice websites it may help tests execute faster.

Selenium Concepts Used
IWebDriver

IWebDriver is the main Selenium interface used to control the browser.

Example:

IWebDriver driver = new ChromeDriver();

The driver:

Opens browser
Navigates pages
Finds elements
Performs actions
Initialization

Initialization means creating the browser object before the test starts.

Example:

IWebDriver driver = new ChromeDriver();

This launches Chrome browser for automation.

Setup

Setup contains actions required before test execution.

Typical setup actions:

Open browser
Maximize window
Navigate to URL

Example:

driver.Manage().Window.Maximize();

driver.Navigate().GoToUrl("https://automationexercise.com");
TearDown

TearDown is cleanup after test execution.

Purpose:

Close browser
Stop ChromeDriver process
Free memory/resources

Example:

driver.Close();
driver.Quit();
Difference

Close()

Closes current browser tab/window

Quit()

Completely shuts down Selenium session and browser process

Usually both are used.

Explicit Wait

Explicit Wait waits until a specific condition becomes true.

Useful for:

Slow pages
Dynamic elements
Popups
Delayed loading

Example:

WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

wait.Until(ExpectedConditions.ElementIsVisible(By.Id("login")));

Without waits:

Selenium may move too fast
Element may not yet exist
Tests become flaky
Assertions Used
Assert.AreEqual()

Used to compare expected vs actual values.

Example:

Assert.AreEqual(expectedTitle, actualTitle);
Assert.IsTrue()

Used to validate boolean conditions.

Example:

Assert.IsTrue(productInfo.Displayed);
Beginner Skills Practiced

Students practice:

CSS Selectors
XPath
Assertions
Browser navigation
Element interaction
Debugging
Reading HTML structure
Understanding DOM
Working with unstable UI
Debugging In Visual Studio

Useful debugging tools:

Breakpoints
Step Into
Step Over
Watch Window
Console.WriteLine()
Test Explorer
Debug Test

Example:

Console.WriteLine(driver.Title);
Example Test Method
VerifyProductDetailsAreVisible()
Running Tests In Visual Studio
Build Solution
Open Test Explorer
Run Test
Debug Test if needed
Learning Goal

The goal is not only passing tests.

The goal is learning how to:

Read HTML
Understand DOM structure
Create stable locators
Debug failures
Analyze application behavior
Think like a QA Engineer