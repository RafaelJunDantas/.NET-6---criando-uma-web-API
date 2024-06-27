using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTO;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SessaoController : ControllerBase
{
    private Context _context;
    private IMapper _mapper;

    public SessaoController(Context context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionaSessao([FromBody] CreateSessaoDTO sessaoDTO)
    {
        Sessao sessao = _mapper.Map<Sessao>(sessaoDTO);
        _context.Sessoes.Add(sessao);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetSessao), new { id = sessao.id }, sessao);
    }

    [HttpGet]
    public IActionResult GetSessoes([FromQuery] int take = 0, [FromQuery] int skip = 0)
    {

        if (_context.Sessoes.Count() == 0)
        {
            //Console.WriteLine("Nao ha sessoes cadastrados!");
            return Ok(_context.Sessoes);
        }
        if (take == 0) take = _context.Sessoes.Count();

        var listaSessao = _mapper.Map<List<ReadSessaoDTO>>(_context.Sessoes.Skip(skip).Take(take).ToList());
        return Ok(listaSessao);
    }

    [HttpGet("{id}")]
    public IActionResult GetSessao(int id)
    {
        var sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.id == id);

        if (sessao == null)
        {
            //Console.WriteLine($"Nao existe sessao com o id {id}");
            return NotFound();
        }

        var sessaoDTO = _mapper.Map<ReadSessaoDTO>(sessao);

        return Ok(sessaoDTO);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteSessao(int id)
    {
        var sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.id == id);

        if (sessao == null) return NotFound();

        _context.Remove(sessao);
        _context.SaveChanges();

        return NoContent();
    }
}
