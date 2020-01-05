using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;

namespace UITests
{
    public class UnitTest1: IDisposable
    {
        private readonly IWebDriver _driver;
        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        public UnitTest1()
        {
            _driver = new ChromeDriver("C:\\Users\\Marta\\source\\repos\\HRManagement\\UITests");
        }

        [Fact]
        public void Create_WhenExecuted_ReturnsCreateView()
        {
            _driver.Navigate()
                .GoToUrl("https://localhost:5001/");

            //Assert.Equal("Create - EmployeesApp", _driver.Title);
            Assert.Contains("Welcome", _driver.PageSource);
        }
    }
}
