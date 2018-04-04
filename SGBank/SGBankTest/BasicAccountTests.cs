using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SGBank.BLL.DepositRules;
using SGBank.BLL.WithdrawRules;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;

namespace SGBankTest
{
    [TestFixture]
    public class BasicAccountTests
    {
        [TestCase("22222", "Basic Account", 100, AccountType.F, 250, false)]
        [TestCase("22222", "Basic Account", 100, AccountType.B, -100, false)]
        [TestCase("22222", "Basic Account", 100, AccountType.B, 250, true)]
        public void BasicAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {
            IDeposit myDeposit = new NoLimitDepositRule();
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

        [TestCase("22222", "Basic Account", 1500, AccountType.B, -1000, 1500, false)]
        [TestCase("22222", "Basic Account", 100, AccountType.F, -100, 100, false)]
        [TestCase("22222", "Basic Account", 100, AccountType.B, 100, 100, false)]
        [TestCase("22222", "Basic Account", 150, AccountType.B, -50, 100, true)]
        [TestCase("22222", "Basic Account", 100, AccountType.B, -150, -60, true)]
        public void BasicAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, decimal newBalance, bool expectedResult)
        {
            IWithdraw myWithdraw = new BasicAccountWithdrawRule();
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
