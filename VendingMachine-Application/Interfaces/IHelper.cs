using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using VendingMachine_Domain.Entities;

namespace VendingMachine_Application.Interfaces
{
    public interface IHelper
    {
        Dictionary<int, VendingItem> PopulateItems();

    }
}
