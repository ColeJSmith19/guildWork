using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SGBank.BLL;
using SGBank.BLL.DepositRules;
using SGBank.BLL.WithdrawRules;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;

namespace SGBankTest
{
    [TestFixture]
    public class FreeAccountTests
    {
        [Test]
        public void CanLoadFreeAccountTestData()
        {
            AccountManager manager = AccountManagerFactory.Create();

            AccountLookupResponse response = manager.LookupAccount("12345");
            Assert.IsNotNull(response.Account);
            Assert.IsTrue(response.Success);
            Assert.AreEqual("12345", response.Account.AccountNumber);
        }

        [TestCase("12345", "Free Account", 100, AccountType.F, 250, false)]
        [TestCase("12345", "Free Account", 100, AccountType.F, -100, false)]
        [TestCase("12345", "Free Account", 100, AccountType.B, 50, false)]
        [TestCase("12345", "Free Account", 100, AccountType.F, 50, true)]
        public void FreeAccountDepoistRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {
            IDeposit myDeposit = new FreeAccountDepositRule();
            Account myAccount = new Account()
            { 
                Name = name,
                AccountNumber = accountNumber,
                Balance = balance,
                Type = accountType
            };
            AccountDepositResponse response = myDeposit.Deposit(myAccount, amount);

            Assert.AreEqual(expectedResult, response.Success);
        }

        [TestCase("12345", "Free Account", 100, AccountType.F, 50, false)]
        [TestCase("12345", "Free Account", 200, AccountType.F, -150, false)]
        [TestCase("12345", "Free Account", 100, AccountType.B, -50, false)]
        [TestCase("12345", "Free Account", 50, AccountType.F, -75, false)]
        [TestCase("12345", "Free Account", 100, AccountType.F, -50, true)]
        public void FreeAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {
            IWithdraw myWithdraw = new FreeAccountWithdrawRule();
            Account myAccount = new Account()
            {
                Name = name,
                AccountNumber = accountNumber,
                Balance = balance,
                Type = accountType
            };
            AccountWithdrawResponse response = myWithdraw.Withdraw(myAccount, amount);

            Assert.AreEqual(expectedResult, response.Success);
        }
    }
}
