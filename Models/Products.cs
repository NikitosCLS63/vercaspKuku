namespace WebApplication1TEST.Models
{
    public record class Products
    {
        public int ID { get; set; }
        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int CategoriesID { get; set; }

        public string img {  get; set; }
    }
}
