using System;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WebAddressBookTests;

namespace sol2attampt1
{
    [Binding]
    public class LoginSteps
    {
        private ApplicationManager App => ApplicationManager.GetInstance();

        [Given(@"Given a user is logged out")]
        public void GivenGivenAUserIsLoggedOut()
        {
            App.Auth.Logout();
        }

        [When(@"I login with username ""(.*)"" and password ""(.*)""")]
        public void WhenWhenILoginWithValidCredentials(string username ,string password)
        {
            var account = new AccountData(username, password);
            ScenarioContext.Current.Add("account", account);
            App.Auth.Login(account);
        }

        //[When(@"When I login with invalid credentials")]  эта строка мне уже не нужна 
        //public void WhenWhenILoginWithInvalidCredentials() т.к реализация точно такая же как и в login with valid credantials
        //{                                                  а данные поставляем прямо в строке When I login ..... 
        //    var account = new AccountData("admin", "123456");
        //    ScenarioContext.Current.Add("account", account);
        //    App.Auth.Login(account);
        //}

        [Then(@"I have logged in")]
        public void ThenIHaveLoggedIn()
        {
            var account = ScenarioContext.Current.Get<AccountData>("account");
            Assert.IsTrue(App.Auth.IsLoggedIn(account));
        }

        [Then(@"I have not logged in")]
        public void ThenIHaveNotLoggedIn()
        {
            var account = ScenarioContext.Current.Get<AccountData>("account");
            Assert.IsFalse(App.Auth.IsLoggedIn(account));
        }
    }
}
