using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticTest
{
  class CalculatePage
  {
    private readonly IWebDriver webDriver;

    public CalculatePage(IWebDriver webDriver)
    {
      this.webDriver = webDriver;
    }

    public IWebElement Name
    {
      get
      {
        return webDriver.FindElement(By.Name("name"));
      }
    }

    public string NameValue
    {
      get
      {
        return Name.GetAttribute("value");
      }
    }

    public IWebElement Height
    {
      get
      {
        return webDriver.FindElement(By.Name("height"));
      }
    }

    public string HeightValue
    {
      get
      {
        return Height.GetAttribute("value");
      }
    }

    public IWebElement Weight
    {
      get
      {
        return webDriver.FindElement(By.Name("weight"));
      }
    }

    public string WeightValue
    {
      get
      {
        return Weight.GetAttribute("value");
      }
    }

    public string Title
    {
      get
      {
        return webDriver.Title;
      }
    }

    public ReadOnlyCollection<IWebElement> Gender
    {
      get
      {
        return webDriver.FindElements(By.Name("gender"));
      }
    }

    public IWebElement CalcButton
    {
      get
      {
        return webDriver.FindElement(By.TagName("input"));
      }      
    }

    public IWebElement ErrorInfo
    {
      get
      {
        return webDriver.FindElement(By.TagName("form")).FindElements(By.TagName("tr"))[0];
      }
    }

    public IWebElement CalcAnswer
    {
      get
      {
        return webDriver.FindElement(By.TagName("table")).FindElements(By.TagName("tr"))[1];
      }
    }
  }
}
