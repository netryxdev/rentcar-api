using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rentcar_api.Data;
using rentcar_api.Models;
using rentcar_api.Models.DTOs;

namespace rentcar_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly AppDbContext _db;
        public CarController(AppDbContext appDbContext)
        {
            _db = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Car>>> GetCars()
        {
            try
            {
                var cars = await _db.Cars.ToListAsync();

                return Ok(cars);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Erro ao buscar Carros: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarDTO>> GetCarById(int id)
        {
            try
            {
                // Busca o carro pelo ID
                var car = await _db.Cars.FindAsync(id);

                // Verifica se o carro foi encontrado
                if (car == null)
                {
                    return NotFound($"Carro com ID {id} não encontrado.");
                }

                return Ok(car);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, $"Erro ao buscar Carro ID: {id} " + ex.Message);
            }
        }
    }
}
