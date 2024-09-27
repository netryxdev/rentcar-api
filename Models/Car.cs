using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace rentcar_api.Models
{
    [Table("t_car")]
    public class Car
    {
        [Key]
        public int id_car { get; set; }
        public string nm_car { get; set; } = "";
        public int id_category { get; set; }
        public int? id_manufacturer { get; set; }
        public decimal vl_price { get; set; }
        public string nm_description { get; set; } = "";
        public int? yr_decade { get; set; }
        public string url_img { get; set; } = "";

        // Chaves estrangeiras
        // Chave estrangeira para t_category
        [ForeignKey("id_category")]
        public virtual Category Category { get; set; }

        // Chave estrangeira para t_manufacturer
        [ForeignKey("id_manufacturer")]
        public virtual Manufacturer Manufacturer { get; set; }
    }
}
