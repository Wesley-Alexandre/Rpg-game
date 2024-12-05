using Microsoft.AspNetCore.Mvc;
using FichaRPG.Models;

namespace FichaRPG.Controllers
{
    // Definição da classe Personagem, que será usada no combate
    public class Personagem
    {
        public string Nome { get; set; }
        public int HP { get; set; }
        public int Forca { get; set; }
    }

    // Controller para gerar a ficha do personagem
    [ApiController]
    [Route("api/[controller]")]
    public class FichaController : ControllerBase
    {
        private static readonly string[] Classes = { "Guerreiro", "Mago", "Ladino", "Bardo" };
        private static readonly string[] Racas = { "Humano", "Elfo", "Anão", "Orc" };

        // Endpoint para gerar uma ficha de personagem aleatória
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

    // Controller para rolar dados
    [ApiController]
    [Route("api/[controller]")]
    public class RolagemController : ControllerBase
    {
        private static readonly Random _random = new Random();

        // Endpoint para rolar diferentes tipos de dados
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

    // Controller para iniciar o combate
    [ApiController]
    [Route("api/[controller]")]
    public class CombateController : ControllerBase
    {
        private static readonly Random _random = new Random();

        // Endpoint para iniciar o combate
        [HttpGet("combater")]
        public ActionResult<string> Combater()
        {
            var jogador = new Personagem { Nome = "Aventureiro", HP = 30, Forca = 10 };
            var inimigo = new Personagem { Nome = "Goblin", HP = 25, Forca = 8 };

            int danoJogador = _random.Next(1, 7) + jogador.Forca;
            inimigo.HP -= danoJogador;

            int danoInimigo = _random.Next(1, 7) + inimigo.Forca;
            jogador.HP -= danoInimigo;

            string resultado = $"{jogador.Nome} atacou {inimigo.Nome} causando {danoJogador} de dano! " +
                               $"{inimigo.Nome} agora tem {inimigo.HP} HP.\n" +
                               $"{inimigo.Nome} atacou {jogador.Nome} causando {danoInimigo} de dano! " +
                               $"{jogador.Nome} agora tem {jogador.HP} HP.";

            if (jogador.HP <= 0 && inimigo.HP <= 0)
            {
                resultado += "\nO combate terminou em empate!";
            }
            else if (jogador.HP <= 0)
            {
                resultado += "\nO inimigo venceu!";
            }
            else if (inimigo.HP <= 0)
            {
                resultado += "\nO jogador venceu!";
            }

            return Ok(resultado);
        }
    }
}
