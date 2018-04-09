using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spark.Models.Tables;

namespace Spark.Data.Interfaces
{
    public interface IColorRepository
    {
        List<Colors> GetAll();
    }
}
