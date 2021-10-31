
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication10.Models;

namespace WebApplication10.Repository
{
    public class JsonProductRepository : IProductRepository
    {
        private List<Product> _products = new List<Product>();
        private string filePath = @"C:\Users\Milica\OneDrive\Desktop\ProductsFile\Product.TXT";

        public JsonProductRepository()
        {
           
           var jsonData = System.IO.File.ReadAllText(filePath);
            _products = String.IsNullOrEmpty(jsonData)
                ? new List<Product>()
                : JsonConvert.DeserializeObject<List<Product>>(jsonData);
        }
        public Product EditProduct(Product product)
        {   //Ako je id=0 u pitanju je create
            if (product.Id == 0)
            {
                //slucaj kada imamo praznu listu
                if (_products.Count==0)
                {
                    product.Id = 1;
                }
                else
                {
                    // simulacija identity(1,1)
                    var maxID = _products.Max(p => p.Id);
                    product.Id = maxID + 1;
                }
                _products.Add(product);
            }
            else
            {
                var c = _products.SingleOrDefault(p => p.Id == product.Id);
                //Izvuceni Id nad kojim se vrsi Edit
                product.Id = c.Id;
                _products.Remove(c);
                _products.Add(product);
            }
            var json = JsonConvert.SerializeObject(_products.ToArray(), Formatting.Indented);
            //Upisivanje u txt fajl
            System.IO.File.WriteAllText(filePath, json);
            return product;

        }

        public IEnumerable<Product> GetAllProducts()
        { 
            return _products;

        }

        public Product GetProductById(int Id)
        {
            return _products.SingleOrDefault(p => p.Id == Id);
        }
    }
}
