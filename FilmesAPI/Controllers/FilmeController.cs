using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class FilmeController
{
    private static List<Filme> filmes = new List<Filme>();

    private void PrintFilme(Filme filme)
    {
        Console.WriteLine("------------------------------");
        Console.WriteLine($"Titulo do filme: {filme.titulo}");
        Console.WriteLine($"Genero do filme: {filme.genero}");
        Console.WriteLine($"Duracao do filme: {filme.duracao}");
    }

    [HttpPost]
    public void AdicionaFilme([FromBody] Filme filme)
    {
        filmes.Add(filme);
        PrintFilme(filme);
    }

    [HttpGet]
    public List<Filme> GetFilmes()
    {
        if (filmes.Count == 0)
        {
            Console.WriteLine("Nao ha filmes cadastrados!");
        }
        else
        {
            Console.WriteLine("Filmes Cadastrados:");
            foreach (Filme filme in filmes)
            {
                PrintFilme(filme);
            }
        }
        return filmes;
    }
}
