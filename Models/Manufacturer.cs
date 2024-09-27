using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace rentcar_api.Models
{
    [Table("t_manufacturer")]
    public class Manufacturer
    {
        [Key]
        public int id_manufacturer { get; set; }

        public string nm_manufacturer { get; set; } = "";
    }
}
