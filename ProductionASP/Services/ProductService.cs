using GestionTournoi.ASP.Exceptions;
using ProductionDAL;
using ProductionDAL.Entities;

namespace ProductionASP.Services
{
    public class ProductService
    {
        private readonly ProductContext _dc;
        private readonly MailService _mailService;

        public ProductService(ProductContext dc, MailService mailService)
        {
            _dc = dc;
            _mailService = mailService;
        }
        public IEnumerable<Product> GetLastProduct()
        {
            return _dc.Products
                .Where(p => p.IsDeleted == false)
                .OrderBy(t => t.Reference)
                .Take(20);
        }
        public IEnumerable<Product> GetProducts()
        {
            return _dc.Products;
        }
        public Product? GetById(int id)
        {
            return _dc.Products.Find(id);
        }

        public void CreateProduct(Product p)
        {
            p.Reference = p.Name.Substring(0, 4);
            IEnumerable<Product> sameName_prod = GetProducts()
                    .Where(pnew => pnew.Reference.Substring(0, 4) == p.Name.Substring(0, 4));
            string temp = "000" + (sameName_prod.Count() + 1);
            p.Reference += temp.Substring(temp.Length - 4, 4);
            p.CreationDate = DateTime.Now;
            p.UpdateDate = DateTime.Now;
            p.IsDeleted = false;
            // enregistrer dans la db
            _dc.Products.Add(p);
            _dc.SaveChanges();
        }
        public void ModifyProduct(Product p, int StockModify)
        {
            Product product = GetById(p.Id);
            product.Name = p.Name;
            product.Price = p.Price;
            product.Category = p.Category;
            product.Description = p.Description;
            if (StockModify < 0 && p.Price * StockModify <= -1000)
            {
                _mailService.Send("Perte Importante", "l'article " + p.Name + " dont la valeur était de " + p.Stock * p.Price + ", vien d'être modifié et a enregistrer une perte de " + p.Price * StockModify + " euros à été supprimé");
            }
            else if (p.Stock + StockModify < 0)
            {
                _mailService.Send("Stock Epuisé", "l'article " + p.Name + " n'as plus de stock");
            }
            if (p.Stock + StockModify < 0)
            {
                product.Stock = 0;
            }
            else product.Stock += StockModify;
            p.UpdateDate = DateTime.Now;
            _dc.SaveChanges();
        }
        public void Delete(Product p)
        {
            if (p.Stock * p.Price > 1000)
            {
                throw new ProductException("Valeur trop élevé");
            }
            else if (p.Stock * p.Price > 100)
            {
                _mailService.Send("Produit encore disponible", "Un article dont la valeur est de " + p.Price * p.Stock + " euros à été supprimé");
            }
            p.UpdateDate = DateTime.Now;
            p.IsDeleted = true;
            _dc.SaveChanges();
        }
    }
}