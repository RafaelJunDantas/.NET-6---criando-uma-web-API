using AutoMapper;
using FilmesAPI.Data.DTO;
using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[Controller]")]
public class CinemaController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    public CinemaController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um cinema ao banco de dados
    /// </summary>
    /// <param name="cinemaDTO">Objeto com os atributos necessários para a criaçãode um cinema</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso a criação e inserção sejam sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionaCinema([FromBody] CreateCinemaDTO cinemaDTO)
    {
        Cinema cinema = _mapper.Map<Cinema>(cinemaDTO);
        _context.Cinemas.Add(cinema);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetCinema), new { id = cinema.id }, cinema);
    }

    /// <summary>
    /// Retorna uma quantidade ou todos os cinemas do banco de dados
    /// </summary>
    /// <param name="take">Quantos cinemas quer que retorno, se não especificar, retorna todos</param>
    /// <param name="skip">Quantos cinemas quer pular</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a requisiçao seja sucesso</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetCinemas([FromQuery] int take = 0, [FromQuery] int skip = 0)
    {

        if (_context.Cinemas.Count() == 0)
        {
            //Console.WriteLine("Nao ha cinemas cadastrados!");
            return Ok(_context.Cinemas);
        }
        if (take == 0) take = _context.Cinemas.Count();
        return Ok(_mapper.Map<List<ReadCinemaDTO>>(_context.Cinemas.Skip(skip).Take(take)));
    }

    /// <summary>
    /// Retorna o cinema especificado pelo ID
    /// </summary>
    /// <param name="id">ID do cinema no banco de dados</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso o cinema com o ID exista no banco de dados</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetCinema(int id)
    {
        var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.id == id);

        if (cinema == null)
        {
            //Console.WriteLine($"Nao existe cinema com o id {id}");
            return NotFound();
        }

        var cinemaDTO = _mapper.Map<ReadCinemaDTO>(cinema);

        return Ok(cinemaDTO);
    }

    /// <summary>
    /// Atualiza um cinema por ID
    /// </summary>
    /// <param name="id">ID do cinema para atualizar</param>
    /// <param name="cinemaDTO">Objeto com os atributos para atualizar</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso consiga atualizar o cinema com o ID especificado</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult UpdateCinema(int id, [FromBody] UpdateFilmeDTO cinemaDTO)
    {
        var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.id == id);

        if (cinema == null) return NotFound();

        _mapper.Map(cinemaDTO, cinema);
        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Atualiza um atibuto do cinema por ID
    /// </summary>
    /// <param name="id">ID do cinema para atualizar</param>
    /// <param name="patch">Objeto com os atributos para atualizar</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso consiga atualizar o cinema com o ID especificado</response>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult PatchCinema(int id, JsonPatchDocument<UpdateCinemaDTO> patch)
    {
        var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.id == id);

        if (cinema == null) return NotFound();

        var cinemaToUpdate = _mapper.Map<UpdateCinemaDTO>(cinema);

        patch.ApplyTo(cinemaToUpdate, ModelState);

        if (!TryValidateModel(cinemaToUpdate))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(cinemaToUpdate, cinema);
        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Delete um cinema por ID
    /// </summary>
    /// <param name="id">ID do cinema para deletar</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso o cinema seja deletado</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteCinema(int id)
    {
        var cinema = _context.Cinema.FirstOrDefault(cinema => cinema.id == id);

        if (cinema == null) return NotFound();

        _context.Remove(cinema);
        _context.SaveChanges();

        return NoContent();
    }
}
