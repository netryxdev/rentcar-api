using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace rentcar_api.Models
{
    [Table("t_category")]
    public class Category
    {
        [Key]
        public int id_category { get; set; }

        public string nm_category { get; set; } = "";
    }
}
