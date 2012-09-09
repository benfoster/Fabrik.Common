using System.Collections.Generic;
using System.Linq;
using Fabrik.Common.Web.Example.Models;

namespace Fabrik.Common.Web.Example.Application
{
    public class ProductDetailsViewBuilder : IViewBuilder<int, ProductDetailsView>
    {
        private IEnumerable<Product> GetAllProducts()
        {
            return new[] {
                new Product {Id = 1, Name = "Product 1", Price = 10 },
                new Product {Id = 2, Name = "Product 2", Price = 20 },
                new Product {Id = 3, Name = "Product 3", Price = 30 },
                new Product {Id = 4, Name = "Product 4", Price = 40 },
                new Product {Id = 5, Name = "Product 5", Price = 50 },
                new Product {Id = 6, Name = "Product 6", Price = 60 },
                new Product {Id = 7, Name = "Product 7", Price = 70 },
                new Product {Id = 8, Name = "Product 8", Price = 80 },
                new Product {Id = 9, Name = "Product 9", Price = 90 },
                new Product {Id = 10, Name = "Product 10", Price = 100 },
            };
        }

        public ProductDetailsView Build(int id)
        {
            var product = GetAllProducts().SingleOrDefault(x => x.Id == id);

            if (product == null)
                return null;

            return new ProductDetailsView
            {
                Id = id,
                Price = product.Price,
                Name = product.Name
            };
        }

        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
        }
    }
}