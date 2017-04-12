using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.IO;
using OpenQA.Selenium.Support.UI;

namespace AutomaticTest
{
  [TestClass]
  public class SeleniumUnitTest
  {
    private static readonly IWebDriver webDriver = getDriver();
    private static string rootPage = "http://svyatoslav.biz/testlab/wt/";

    private static IWebDriver getDriver()
    {
      FirefoxOptions firefoxOptions = new FirefoxOptions();
      firefoxOptions.BrowserExecutableLocation = "C:\\Program Files\\Mozilla Firefox\\firefox.exe";
      return new FirefoxDriver(firefoxOptions);
    }

    private static CalculatePage Page
    {
      get
      {
        return new CalculatePage(webDriver);
      }
    }

    private static WebDriverWait WaitWeb
    {
      get
      {
        return new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));
      }
    }

    [TestMethod]
    public void TestOpenStartPage()
    {
      webDriver.Navigate().GoToUrl(rootPage);
      WaitLoadPage();
      Assert.IsTrue(Page.Title.Equals("Расчёт веса"));
    }

    [TestMethod]
    public void TestChangeName()
    {
      webDriver.Navigate().GoToUrl(rootPage);
      WaitLoadPage();
      Page.Name.Clear();
      Page.Name.SendKeys("Text");
      Assert.IsTrue(Page.NameValue.Equals("Text"));
    }

    private static void WaitChangePage()
    {
      WaitWeb.Until(ExpectedConditions.UrlContains("/wt/index.php"));
    }

    private static void WaitLoadPage()
    {
      webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
    }

    [TestMethod]
    public void TestSubmitButton()
    {
      webDriver.Navigate().GoToUrl(rootPage);
      WaitLoadPage();
      Page.CalcButton.Submit();
      WaitLoadPage();
      WaitChangePage();
      Assert.IsTrue(webDriver.Url.Contains("/wt/index.php"));
    }
    [TestMethod]
    public void TestChangeGender()
    {
      webDriver.Navigate().GoToUrl(rootPage);
      WaitLoadPage();
      Assert.IsTrue(Page.Gender.Count == 2);
      foreach (var selector in Page.Gender)
      {
        selector.Click();
        Assert.IsTrue(selector.Selected);
      }
    }

    private void ClearPage()
    {
      Page.Name.Clear();
      Page.Weight.Clear();
      Page.Height.Clear();
    }

    private static string errorNameInfo = "Не указано имя.";

    [TestMethod]
    public void TestSendName()
    {
      webDriver.Navigate().GoToUrl(rootPage);
      WaitLoadPage();
      ClearPage();
      Page.Name.SendKeys("123");
      Page.CalcButton.Submit();
      WaitLoadPage();
      WaitChangePage();
      Assert.IsFalse(Page.ErrorInfo.Text.Contains(errorNameInfo));
    }

    [TestMethod]
    public void TestSendClearName()
    {
      webDriver.Navigate().GoToUrl(rootPage);
      ClearPage();
      Page.CalcButton.Submit();
      WaitLoadPage();
      WaitChangePage();
      Assert.IsTrue(Page.ErrorInfo.Text.Contains(errorNameInfo));
    }

    private void TestResult(string name, string height, string weight, int sender, string checkResult)
    {
      webDriver.Navigate().GoToUrl(rootPage);
      WaitLoadPage();
      ClearPage();
      Page.Name.SendKeys(name);
      Page.Height.SendKeys(height);
      Page.Weight.SendKeys(weight);
      Page.Gender[sender].Click();
      Page.CalcButton.Submit();
      WaitLoadPage();
      WaitChangePage();
      Assert.IsTrue(Page.CalcAnswer.Text.Contains(checkResult));
    }

    [TestMethod]
    public void TestCalculateMediumIndex()
    {
      TestResult("Mone", "180", "70", 0, "Идеальная масса тела");
    }

    [TestMethod]
    public void TestCalculateLowIndex()
    {
      TestResult("Mones", "150", "30", 1, "Слишком малая масса тела");
    }

    [ClassCleanup]
    public static void Cleanup()
    {
      webDriver.Quit();
    }
  }
}
