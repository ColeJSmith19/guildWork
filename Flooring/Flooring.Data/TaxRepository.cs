using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;

namespace Flooring.Data
{
    public class TaxRepository
    {
        public List<Tax> taxIndex;
        private const string _taxFile = @"C:\Cole Repo\cole-smith-individual-work\Flooring.UI\Data\Taxes.txt";
        //private const string _taxFile = @"C:\Users\coles\Desktop\Data\Taxes.txt";

        //public TaxRepository()
        public List<Tax> Taxes()
        {
            List<Tax> taxIndex = new List<Tax>();
            using (StreamReader sr = new StreamReader(_taxFile))
            {
                string line;
                sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    Tax currentTax = new Tax();

                    string[] columns = line.Split(',');
                    if (columns.Length >= 3)
                    {
                        currentTax.StateAbbreviation = columns[0];
                        currentTax.StateName = columns[1];
                        currentTax.TaxRate = decimal.Parse(columns[2]);
                        taxIndex.Add(currentTax);
                    }
                }
                return taxIndex;
            }
        }
        //public List<Tax> Taxes()
        //{
        //    return taxIndex;
        //}

        private string CreateStateList(Tax tax)
        {
            return string.Format("{0},{1},{2}", tax.StateName, tax.StateAbbreviation, tax.TaxRate);
        }
    }
}
