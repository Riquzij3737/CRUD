using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using ComunicaationToServer.Methods;

namespace ComunicaationToServer.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class Control1
    {
        [HttpGet("MinhaAPI/read")]
        public JsonResult GetJson()
        {
        
            MetodosDobancodeDados met = new MetodosDobancodeDados();

            return met.GetAllData();

        }

        [HttpPost("MinhaAPI/create")]
        public void Create([FromQuery] string Nomes, [FromQuery] int idade)
        {
            MetodosDobancodeDados dados = new MetodosDobancodeDados();

            dados.add(Nomes, idade);
            
        }

        [HttpDelete("MinhaAPI/delete")]
        public void Delete([FromQuery] int id)
        {
            MetodosDobancodeDados dados = new MetodosDobancodeDados();

            dados.delete(id);

        }

        [HttpPut("MinhaAPI/update")]
        public void Update([FromQuery] string Nomes, [FromQuery] int idade, [FromQuery] int id)
        {
            MetodosDobancodeDados dados = new MetodosDobancodeDados();

            dados.update(Nomes, idade, id);
        }

    }

    public class DadosDeMinhaAPI
    {
        public List<string> Nomes { get; set; }
        public List<int> idades { get; set; }

        public DadosDeMinhaAPI()
        {
            Nomes = new List<string>();
            idades = new List<int>();
        }
    }

  

}
