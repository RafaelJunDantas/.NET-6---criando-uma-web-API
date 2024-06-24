using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class FilmeController : ControllerBase
{
    private static List<Filme> filmes = new List<Filme>();
    private static int id = 0;

    private void PrintFilme(Filme filme)
    {
        Console.WriteLine("------------------------------");
        Console.WriteLine($"ID do filme: {filme.id}");
        Console.WriteLine($"Titulo do filme: {filme.titulo}");
        Console.WriteLine($"Genero do filme: {filme.genero}");
        Console.WriteLine($"Duracao do filme: {filme.duracao}");
    }

    [HttpPost]
    public IActionResult AdicionaFilme([FromBody] Filme filme)
    {
        filme.id = id++;
        filmes.Add(filme);
        PrintFilme(filme);
        return CreatedAtAction(nameof(GetFilme), new { id = filme.id }, filme);
    }

    [HttpGet]
    public IActionResult GetFilmes([FromQuery] int take = 0, [FromQuery] int skip = 0)
    {
        if (filmes.Count == 0)
        {
            Console.WriteLine("Nao ha filmes cadastrados!");
            return Ok(filmes);
        }
        if(take == 0) take = filmes.Count; 
        Console.WriteLine("Filmes Cadastrados:");
        foreach (Filme filme in filmes.Skip(skip).Take(take))
        {
            PrintFilme(filme);
        }
        return Ok(filmes.Skip(skip).Take(take));
    }

    [HttpGet("{id}")]
    public IActionResult GetFilme(int id)
    {
        var filme = filmes.FirstOrDefault(filme => filme.id == id);

        if (filme == null)
        {
            Console.WriteLine($"Nao existe filme com o id {id}");
            return NotFound();
        }

        PrintFilme(filme);
        return Ok(filme);
    }
}
