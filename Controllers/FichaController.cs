using Microsoft.AspNetCore.Mvc;
using FichaRPG.Models;

namespace FichaRPG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FichaController : ControllerBase
    {
        private static readonly string[] Classes = { "Guerreiro", "Mago", "Ladino", "Bardo" };
        private static readonly string[] Racas = { "Humano", "Elfo", "Anão", "Orc" };

        [HttpGet("gerar")]
        public ActionResult<Ficha> GerarFicha()
        {
            var random = new Random();
            var ficha = new Ficha
            {
                Nome = $"Aventureiro {random.Next(1000, 9999)}",
                Classe = Classes[random.Next(Classes.Length)],
                Raca = Racas[random.Next(Racas.Length)],
                Forca = random.Next(8, 19),
                Destreza = random.Next(8, 19),
                Constituicao = random.Next(8, 19),
                Inteligencia = random.Next(8, 19),
                Sabedoria = random.Next(8, 19),
                Carisma = random.Next(8, 19)
            };
            return Ok(ficha);
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class RolagemController : ControllerBase
    {
        private static readonly Random _random = new Random();

        [HttpGet("rolar/{tipo}")]
        public ActionResult<int> RolarDado(string tipo)
        {
            int resultado = tipo.ToLower() switch
            {
                "d20" => _random.Next(1, 21),
                "d6" => _random.Next(1, 7),
                "d4" => _random.Next(1, 5),
                "d8" => _random.Next(1, 9),
                "d10" => _random.Next(1, 11),
                "d12" => _random.Next(1, 13),
                "d100" => _random.Next(1, 101),
                _ => throw new ArgumentException("Tipo de dado inválido")
            };
            return Ok(resultado);
        }
    }
}
