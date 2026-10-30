using Microsoft.AspNetCore.Mvc;
using LhPets.Models;

namespace LhPets.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    // Lista em memória. É static para sobreviver entre requisições,
    // já que o controller é criado novamente a cada requisição.
    public static List<Cliente> Clientes = new();

    // GET: /api/clientes
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(Clientes);
    }

    // POST: /api/clientes
    // Cadastra um cliente. A validação dos campos é feita pelos
    // atributos do Model (o [ApiController] devolve 400 automaticamente).
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Post([FromBody] Cliente cliente)
    {
        cliente.Id = Clientes.Any() ? Clientes.Max(c => c.Id) + 1 : 1;
        Clientes.Add(cliente);
        return Created("", cliente);
    }
}
