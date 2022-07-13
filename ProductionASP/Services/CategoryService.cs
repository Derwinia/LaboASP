using ProductionDAL;
using ProductionDAL.Entities;

namespace ProductionASP.Services
{
    public class CategoryService
    {
        private readonly ProductContext _dc;

        public CategoryService(ProductContext dc)
        {
            _dc = dc;
        }
        public IEnumerable<Category> GetCategories()
        {
            return _dc.Categories;
        }
        public Category? GetById(int Id)
        {
            return _dc.Categories.Find(Id);
        }
    }
}
