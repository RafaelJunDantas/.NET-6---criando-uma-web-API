using AutoMapper;
using FilmesAPI.Data.DTO;
using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[Controller]")]
public class EnderecoController : ControllerBase
{
    private Context _context;
    private IMapper _mapper;

    public EnderecoController(Context context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um endereco ao banco de dados
    /// </summary>
    /// <param name="enderecoDTO">Objeto com os atributos necessários para a criação de um endereco</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso a criação e inserção sejam sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionaEndereco([FromBody] CreateEnderecoDTO enderecoDTO)
    {
        Endereco endereco = _mapper.Map<Endereco>(enderecoDTO);
        _context.Enderecos.Add(endereco);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetEndereco), new { id = endereco.id }, endereco);
    }

    /// <summary>
    /// Retorna uma quantidade ou todos os enderecos do banco de dados
    /// </summary>
    /// <param name="take">Quantos enderecos quer que retorno, se não especificar, retorna todos</param>
    /// <param name="skip">Quantos enderecos quer pular</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a requisiçao seja sucesso</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetEnderecos([FromQuery] int take = 0, [FromQuery] int skip = 0)
    {

        if (_context.Enderecos.Count() == 0)
        {
            //Console.WriteLine("Nao ha enderecos cadastrados!");
            return Ok(_context.Enderecos);
        }
        if (take == 0) take = _context.Enderecos.Count();
        return Ok(_mapper.Map<List<ReadEnderecoDTO>>(_context.Enderecos.Skip(skip).Take(take)));
    }

    /// <summary>
    /// Retorna o endereco especificado pelo ID
    /// </summary>
    /// <param name="id">ID do endereco no banco de dados</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso o endereco com o ID exista no banco de dados</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetEndereco(int id)
    {
        var endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.id == id);

        if (endereco == null)
        {
            //Console.WriteLine($"Nao existe endereco com o id {id}");
            return NotFound();
        }

        var enderecoDTO = _mapper.Map<ReadEnderecoDTO>(endereco);

        return Ok(enderecoDTO);
    }

    /// <summary>
    /// Atualiza um endereco por ID
    /// </summary>
    /// <param name="id">ID do endereco para atualizar</param>
    /// <param name="enderecoDTO">Objeto com os atributos para atualizar</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso consiga atualizar o endereco com o ID especificado</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult UpdateEndereco(int id, [FromBody] UpdateEnderecoDTO enderecoDTO)
    {
        var endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.id == id);

        if (endereco == null) return NotFound();

        _mapper.Map(enderecoDTO, endereco);
        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Atualiza um atibuto do endereco por ID
    /// </summary>
    /// <param name="id">ID do endereco para atualizar</param>
    /// <param name="patch">Objeto com os atributos para atualizar</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso consiga atualizar o endereco com o ID especificado</response>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult PatchEndereco(int id, JsonPatchDocument<UpdateEnderecoDTO> patch)
    {
        var endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.id == id);

        if (endereco == null) return NotFound();

        var enderecoToUpdate = _mapper.Map<UpdateEnderecoDTO>(endereco);

        patch.ApplyTo(enderecoToUpdate, ModelState);

        if (!TryValidateModel(enderecoToUpdate))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(enderecoToUpdate, endereco);
        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Delete um endereco por ID
    /// </summary>
    /// <param name="id">ID do endereco para deletar</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso o endereco seja deletado</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteEndereco(int id)
    {
        var endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.id == id);

        if (endereco == null) return NotFound();

        _context.Remove(endereco);
        _context.SaveChanges();

        return NoContent();
    }
}
