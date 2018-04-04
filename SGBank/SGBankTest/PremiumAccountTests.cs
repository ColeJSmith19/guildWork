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
    public class PremiumAccountTests
    {
        [TestCase("33333", "Premium Account", 100, AccountType.F, 250, false)]
        [TestCase("33333", "Premium Account", 100, AccountType.P, -1000, false)]
        [TestCase("33333", "Premium Account", 100, AccountType.P, 250, true)]
        public void PremiumAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
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

        [TestCase("33333", "Premium Account", 100, AccountType.F, -100, 100, false)]
        [TestCase("33333", "Premium Account", 100, AccountType.P, 100, 100, false)]
        [TestCase("33333", "Premium Account", 100, AccountType.P, -700, 100, false)]
        [TestCase("33333", "Premium Account", 150, AccountType.P, -50, 100, true)]
        [TestCase("33333", "Premium Account", 100, AccountType.P, -150, -50, true)]
        public void PremiumAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, decimal newBalance, bool expectedResult)
        {
            IWithdraw myWithdraw = new PremiumAccountWithdrawRule();
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
