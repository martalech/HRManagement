using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Threading;
//using System.Web.Mvc;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using WebApplication3.Controllers;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.EntityFrameworkCore.Infrastructure;

namespace UnitTests
{
    //public class UnitTest2
    //{
    //    private readonly IServiceProvider serviceProvider;
    //    public UnitTest2()
    //    {
    //        //var services = new ServiceCollection();
    //        //var connection = Configuration["Server=(localdb)\\mssqllocaldb;Database=HRDatabase;Trusted_Connection=True;"];
    //        //services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));
    //        //services.AddEntityFrameworkSqlServer().AddDbContext<ApplicationDbContext>(o => o.UseInMemoryDatabase());
    //    }
    //}
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
        public static bool ApplyForAJob(IWebDriver driver, string firstName, string lastName, string phoneNumber, string cvUrl, string emailAddress)
        {
            while (true)
            {
                try
                {
                    var apply = driver.FindElement(By.XPath("/html/body/div/main/a[2]"));
                    apply.Click();
                    var fName = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div[1]/textarea"));
                    fName.Click();
                    fName.SendKeys(firstName);
                    Thread.Sleep(1000);
                    var lName = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div[2]/textarea"));
                    lName.Click();
                    lName.SendKeys(lastName);
                    Thread.Sleep(1000);
                    var pN = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div[3]/textarea"));
                    pN.Click();
                    pN.SendKeys(phoneNumber);
                    Thread.Sleep(1000);
                    var cvU = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div[5]/textarea"));
                    cvU.Click();
                    cvU.SendKeys(cvUrl);
                    Thread.Sleep(1000);
                    var em = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div[6]/textarea"));
                    em.Click();
                    em.SendKeys(emailAddress);
                    Thread.Sleep(1000);
                    var save = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/button"));
                    save.Click();
                    return true;
                }
                catch
                {
                    //return false;
                }
            }
            return false;
        }
        public static bool SprawdzamyDane(IWebDriver driver, string jobTitle, string company, string location, string salary, string description)
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
            //dane
            var jobTitleT = "dentist";
            var fName = "Ela";
            var lName = "Kowal";
            var phoneN = "736284732";
            var cvUrl = "https://www.elo.pl";
            var emailA = "aak@op.pl";
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
            int superFlaga = 0;
            //przegladamy strony z ofertami
            while (true)
            {
                try
                {
                    var pasek = driver.FindElement(By.XPath("/html/body/div/main/div/table/tr[2]/td"));
                    var spany = pasek.FindElements(By.TagName("span"));
                    for (int i = 0; i < spany.Count; i++)
                    {
                        while (true)
                        {
                            try
                            {
                                var jobT = driver.FindElement(By.XPath("/html/body/div/main/div/table/tr[1]/td/a"));
                                if (jobT.Text == jobTitleT)
                                {
                                    jobT.Click();
                                    //aplikujemy

                                    var apl = ApplyForAJob(driver, fName, lName, phoneN, cvUrl, emailA);

                                    if (apl) superFlaga = 1;

                                    //else superFlaga = 2;
                                    ////wracamy do wszsytkich jobOffers
                                    //if (superFlaga != 1) driver.FindElement(By.XPath("/html/body/div/main/a")).Click();
                                    break;
                                }
                                else
                                {
                                    pasek = driver.FindElement(By.XPath("/html/body/div/main/div/table/tr[2]/td"));
                                    spany = pasek.FindElements(By.TagName("span"));
                                    var nastepny = spany[(i + 1) % spany.Count];
                                    if (i != spany.Count - 1) nastepny.Click();

                                }
                            }
                            catch
                            {

                            }
                            //if (superFlaga == 2)
                            //    break;
                        }
                        if (superFlaga == 1)
                            break;
                    }
                    if (superFlaga == 1)
                        break;
                }
                catch
                {

                }
            }
            bool superFlaga2 = false;
            //przegladamy tabele czy jest nasz tr
            while (true)
            {
                try
                {
                    var tbody = driver.FindElement(By.XPath("/html/body/div/main/table/tbody"));
                    var trs = tbody.FindElements(By.TagName("tr"));
                    for (int i = 1; i < trs.Count; i++)
                    {
                        if (SprawdzTr(driver, i, fName, lName, phoneN, emailA))
                        {
                            superFlaga2 = true;
                            break;
                        }
                    }
                }
                catch
                {

                }
                if (superFlaga2) break;
            }

