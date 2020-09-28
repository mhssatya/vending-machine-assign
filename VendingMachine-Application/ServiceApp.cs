using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VendingMachine_Application.Interfaces;
using VendingMachine_Domain.Entities;

namespace VendingMachine_Application
{
    public class ServiceApp : IServiceApp
    {
        AppSettings _configuration;
        Dictionary<int, VendingItem> _items;
        IBlOrders _blOrders;
        IHelper _csv;

        public ServiceApp(AppSettings configuration, IHelper csv, IBlOrders blOrders)
        {
            _configuration = configuration;
            _csv = csv;
            _blOrders = blOrders;
        }

        public void LoadItems()
        {
            _items = _csv.PopulateItems();
        }


        public void DoProcess()
        {

            PrintHeader();

            while (true)
            {
                PrintCommands();

                
                Console.Write("Type command: ");
                string input = Console.ReadLine();

                

                if (input == "inv")
                {
                    Console.WriteLine("Displaying Items");
                    PrintItems();
                }

                else if (input.Contains("order"))
                {
                    string[] order = input.Split(' ');
                    try
                    {
                        if (_blOrders.CheckCommand(input,_items.Count))
                        {
                            int itemNumber = Convert.ToInt32(order[2]);
                            int quantity= Convert.ToInt32(order[3]);
                            bool status= _blOrders.CheckAmount(Convert.ToDecimal(order[1]), _items[itemNumber], quantity);

                            if(status)
                            {
                                _blOrders.Order(itemNumber, quantity,ref _items);
                                Console.WriteLine("Order was successful!");
                                
                            }        
                        }
                    }
                    catch(OrderException e)
                    {
                        Console.WriteLine(e.Message+" "+e.InnerException.Message);
                    }

                }

                else if (input.ToUpper() == "Q")
                {
                    Console.WriteLine("Quitting");
                    break;
                }
                else
                {
                    Console.WriteLine("Please try again");
                }

                //Console.ReadLine();
                //Console.Clear();
            }
        }



        #region Print methods

        private void PrintCommands()
        {
            Console.WriteLine();
            Console.WriteLine("Commands");
            Console.WriteLine("inv - Display Vending Machine Items");
            Console.WriteLine("order <amount> <item_number> <Quantity> - Purchase");
            Console.WriteLine("Q - Quit");
        }


        private static void PrintHeader()
        {
            Console.WriteLine("WELCOME TO THE BEST VENDING MACHINE EVER!!!! (Distant crowd roar)");
        }

        private void PrintItems()
        {
            foreach (var item in _items)
            {
                Console.WriteLine(item.Key + " " + item.Value.ItemName + "(" + item.Value.Quantity + "): $" + item.Value.Price);
            }
        }

        
        #endregion
    }
}
