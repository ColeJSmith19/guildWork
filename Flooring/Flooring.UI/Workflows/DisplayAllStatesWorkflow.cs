using Flooring.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.UI.Workflows
{
    public class DisplayAllStatesWorkflow
    {
        public void Execute()
        {
            TaxManager myTaxManager = new TaxManager();
            ConsoleIO.PrintAllStates(myTaxManager.GetAllStateInfo());
        }
    }
}
