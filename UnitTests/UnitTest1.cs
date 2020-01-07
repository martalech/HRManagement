using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace UnitTests
{
    public class UnitTest1 : IDisposable
    {
        private readonly IWebDriver driver;
        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
        public UnitTest1()
        {
            var dir = Directory.GetCurrentDirectory();
            driver = new ChromeDriver(dir);
        }
        public static bool ApplyForAJob(IWebDriver driver, string firstName, string lastName, string phoneNumber, string cvUrl, string emailElddress)
        {
            while (true)
            {
                try
                {
                    var apply = driver.FindElement(By.XPath("/html/body/div/main/a[2]"));
                    apply.Click();
                    var fristNameEl = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div[1]/textarea"));
                    fristNameEl.Click();
                    fristNameEl.SendKeys(firstName);
                    Thread.Sleep(1000);
                    var lastNameEl = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div[2]/textarea"));
                    lastNameEl.Click();
                    lastNameEl.SendKeys(lastName);
                    Thread.Sleep(1000);
                    var phoneNumberEl = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div[3]/textarea"));
                    phoneNumberEl.Click();
                    phoneNumberEl.SendKeys(phoneNumber);
                    Thread.Sleep(1000);
                    var cvUrlEl = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div[5]/textarea"));
                    cvUrlEl.Click();
                    cvUrlEl.SendKeys(cvUrl);
                    Thread.Sleep(1000);
                    var emailEl = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div[6]/textarea"));
                    emailEl.Click();
                    emailEl.SendKeys(emailElddress);
                    Thread.Sleep(1000);
                    var save = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/button"));
                    save.Click();
                    return true;
                }
                catch
                {
                }
            }
            return false;
        }
        public static bool CheckData(IWebDriver driver, string jobTitle, string company, string location, string salary, string description)
        {
            var jT = driver.FindElement(By.XPath("/html/body/div/main/div[1]/div[2]/div/div/h4"));
            if (jT.Text != jobTitle) return false;
            var dC = driver.FindElement(By.XPath("/html/body/div/main/div[1]/div[2]/div/div/div[1]"));
            if (dC.Text != description) return false;
            var cP = driver.FindElement(By.XPath("/html/body/div/main/div[1]/div[2]/div/div/div[2]"));
            if (cP.Text != company) return false;
            var lN = driver.FindElement(By.XPath("/html/body/div/main/div[1]/div[2]/div/div/div[3]"));
            if (lN.Text != location) return false;
            var sR = driver.FindElement(By.XPath("/html/body/div/main/div[1]/div[2]/div/div/div[4]"));
            if (sR.Text.Substring(0, sR.Text.Length - 6) != salary) return false;
            return true;
        }
        [Fact]
        public void ApplyForAJobTest()
        {
            driver.Navigate().GoToUrl("https://localhost:5001");
            var jobTitleT = "C# Developer";
            var fristNameEl = "Jan";
            var lastNameEl = "Kowalski";
            var phoneN = "278987456";
            var cvUrlEl = "https://www.example.pl";
            var emailEl = "cutiepiemarta@gmail.com";
            while (true)
            {
                try
                {
                    var jobOffers = driver.FindElement(By.XPath("/html/body/header/nav/div/div/ul/li[2]/a"));
                    jobOffers.Click();
                    break;
                }
                catch
                {
                }
            }
            Thread.Sleep(1000);
            int flag = 0;

            while (true)
            {
                try
                {
                    var bar = driver.FindElement(By.XPath("/html/body/div/main/div/table/tr[2]/td"));
                    var spans = bar.FindElements(By.TagName("span"));
                    for (int i = 0; i < spans.Count; i++)
                    {
                        while (true)
                        {
                            try
                            {
                                var jobT = driver.FindElement(By.XPath("/html/body/div/main/div/table/tr[1]/td/a"));
                                if (jobT.Text == jobTitleT)
                                {
                                    jobT.Click();

                                    var apl = ApplyForAJob(driver, fristNameEl, lastNameEl, phoneN, cvUrlEl, emailEl);

                                    if (apl) flag = 1;

                                    break;
                                }
                                else
                                {
                                    bar = driver.FindElement(By.XPath("/html/body/div/main/div/table/tr[2]/td"));
                                    spans = bar.FindElements(By.TagName("span"));
                                    var next = spans[(i + 1) % spans.Count];
                                    if (i != spans.Count - 1) next.Click();

                                }
                            }
                            catch
                            {
                            }
                        }
                        if (flag == 1)
                            break;
                    }
                    if (flag == 1)
                        break;
                }
                catch
                {
                }
            }
            bool flag2 = false;
            
            while (true)
            {
                try
                {
                    var tbody = driver.FindElement(By.XPath("/html/body/div/main/table/tbody"));
                    var trs = tbody.FindElements(By.TagName("tr"));
                    for (int i = 1; i < trs.Count; i++)
                    {
                        if (CheckTr(driver, i, fristNameEl, lastNameEl, phoneN, emailEl))
                        {
                            flag2 = true;
                            break;
                        }
                    }
                }
                catch
                {
                }
                if (flag2) break;
            }

            Thread.Sleep(5000);
        }
        public static bool CheckTr(IWebDriver driver, int i, string firstName, string lastName, string phoneNumber, string emailElddress)
        {
            var tbody = driver.FindElement(By.XPath("/html/body/div/main/table/tbody"));
            var trs = tbody.FindElements(By.TagName("tr"));
            var tds = trs[i].FindElements(By.TagName("td"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", trs[i]);

            if (tds[0].Text != firstName) return false;
            if (tds[1].Text != lastName) return false;
            if (tds[2].Text != phoneNumber) return false;
            if (tds[3].Text != emailElddress) return false;
            return true;
        }
        [Fact]
        public void CompanyTest()
        {
            driver.Navigate().GoToUrl("https://localhost:5001");
            
            var name = "Super Company";
            while (true)
            {
                try
                {
                    var companies = driver.FindElement(By.XPath("/html/body/header/nav/div/div/ul/li[3]/a"));
                    companies.Click();
                    break;
                }
                catch
                {
                }
            }
            Thread.Sleep(1000);
            while (true)
            {
                try
                {
                    var create = driver.FindElement(By.XPath("/html/body/div/main/a"));
                    create.Click();
                    break;
                }
                catch
                {
                }
            }
            Thread.Sleep(1000);
            while (true)
            {
                try
                {
                    var nameInput = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div[1]/input"));
                    nameInput.Click();
                    nameInput.SendKeys(name);
                    break;
                }
                catch
                {
                }
            }
            Thread.Sleep(1000);
            while (true)
            {
                try
                {
                    var create = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div[2]/button"));
                    create.Click();
                    break;
                }
                catch
                {
                }
            }
            Thread.Sleep(1000);
            bool found = false;
            while (true)
            {
                try
                {
                    var tbody = driver.FindElement(By.XPath("/html/body/div/main/table/tbody"));
                    var trs = tbody.FindElements(By.TagName("tr"));
                    for (int i = 1; i < trs.Count; i++)
                    {
                        var td = trs[i].FindElement(By.TagName("td"));
                        var a = td.FindElement(By.TagName("a"));
                        if (a.Text == name)
                        {
                            found = true;
                            break;
                        }
                    }
                    //var list = driver.FindElement(By.XPath("/html/body/div/main/ul"));
                    //var lis = list.FindElements(By.TagName("li"));
                    //for (int i = 0; i < lis.Count; i++)
                    //{
                    //    if (lis[i].Text == name)
                    //    {
                    //        list = driver.FindElement(By.XPath("/html/body/div/main/ul"));
                    //        lis = list.FindElements(By.TagName("li"));
                    //        var a = lis[i].FindElement(By.TagName("a"));
                    //        a.Click();
                    //        Thread.Sleep(1000);
                    //        found = true;
                    //        break;
                    //    }
                    //}
                    if (found)
                        break;
                }
                catch
                {
                }
            }
            Thread.Sleep(5000);
        }
        
        [Fact]
        public void CreateNewJobOfferTest()
        {
            var jobTitleT = "ASP .NET MVC Developer";
            var companyT = "Marta Company";
            var locationT = "Warsaw";
            var salaryT = "4000";
            var descriptionT = "Description..";
            
            var executor = driver as IJavaScriptExecutor;
            driver.Navigate().GoToUrl("https://localhost:5001");
            Assert.Contains("Welcome", driver.PageSource);

            Thread.Sleep(1000);
            while (true)
            {
                try
                {
                    var jobOffers = driver.FindElement(By.XPath("/html/body/header/nav/div/div/ul/li[2]/a"));
                    jobOffers.Click();
                    break;
                }
                catch
                {
                }
            }
            Thread.Sleep(1000);
            while (true)
            {
                try
                {
                    var createNewJobOffer = driver.FindElement(By.XPath("/html/body/div/main/button"));
                    createNewJobOffer.Click();
                    break;
                }
                catch
                {
                }
            }
            while (true)
            {
                try
                {
                    var jobTitle = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div[1]/input"));
                    jobTitle.Click();
                    jobTitle.Clear();
                    jobTitle.SendKeys(jobTitleT);
                    Thread.Sleep(1000);
                    var companySelect = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div[2]/select"));
                    foreach (var el in companySelect.FindElements(By.TagName("option")))
                    {
                        if (el.Text == companyT)
                        {
                            el.Click();
                        }
                    }
                    Thread.Sleep(1000);
                    var location = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div[3]/input"));
                    location.Click();
                    location.SendKeys(locationT);
                    Thread.Sleep(1000);
                    var salary = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div[4]/div/input"));
                    salary.Click();
                    salary.Clear();
                    salary.SendKeys(salaryT);
                    Thread.Sleep(1000);
                    var description = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div[5]/textarea"));
                    description.SendKeys(descriptionT);
                    Thread.Sleep(1000);
                    var contractType = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div[6]/input"));
                    contractType.Click();
                    contractType.SendKeys("Full - time");
                    Thread.Sleep(1000);
                    break;
                }
                catch
                {
                }
            }
            while (true)
            {
                try
                {
                    var create = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/button"));
                    create.Click();
                    break;
                }
                catch
                {
                }
            }


            Thread.Sleep(2000);
            int flag = 0;
            while (true)
            {
                try
                {
                    var bar = driver.FindElement(By.XPath("/html/body/div/main/div/table/tr[2]/td"));
                    var spans = bar.FindElements(By.TagName("span"));
                    for (int i = 0; i < spans.Count; i++)
                    {
                        while (true)
                        {
                            try
                            {
                                var jobT = driver.FindElement(By.XPath("/html/body/div/main/div/table/tr[1]/td/a"));
                                if (jobT.Text == jobTitleT)
                                {
                                    jobT.Click();
                                    var check = CheckData(driver, jobTitleT, companyT, locationT, salaryT, descriptionT);

                                    if (check) flag = 1;
                                    else flag = 2;

                                    if (flag != 1)
                                        driver.FindElement(By.XPath("/html/body/div/main/a")).Click();
                                    break;
                                }
                                else
                                {
                                    bar = driver.FindElement(By.XPath("/html/body/div/main/div/table/tr[2]/td"));
                                    spans = bar.FindElements(By.TagName("span"));
                                    var next = spans[(i + 1) % spans.Count];
                                    if (i != spans.Count - 1) next.Click();

                                }
                            }
                            catch
                            {
                            }
                        }
                        if (flag == 1)
                            break;
                    }
                    if (flag == 1)
                        break;
                }
                catch
                {
                }
            }
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("/html/body/div/main/a")).Click();
        }
    }
}
