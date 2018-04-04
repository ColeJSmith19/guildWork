using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;
using SGBank.Models.Interfaces;

namespace SGBank.BLL.WithdrawRules
{
    public class WithdrawRulesFactory
    {
        public static IWithdraw Create(AccountType type)
        {
            switch (type)
            {
                case AccountType.F:
                    return new FreeAccountWithdrawRule();
                case AccountType.B:
                    return new BasicAccountWithdrawRule();
                case AccountType.P:
                    return new PremiumAccountWithdrawRule();
                default:
                    throw new Exception("Account type is not supported!");
            }
        }
    }
}
