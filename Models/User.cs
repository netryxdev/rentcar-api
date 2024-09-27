using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rentcar_api.Models
{
    [Table("t_user")]
    public class User
    {
        [Key]
        public int id_user { get; set; }
        public string nm_user { get; set; } = string.Empty;
        public string nm_email { get; set; } = string.Empty;
        public string nm_senha { get; set; } = string.Empty;
    }   
}
