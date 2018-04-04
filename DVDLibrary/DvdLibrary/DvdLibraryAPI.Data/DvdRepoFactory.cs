using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibraryAPI.Data
{
    public static class DvdRepoFactory
    {
        public static IDvdRepo GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "Memory":
                    return new DvdMockRepo();
                case "EF":
                    return new DvdEFRepo();
                case "ADO":
                    return new DvdADORepo();
                default:
                    throw new Exception("Not a valid Repo type");
            }
        }
    }
}
