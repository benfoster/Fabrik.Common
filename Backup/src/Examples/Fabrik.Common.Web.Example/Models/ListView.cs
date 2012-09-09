using System.Collections.Generic;
using System.Linq;

namespace Fabrik.Common.Web.Example.Models
{
    public class ListView : PageMetadata
    {
        private readonly ListParameters parameters;

        public List<Product> Products { get; set; }

        public ListView()
        {

        }

        public ListView(ListParameters parameters)
        {
            this.parameters = parameters;
            var skip = (parameters.Page - 1) * parameters.Size;
            Products = GetAllProducts().Skip(skip).Take(parameters.Size).ToList();
        }

        public override string MetaDescription
        {
            get
            {
                return "Products Description";
            }
        }

        public override string MetaKeywords
        {
            get
            {
                return "Products, Stuff, Monies";
            }
        }

        public override string PageTitle
        {
            get
            {
                return "Products";
            }
        }

        private IEnumerable<Product> GetAllProducts()
        {
            return new[] {
                new Product { Name = "Product 1", Price = 10 },
                new Product { Name = "Product 2", Price = 20 },
                new Product { Name = "Product 3", Price = 30 },
                new Product { Name = "Product 4", Price = 40 },
                new Product { Name = "Product 5", Price = 50 },
                new Product { Name = "Product 6", Price = 60 },
                new Product { Name = "Product 7", Price = 70 },
                new Product { Name = "Product 8", Price = 80 },
                new Product { Name = "Product 9", Price = 90 },
                new Product { Name = "Product 10", Price = 100 },
            };
        }

        public class Product
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
        }
    }

    public class ListParameters
    {
        public int Size { get; set; }
        public int Page { get; set; }

        public ListParameters()
        {
            Size = 2;
            Page = 1;
        }
    }
}