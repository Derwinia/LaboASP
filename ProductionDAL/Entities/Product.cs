using System.ComponentModel.DataAnnotations.Schema;

namespace ProductionDAL.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
        public Category? Category { get; set; }
        public string? Picture { get; set; }
    }
}
