using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.BLL;
using SGBank.Models.Responses;

namespace SGBank.UI.Workflows
{
    public class WithdrawWorkflow
    {
        public void Execute()
        {
            string amountToParse;
            decimal amount;
            bool parse;
            do
            {
                Console.Clear();
                AccountManager accountManager = AccountManagerFactory.Create();
                Console.WriteLine("Enter an account number: ");
                string accountNumber = Console.ReadLine();

                Console.Write("Enter a withdraw(-) amount: ");
                amountToParse = Console.ReadLine();
                parse = decimal.TryParse(amountToParse, out amount);
                if (parse)
                {

                    AccountWithdrawResponse response = accountManager.Withdraw(accountNumber, amount);

                    if (response.Success)
                    {
                        Console.WriteLine("Withdraw completed!");
                        Console.WriteLine($"Account Number: {response.Account.AccountNumber}");
                        Console.WriteLine($"Old balance: {response.OldBalance:c}");
                        Console.WriteLine($"Amount Withdrawn: {response.Amount:c}");
                        Console.WriteLine($"New balance: {response.Account.Balance:c}");
                    }
                    else
                    {
                        Console.WriteLine("An error occurred: ");
                        Console.WriteLine(response.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Error with amount. Please enter a decimal.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            } while (!parse);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
