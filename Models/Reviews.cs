using Microsoft.EntityFrameworkCore.Diagnostics;

namespace WebApplication1TEST.Models
{
    public class Reviews
    {
        public int ID { get; set; }

        public string ProductID { get; set; }
        public string CustomerID { get; set; }
        public int Rating { get; set; }

        public string Comment { get; set; }


    }
}
