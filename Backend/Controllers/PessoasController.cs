
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PessoasController : ControllerBase
{
    private readonly AppDbContext _context;
    public PessoasController(AppDbContext context)
    {
        _context = context;

    }

    [HttpPost]
    public async Task<IActionResult> AddPessoa(Pessoas pessoa)
    {
        try
        {
            _context.Pessoa.Add(pessoa);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPessoa",new {id=pessoa.Id},pessoa); //201  o criado objeto pessoa
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
             ex.Message); //500 Internal Server Error com a mensagem de erro
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetPessoas()
    {
        try
        {
            var pessoas = await _context.Pessoa.ToListAsync();
            if (pessoas is null)
            {
                return NotFound(); //404 Not Found se não houver pessoas cadastradas
            }
            return Ok(pessoas); //200 OK com a lista de pessoas
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
             ex.Message); //500 Internal Server Error com a mensagem de erro
        }
    }

    [HttpGet("{id:int}", Name = "GetPessoa")]
    public async Task<IActionResult> GetPessoa(int id)
    {
        try
        {
            var pessoa = await _context.Pessoa.FindAsync(id);
            if (pessoa is null)
            {
                return NotFound(); //404 Not Found se a pessoa não for encontrada
            }

            return Ok(pessoa); //200 OK com a lista de pessoas
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
             ex.Message); //500 Internal Server Error com a mensagem de erro
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePessoa(int id)
    {
        try
        {
            var pessoa = await _context.Pessoa.FindAsync(id);
            if (pessoa is null)
            {
                return NotFound(); //404 Not Found se a pessoa não for encontrada
            }

            _context.Pessoa.Remove(pessoa);
            await _context.SaveChangesAsync();
            return NoContent(); //204 No Content indicando que a exclusão foi bem-sucedida
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
             ex.Message); //500 Internal Server Error com a mensagem de erro
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePessoa(int id, [FromBody] Pessoas pessoa)
    {
        try
        {
            
            if(id != pessoa.Id)
            {
                return BadRequest("ID da pessoa não corresponde ao ID da rota."); //400 Bad Request se o ID na rota não corresponder ao ID no corpo da requisição
            }
            if (!await _context.Pessoa.AnyAsync(p => p.Id == id))
            {
                return NotFound(); //404 Not Found se a pessoa não for encontrada
            }
            
            _context.Pessoa.Update(pessoa);
            await _context.SaveChangesAsync();
               
            return NoContent(); //200 OK com o objeto pessoa atualizado

        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
             ex.Message); //500 Internal Server Error com a mensagem de erro
        }
    }
}


