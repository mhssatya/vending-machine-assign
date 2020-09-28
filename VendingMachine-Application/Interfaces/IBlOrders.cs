using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VendingMachine_Domain.Entities;

namespace VendingMachine_Application.Interfaces
{
    public interface IBlOrders
    {
        bool CheckAmount(decimal v, VendingItem vendingItem, int v1);

        bool CheckCommand(string command, int itemsCount);

        void Order(int itemNo, int quantity, ref Dictionary<int, VendingItem> items);
    }
}
