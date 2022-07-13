using GestionTournoi.ASP.Exceptions;
using Microsoft.AspNetCore.Mvc;
using ProductionASP.Models;
using ProductionASP.Services;
using ProductionDAL.Entities;

namespace ProductionASP.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _ProductService;
        private readonly CategoryService _CategoryService;

        public ProductController(ProductService ProductService, CategoryService CategoryService)
        {
            _ProductService = ProductService;
            _CategoryService = CategoryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Product> Products = _ProductService.GetLastProduct();
            IEnumerable<ProductIndexModel> model = Products
                .Select(t => new ProductIndexModel
                {
                    Id = t.Id,
                    Reference = t.Reference,
                    Name = t.Name,
                    Price = t.Price,
                    Picture = t.Picture
                });
            return View(model);
        }
        [HttpGet]
        public IActionResult CreateProduct()
        {
            ProductCreateModel model = new ProductCreateModel();
            model.AllCategories = _CategoryService.GetCategories().ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult CreateProduct(ProductCreateModel model)
        {
            if (ModelState.IsValid)
            {
                Product p = new Product
                {
                    Name = model.Name,
                    Price = model.Price/100,
                    Description = model.Description,
                    Stock = model.Stock,
                    Category = _CategoryService.GetById ( model.SelectedCategory )
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
            model.AllCategories = _CategoryService.GetCategories().ToList();
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
        [HttpGet]
        public IActionResult Modify([FromRoute] int id)
        {
            ProductModifyModel model = new ProductModifyModel();
            Product? p = _ProductService.GetById(id);

            model.AllCategories = _CategoryService.GetCategories().ToList();
            model.Name = p.Name;
            model.Price = p.Price;
            model.Description = p.Description;
            model.Stock = p.Stock;
            model.StockModify = 0;
            return View(model);
        }
        [HttpPost]
        public IActionResult Modify(ProductModifyModel model)
        {
            if (ModelState.IsValid)
            {
                Product p = new Product
                {
                    Id = model.Id,
                    Name = model.Name,
                    Price = model.Price / 100,
                    Description = model.Description,
                    Stock = model.Stock,
                    Category = _CategoryService.GetById(model.SelectedCategory)
                };
                try
                {
                    _ProductService.ModifyProduct(p,model.StockModify);
                    TempData.Add("SUCCESS", "Enregistrement OK");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Name", ex.Message);
                }
            }
            model.AllCategories = _CategoryService.GetCategories().ToList();
            TempData.Add("ERROR", "KO");
            return View();
        }
    }
}
