using System;

namespace Lipar.Entities.Domain.Application.DTOs
{
   public class ProductDTO
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool ShowOnHomePage { get; set; }
        public int NumberProductQuestions { get; set; }
    }
}
