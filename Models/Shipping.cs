
namespace WebApplication1TEST.Models
{
    public class Shipping
    {
        public int ID { get; set; }
        public int OrderID { get; set; }

        public string ShippingAddress { get; set; }

        public DateTime ShippingDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }

    }
}
