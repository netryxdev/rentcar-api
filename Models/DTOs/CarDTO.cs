namespace rentcar_api.Models.DTOs
{
    public class CarDTO
    {
        public int IdCar { get; set; }

        public string NmCar { get; set; }

        public int IdCategory { get; set; }

        public int? IdManufacturer { get; set; }

        public decimal VlPrice { get; set; }

        public string NmDescription { get; set; }

        public int? YrDecade { get; set; }

        public string UrlImg { get; set; }

        // Relacionamentos opcionais se você quiser incluir os nomes de Categoria e Fabricante no DTO
        public string NmCategory { get; set; }

        public string NmManufacturer { get; set; }
    }
}
