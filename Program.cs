// See https://aka.ms/new-console-template for more information
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

class Program
{
  static void Main()
  {
    IWebDriver driver = new ChromeDriver();

    try
    {
      driver.Navigate().GoToUrl("https://www.start.gg");
      
      IWebElement termsButton = driver.FindElement(By.Id("onetrust-accept-btn-handler"));
      termsButton.Click();
     
      Thread.Sleep(2000);
      IWebElement loginButton = driver.FindElement(By.Name("loginOrRegister"));
      loginButton.Click();

      
      Thread.Sleep(1500);
      IWebElement nameField = driver.FindElement(By.Name("loginEmail"));
      nameField.SendKeys("arst49695@gmail.com");

      IWebElement passwordField = driver.FindElement(By.Name("loginPassword"));
      passwordField.SendKeys("arstzxcd1");
   
      IWebElement submitButton = driver.FindElement(By.Name("loginSubmit"));
      submitButton.Click();

      Thread.Sleep(3000);
      driver.Navigate().GoToUrl("https://www.start.gg/create/tournament");

      Thread.Sleep(1000);
      FillInputField(driver, "name", "I Don't Smash #");
      FillInputField(driver, "primaryContact", "arst49695@gmail.com");

      string startTime = DateTime.Today.ToString("MM/dd/yyyy");
      FillInputField(driver, "startAt", startTime + " 07:15 pm");
      FillInputField(driver, "endAt", startTime + " 11:15 pm");

      IWebElement dropDownButton = driver.FindElement(By.XPath("//button[contains(text(), 'Copy Tournament Settings')]"));
      dropDownButton.Click();

      FindElement(driver, By.XPath("//input[@role='combobox' and @aria-activedescendant='react-select-2--value']"), "I Don't Smash #22");
      Console.ReadLine();
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

  static void ClickButton(IWebDriver driver, string element, int wait)
  {
    Thread.Sleep(wait);
    IWebElement button = driver.FindElement(By.Name(element)); 
    button.Click();
  }

  static void FindElement(IWebDriver driver, By locator, string value) 
  {
    IWebElement field = driver.FindElement(locator);
    field.SendKeys(value);
  }
}

