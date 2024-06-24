using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTO;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class FilmeController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private void PrintFilme(Filme filme)
    {
        Console.WriteLine("------------------------------");
        Console.WriteLine($"ID do filme: {filme.id}");
        Console.WriteLine($"Titulo do filme: {filme.titulo}");
        Console.WriteLine($"Genero do filme: {filme.genero}");
        Console.WriteLine($"Duracao do filme: {filme.duracao}");
    }

    [HttpPost]
    public IActionResult AdicionaFilme([FromBody] CreateFilmeDTO filmeDTO)
    {
        Filme filme = _mapper.Map<Filme>(filmeDTO);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        //PrintFilme(filme);
        return CreatedAtAction(nameof(GetFilme), new { id = filme.id }, filme);
    }

    [HttpGet]
    public IActionResult GetFilmes([FromQuery] int take = 0, [FromQuery] int skip = 0)
    {

        if (_context.Filmes.Count() == 0)
        {
            //Console.WriteLine("Nao ha filmes cadastrados!");
            return Ok(_context.Filmes);
        }
        if (take == 0) take = _context.Filmes.Count();
        //Console.WriteLine("Filmes Cadastrados:");
        //foreach (Filme filme in _context.Filmes.Skip(skip).Take(take))
        //{
        //    PrintFilme(filme);
        //}
        return Ok(_mapper.Map<List<ReadFilmeDTO>>(_context.Filmes.Skip(skip).Take(take)));
    }

    [HttpGet("{id}")]
    public IActionResult GetFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.id == id);

        if (filme == null)
        {
            //Console.WriteLine($"Nao existe filme com o id {id}");
            return NotFound();
        }

        var filmeDTO = _mapper.Map<ReadFilmeDTO>(filme);

        //PrintFilme(filme);
        return Ok(filmeDTO);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateFilme(int id, [FromBody] UpdateFilmeDTO filmeDTO)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.id == id);

        if (filme == null) return NotFound();

        _mapper.Map(filmeDTO, filme);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult PatchFilme(int id, JsonPatchDocument<UpdateFilmeDTO> patch)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.id == id);

        if (filme == null) return NotFound();

        var filmeToUpdate = _mapper.Map<UpdateFilmeDTO>(filme);

        patch.ApplyTo(filmeToUpdate, ModelState);

        if (!TryValidateModel(filmeToUpdate))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(filmeToUpdate, filme);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.id == id);

        if (filme == null) return NotFound();

        _context.Remove(filme);
        _context.SaveChanges();

        return NoContent();
    }
}
