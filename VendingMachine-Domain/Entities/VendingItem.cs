
namespace VendingMachine_Domain.Entities
{
    public class VendingItem
    {
       
        public int Id { get; set; }

        public string ItemName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public VendingItem(int id,string itemName, decimal price, int quantity)
        {
            this.ItemName = itemName;
            this.Price = price;
            this.Quantity = quantity;
            this.Id = id;
        }

    }
}
