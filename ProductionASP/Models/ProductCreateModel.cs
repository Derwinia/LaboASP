using ProductionDAL.Entities;

namespace ProductionASP.Models
{
    public class ProductCreateModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public List<int> SelectedCategory { get; set; }
        public List<Category>? AllCategories { get; set; }
    }
}
