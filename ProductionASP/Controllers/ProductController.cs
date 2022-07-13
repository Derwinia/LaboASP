using GestionTournoi.ASP.Exceptions;
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
                    Id = t.Id,
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
                    Price = model.Price / 100,
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
            TempData.Add("ERROR", "KO");
            return View();
        }

        public IActionResult ConfirmationDelete([FromRoute] int id)
        {
            // Afficher une page de confirmation
            return View(id);
        }

        public IActionResult DeleteEntry([FromRoute] int id)
        {
            Product? p = _ProductService.GetById(id);
            if (p is null)
            {
                return NotFound();
            }
            try
            {
                _ProductService.Delete(p);
                TempData["SUCCESS"] = "Bravo";
                return RedirectToAction("Index");
            }
            catch (ProductException ex)
            {
                // afficher un message d'erreur
                TempData["ERROR"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
