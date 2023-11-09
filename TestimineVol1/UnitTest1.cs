using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Threading;

namespace Testimine
{
    public class Tests
    {
        IWebDriver driver;
        String test_url = "http://prelive.aptimea.com/form/questionnaire";
        private Random _random = new Random();

        [SetUp]
        public void start_browser()
        {
            driver = new ChromeDriver(@"C:\Program Files (x86)\Google\Chrome\Application");
            driver.Manage().Window.Maximize();
        }

         [Test]
        public void test_page1()
        {
            driver.Url = test_url;
            driver.Navigate().GoToUrl("http://prelive.aptimea.com/form/questionnaire");

            Thread.Sleep(2500);

            try { IWebElement sButton2 = driver.FindElement(By.XPath("//button[@class='agree-button eu-cookie-compliance-secondary-button']"));
                IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                executor.ExecuteScript("arguments[0].click();", sButton2);
                
            } catch (Exception) { }

            for (int a = 0; a < 10; a++)
            {
                Thread.Sleep(2500);
                var sRadio = driver.FindElements(By.XPath("//div[@class='fieldset-wrapper']"));
                for (int i = 0; i < sRadio.Count; i++)
                {
                    var els = sRadio[i].FindElements(By.XPath(".//input[@type='radio']"));
                    if (els.Count >= 2)
                    {
                        try { 
                            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                            executor.ExecuteScript("arguments[0].click();", els[_random.Next(0, els.Count)]);

                        } catch (Exception) { }
                    }
                }
                var sText = driver.FindElements(By.XPath("//input[@type='text']"));
                for (int i = 0; i < sText.Count; i++)
                {
                    try { 
                        sText[i].SendKeys("LambdaTest");
                    } catch (Exception) { }
                }
                var sTextArea = driver.FindElements(By.XPath("//textarea"));
                for (int i = 0; i < sTextArea.Count; i++)
                {
                    try { 
                        sTextArea[i].SendKeys("LambdaTest");
                    } catch (Exception) { }
                }
                var sNum = driver.FindElements(By.XPath("//input[@type='number']"));
                for (int i = 0; i < sNum.Count; i++)
                {
                    try {
                        sNum[i].SendKeys("21"); 
                    } catch (Exception) { }
                }
                var sSelect = driver.FindElements(By.XPath("//select"));
                for (int i = 0; i < sNum.Count; i++)
                {
                    try {
                        sSelect[i].FindElements(By.XPath(".//*"))[3].Click();
                        IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                        executor.ExecuteScript("arguments[0].click();", sSelect[i]);
                    } catch (Exception) { }
                }
                IWebElement sButton = driver.FindElement(By.XPath("//*[@value='Suivant']"));
                try {
                    IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                    executor.ExecuteScript("arguments[0].click();", sButton);
                    //sButton.Click();
                } catch (Exception) { }
            }
            Thread.Sleep(2500);

            ErrorCheck("Weight", "1000", "en kg");
            ErrorCheck("Height", "300", "en cm");
            ErrorCheck("Childern", "50", "d'enfants");
       



            IWebElement sButton3 = driver.FindElement(By.XPath("//*[@value='Finaliser']"));
            try {
                IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                executor.ExecuteScript("arguments[0].click();", sButton3);
            
            } catch (Exception) { }

            Thread.Sleep(2500);

            IWebElement sButton4 = driver.FindElement(By.XPath("//a[@href='/user/login']"));
            try {
                IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                executor.ExecuteScript("arguments[0].click();", sButton4);
            } catch (Exception) { }

            Thread.Sleep(2500);
        }


        public void ErrorCheck(String about, String amount, String unit)
        {
            var errorMessage = driver.FindElements(By.XPath("//*[@class = 'item item--message']"));
            bool errorFound = false;
            for (int i = 0; i < errorMessage.Count; i++)
            {
                if (errorMessage[i].Text.Contains(amount) && errorMessage[i].Text.Contains(unit))
                {
                    errorFound = true;
                }
            }
            if (errorFound)
            {
                Console.WriteLine(about + " error was correct.");
            }
            if (!errorFound)
            {
                Console.WriteLine(about + " error was incorrect or missing");
            }
        }



        [TearDown]
        public void close_Browser()
        {
            driver.Quit();
        }
    }
}