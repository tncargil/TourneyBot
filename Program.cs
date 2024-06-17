// See https://aka.ms/new-console-template for more information
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

class Program
{
  static void Main()
  {
    IWebDriver driver = new ChromeDriver();
    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

    var config = getConfigValues();

    try
    {
      driver.Navigate().GoToUrl("https://www.start.gg");
      
      ClickButton(wait, By.Id("onetrust-accept-btn-handler"));
     
      Thread.Sleep(1500);
      ClickButton(wait, By.Name("loginOrRegister"));
      
      Thread.Sleep(1500);
      FillInputField(wait, By.Name("loginEmail"), config["Email"]);

      FillInputField(wait, By.Name("loginPassword"), config["Password"]);

      ClickButton(wait, By.Name("loginSubmit"));

      Thread.Sleep(3000);
      driver.Navigate().GoToUrl("https://www.start.gg/create/tournament");

      FillInputField(wait, By.Name("name"), "Test");
      FillInputField(wait, By.Name("primaryContact"), "arst49695@gmail.com");

      string startTime = DateTime.Today.ToString("MM/dd/yyyy");
      FillInputField(driver, "startAt", startTime + " 07:15 pm");
      FillInputField(driver, "endAt", startTime + " 11:15 pm");

      ClickButton(wait, By.XPath("//button[contains(text(), 'Copy Tournament Settings')]"));
      
      FillInputField(wait, By.XPath("//input[@role='combobox' and @aria-activedescendant='react-select-2--value']"), "test");

      Console.WriteLine("Please complete the Captcha and click submit. Then press any key to continue.");
      Console.ReadLine();
      
      //Click Homepage
      ClickButton(wait, By.XPath("//*[@id='main']/div/div/div/div[2]/div[2]/div/section[1]/header/div/button"));
      ClickButton(wait, By.XPath("/html/body/div[6]/div[2]/div/div/form/div[2]/div/div/div[1]/label/section/div/div[1]/div/div/div[2]"));
      ClickButton(wait, By.XPath("//*[@id='mui-component-select-isListed']/div/div/div/div"));
      ClickButton(wait, By.XPath("//*[@id='menu-isListed']/div[3]/ul/li[3]/div/div[1]/div"));
      ClickButton(wait, By.XPath("/html/body/div[6]/div[2]/div/div/form/div[3]/div/div/div/div/button"));
      Thread.Sleep(2000); 

      //Events
      ClickButton(wait, By.XPath("//*[@id='main']/div/div/div/div[2]/div[2]/div/section[2]/header/div/button"));
      ClickButton(wait, By.XPath("/html/body/div[6]/div[2]/div/div/form/div[2]/div/div[1]/div/div[1]/label/section/div/div[1]/div/div/span/div[2]"));
      ClickButton(wait, By.XPath("/html/body/div[6]/div[2]/div/div/form/div[3]/div/div/div/div/button"));

      //Click Registration
      ClickButton(wait, By.XPath("//*[@id='main']/div/div/div/div[2]/div[2]/div/section[3]/header/div/button"));
      ClickButton(wait, By.XPath("/html/body/div[6]/div[2]/div/div/form/div[2]/div/div/div[1]/label/section/div/div[1]/div/div/div[2]/span/div[2]"));
      ClickButton(wait, By.XPath("/html/body/div[6]/div[2]/div/div/form/div[3]/div/div/div/div/button"));
      
    }
    catch(Exception ex) 
    {
      Console.WriteLine("Skill issue occured: " + ex.Message);
    }
    finally
    {
     // driver.Quit();
    }
  }

  static void FillInputField(IWebDriver driver, string element, string value) 
  {
    IWebElement field = driver.FindElement(By.Name(element));
    field.SendKeys(value);
  }

  static void ClickButton(WebDriverWait wait, By locator)
  {
    IWebElement button = wait.Until(ExpectedConditions.ElementToBeClickable(locator)); 
    button.Click();
  }

  static void FillInputField(WebDriverWait wait, By locator, string value) 
  {
    IWebElement field = wait.Until(ExpectedConditions.ElementToBeClickable(locator));
    field.SendKeys(value);
  }
  
  static Dictionary<string, string> getConfigValues() 
  {
    var yaml = File.ReadAllText("config.yaml");

    var deserializer = new DeserializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance)
        .Build();

    return deserializer.Deserialize<Dictionary<string, string>>(yaml);
  }

}

