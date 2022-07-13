using ProductionDAL.Entities;
namespace ProductionASP.Models
{
    public class ProductModifyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public int StockModify { get; set; }
        public int SelectedCategory { get; set; }
        public List<Category>? AllCategories { get; set; }
    }
}
