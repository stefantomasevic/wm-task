using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication10.Models;
using WebApplication10.Repository;

namespace WebApplication10.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public ActionResult Index()
        {
            var products = _productRepository.GetAllProducts();

            return View(products);
        }
        public ActionResult Create()
        {
            return View("Edit", new Product());
        }
        public ActionResult Edit(int id)
        {
            return View(_productRepository.GetProductById(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {     
                var result = _productRepository.EditProduct(product);
                return RedirectToAction(nameof(Index));  
        }
    }
}