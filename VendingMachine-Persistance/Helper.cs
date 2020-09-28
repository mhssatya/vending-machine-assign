using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using VendingMachine_Application;
using VendingMachine_Application.Interfaces;
using VendingMachine_Domain.Entities;

namespace VendingMachine_Persistance
{
    public class Helper: IHelper
    {
        AppSettings _configuration;
        List<VendingItem> _vendingItems;
        Dictionary<int, VendingItem> _items = new Dictionary<int, VendingItem>();
        public Helper(AppSettings configuration)
        {
            _configuration = configuration;
        }



        public Dictionary<int, VendingItem> PopulateItems()
        {
            using (var reader = new StreamReader(_configuration.FileLocation))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();
                _vendingItems = csv.GetRecords<VendingItem>().ToList();

            }
            foreach(var itemType in _vendingItems)
            {
              _items.Add(itemType.Id, itemType);
            }


            return _items;
        }
            
           


    }
}
