using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;
using SGBank.Models.Interfaces;

namespace SGBank.Data
{
    public class FileAccountRepository : IAccountRepository
    {
        private Dictionary<string, Account> _accountIndex;
        private const string repoFile = @"C:\Cole Repo\cole-smith-individual-work\SGBank.UI\Accounts.csv";


        public FileAccountRepository()
        {

            _accountIndex = new Dictionary<string, Account>();
            if (!File.Exists(repoFile))
            {
                File.Create(repoFile);
            }
            else
            {
                using (StreamReader reader = new StreamReader(repoFile))
                {
                    string line;
                    reader.ReadLine();
                    while((line = reader.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');
                        if(values.Length >= 4)
                        {
                            string key = values[0];
                            string accountNumber = values[0];
                            string name = values[1];
                            decimal balance = decimal.Parse(values[2]);
                            AccountType type;
                            Enum.TryParse(values[3], out type);
                            _accountIndex.Add(key, new Account()
                            {
                                AccountNumber = accountNumber,
                                Name = name,
                                Balance = balance,
                                Type = type
                            });
                        }
                    }
                }
            }
        }

        public Account LoadAccount(string id)
        {
            //int ID = Convert.ToInt32(id);
            if (_accountIndex.Keys.Contains(id))
            {
                return _accountIndex[id];
            }
            else
            {
                return null;
            }
        }
        public void SaveAccount(Account account)
        {
            using (StreamWriter writer = new StreamWriter(repoFile))
            {
                writer.WriteLine("AccountNumber,Name,Balance,Type");
                foreach (KeyValuePair<string, Account> kv in _accountIndex)
                {
                    if (kv.Value != null)
                        writer.WriteLine($"{kv.Value.AccountNumber},{kv.Value.Name},{kv.Value.Balance},{kv.Value.Type}");
                }
            }

        }
    }
}
