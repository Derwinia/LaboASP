using Microsoft.AspNetCore.Mvc;
using ProductionASP.Models;
using ProductionASP.Services;
using ProductionDAL.Entities;
using System.Diagnostics;

namespace ProductionASP.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _ProductService;

        public ProductController(ProductService ProductService)
        {
            _ProductService = ProductService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Product> Products = _ProductService.GetLastProduct();
            //mapping transformer un object en au autre objet
            IEnumerable<ProductIndexModel> model = Products
                .Select(t => new ProductIndexModel
                {
                    Reference = t.Reference,
                    Name = t.Name,
                    Price = t.Price,
                });
            return View(model);
        }
        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateProduct(ProductCreateModel model)
        {
            // verifier la validité du formulaire
            // mapper le model en tournament
            if (ModelState.IsValid)
            {
                Product p = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    Stock = model.Stock,
                };

                try
                {
                    _ProductService.CreateProduct(p);
                    TempData.Add("SUCCESS", "Enregistrement OK");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Name" ,ex.Message);
                }
            }
            TempData.Add("ERROR", "Verifiez vos données");
            return View();
        }
    }
}
