using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;
using SGBank.Models.Interfaces;

namespace SGBank.BLL.DepositRules
{
    public class DepositRulesFactory
    {
        public static IDeposit Create(AccountType type)
        {
            switch(type)
            {
                case AccountType.F:
                    return new FreeAccountDepositRule();
                case AccountType.B:
                    return new NoLimitDepositRule();
                case AccountType.P:
                    return new NoLimitDepositRule();
            }

            throw new Exception("Account type is not supported!");
        }
    }
}
