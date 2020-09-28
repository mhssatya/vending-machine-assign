using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VendingMachine_Application.Interfaces;
using VendingMachine_Domain.Entities;

namespace VendingMachine_Application
{
    public class BlOrders: IBlOrders
    {
        public bool CheckAmount(decimal v, VendingItem vendingItem, int quantity)
        {
            try
            {
                if (vendingItem.Quantity == 0) throw new Exception("Item " + vendingItem.ItemName + " out of stock!");
                if (v!=vendingItem.Price*quantity) throw new Exception("Amount not OK!");
                if (vendingItem.Quantity < quantity) throw new Exception("Only " + vendingItem.Quantity + " items " + vendingItem.ItemName + " are available");

                return true;

            }
            catch (Exception e)
            {
                var errorMessage = $"Error ocured in ordering";
                throw new OrderException(errorMessage, e);
            }
        }

        public void Order(int itemNo, int quantity, ref Dictionary<int, VendingItem> items)
        {       
                var item = items[itemNo];
                item.Quantity = item.Quantity - quantity;
                items[itemNo] = item;
        }


        public bool CheckCommand(string command, int itemsCount)
        {
            string[] order = command.Split(' ');
            if (order.Length != 4)
            {
                Console.WriteLine("order command not OK\r\norder <amount> <item_number> <Quantity>");
                return false;
            }
            else
            {
                int itemNumber, quantity;
                decimal amount;
                if (!decimal.TryParse(order[1], out amount))
                {
                    Console.WriteLine("order command not OK\r\norder <amount> <item_number> <Quantity>");
                    return false;
                }
                if (!Int32.TryParse(order[2], out itemNumber))
                {
                    Console.WriteLine("order command not OK\r\norder <amount> <item_number> <Quantity>");
                    return false;
                }
                if (itemNumber > itemsCount)
                {
                    Console.WriteLine("Unknown item!");
                    return false;
                }
                else
                {
                    if (!Int32.TryParse(order[3], out quantity))
                    {
                        Console.WriteLine("order command not OK\r\norder <amount> <item_number> <Quantity>");
                        return false;
                    }
                    else return true;
                }

            }
        }



    }
}
