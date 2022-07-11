using ProductionDAL;
using ProductionDAL.Entities;

namespace ProductionASP.Services
{
    public class ProductService
    {
        private readonly ProductContext _dc;

        public ProductService(ProductContext dc)
        {
            _dc = dc;
        }
        public IEnumerable<Product> GetLastProduct()
        {
            return _dc.Products
                .OrderBy(t => t.Reference)
                .Take(20);
        }
        public IEnumerable<Product> GetProducts()
        {
            return _dc.Products;
        }
        public void CreateProduct(Product p)
        {
            p.Reference =p.Name.Substring(0,4);
            IEnumerable<Product> sameName_prod = GetProducts()
                    .Where(p => p.Reference.Substring(0, 4) == p.Name.Substring(0, 4));
            string temp = "000" + (sameName_prod.Count()+1);
            p.Reference += temp.Substring(temp.Length-4, 4);
            p.CreationDate = DateTime.Now;
            p.UpdateDate = DateTime.Now;
            p.IsDeleted = false;
            // enregistrer dans la db
            _dc.Products.Add(p);
            _dc.SaveChanges();
            //defefg
        }
    }
}
