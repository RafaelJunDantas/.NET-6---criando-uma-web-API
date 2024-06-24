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

    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="filmeDTO">Objeto com os atributos necessários para a criaçãode um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso a criação e inserção sejam sucesso</response>
    [HttpPost]
    [ProducesResponseType( StatusCodes.Status201Created)]
    public IActionResult AdicionaFilme([FromBody] CreateFilmeDTO filmeDTO)
    {
        Filme filme = _mapper.Map<Filme>(filmeDTO);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        //PrintFilme(filme);
        return CreatedAtAction(nameof(GetFilme), new { id = filme.id }, filme);
    }

    /// <summary>
    /// Retorna uma quantidade ou todos os filmes do banco de dados
    /// </summary>
    /// <param name="take">Quantos filmes quer que retorno, se não especificar, retorna todos</param>
    /// <param name="skip">Quantos filmes quer pular</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a requisiçao seja sucesso</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
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

    /// <summary>
    /// Retorna o filmes especificado pelo ID
    /// </summary>
    /// <param name="id">ID do filme no banco de dados</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso o filme com o ID exista no banco de dados</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
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

    /// <summary>
    /// Atualiza um filme por ID
    /// </summary>
    /// <param name="id">ID do filme para atualizar</param>
    /// <param name="filmeDTO">Objeto com os atributos para atualizar</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso consiga atualizar o filme com o ID especificado</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult UpdateFilme(int id, [FromBody] UpdateFilmeDTO filmeDTO)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.id == id);

        if (filme == null) return NotFound();

        _mapper.Map(filmeDTO, filme);
        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Atualiza um atibuto do filme por ID
    /// </summary>
    /// <param name="id">ID do filme para atualizar</param>
    /// <param name="patch">Objeto com os atributos para atualizar</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso consiga atualizar o filme com o ID especificado</response>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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

    /// <summary>
    /// Delete um filme por ID
    /// </summary>
    /// <param name="id">ID do filme para deletar</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso o filme seja deletado</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.id == id);

        if (filme == null) return NotFound();

        _context.Remove(filme);
        _context.SaveChanges();

        return NoContent();
    }
}