            Thread.Sleep(5000);
        }
        public static bool SprawdzTr(IWebDriver driver, int i, string firstName, string lastName, string phoneNumber, string emailAddress)
        {
            var tbody = driver.FindElement(By.XPath("/html/body/div/main/table/tbody"));
            var trs = tbody.FindElements(By.TagName("tr"));
            var tds = trs[i].FindElements(By.TagName("td"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", trs[i]);

            if (tds[0].Text != firstName) return false;
            if (tds[1].Text != lastName) return false;
            if (tds[2].Text != phoneNumber) return false;
            if (tds[3].Text != emailAddress) return false;
            return true;
        }
        [Fact]
        public void AddNewCompanyTest()
        {
            driver.Navigate().GoToUrl("https://localhost:5001");
            //dane
            var name = "Apple";
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
            bool znaleziono = false;
            while (true)
            {
                try
                {
                    var lista = driver.FindElement(By.XPath("/html/body/div/main/ul"));
                    var lis = lista.FindElements(By.TagName("li"));
                    for (int i = 0; i < lis.Count; i++)
                    {
                        if (lis[i].Text == name)
                        {
                            lista = driver.FindElement(By.XPath("/html/body/div/main/ul"));
                            lis = lista.FindElements(By.TagName("li"));
                            var a = lis[i].FindElement(By.TagName("a"));
                            a.Click();
                            Thread.Sleep(1000);
                            znaleziono = true;
                            break;
                        }
                    }
                    if (znaleziono) break;
                }
                catch
                {

                }
            }
            Thread.Sleep(5000);
        }
        //create new job offer test
        [Fact]
        public void CreateNewJobOfferTest()
        {
            //dane
            var jobTitleT = "ASP .NET MVC Developer";
            var companyT = "Marta Company";
            var locationT = "Warsaw";
            var salaryT = "4000";
            var descriptionT = "Best job ever";
            //
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
                    //var optionAla = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div[2]/select/option[4]"));
                    //optionAla.Click();
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
            //jesli sie dodalo to klikamy w ostatnia strone pagingu offers i w offer
            //superflaga 0 - nie znaleziono, 1 - znaleziono, 2 - szukaj dalej
            int superFlaga = 0;
            while (true)
            {
                try
                {
                    var pasek = driver.FindElement(By.XPath("/html/body/div/main/div/table/tr[2]/td"));
                    var spany = pasek.FindElements(By.TagName("span"));
                    for (int i = 0; i < spany.Count; i++)
                    {
                        while (true)
                        {
                            try
                            {
                                var jobT = driver.FindElement(By.XPath("/html/body/div/main/div/table/tr[1]/td/a"));
                                if (jobT.Text == jobTitleT)
                                {
                                    jobT.Click();
                                    //sprawdzamy czy wszystkie dane sie zgadzaja 
                                    var sprawdzenie = SprawdzamyDane(driver, jobTitleT, companyT, locationT, salaryT, descriptionT);

                                    if (sprawdzenie) superFlaga = 1;

                                    else superFlaga = 2;
                                    //wracamy do wszsytkich jobOffers
                                    if (superFlaga != 1) driver.FindElement(By.XPath("/html/body/div/main/a")).Click();
                                    break;
                                }
                                else
                                {
                                    pasek = driver.FindElement(By.XPath("/html/body/div/main/div/table/tr[2]/td"));
                                    spany = pasek.FindElements(By.TagName("span"));
                                    var nastepny = spany[(i + 1) % spany.Count];
                                    if (i != spany.Count - 1) nastepny.Click();

                                }
                            }
                            catch
                            {

                            }
                            //if (superFlaga == 2)
                            //    break;
                        }
                        if (superFlaga == 1)
                            break;
                    }
                    if (superFlaga == 1)
                        break;
                }
                catch
                {

                }
            }
            Thread.Sleep(5000);
            //wracamy do wszsytkich jobOffers
            driver.FindElement(By.XPath("/html/body/div/main/a")).Click();
            //Console.WriteLine(driver.PageSource);
        }
    }
}
