﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpacePirateInventory.Models.Tables;

namespace SpacePirateInventory.Data.Interfaces
{
    public interface ICurrencyRepository
    {
        IEnumerable<Currency> GetAll();
    }
}
