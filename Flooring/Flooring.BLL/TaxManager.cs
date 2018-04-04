using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Data;
using Flooring.Models;

namespace Flooring.BLL
{
    public class TaxManager
    {
        public List<Tax> GetAllStateInfo()
        {
            TaxRepository repo = new TaxRepository();
            return repo.Taxes();
        }
    }
}
