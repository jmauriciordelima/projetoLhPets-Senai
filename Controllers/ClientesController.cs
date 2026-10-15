using Microsoft.AspNetCore.Mvc;
using LhPets.Models;

namespace LhPets.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    public static List<Cliente> Clientes = new();

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(Clientes);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Cliente cliente)
    {
        cliente.Id = Clientes.Any() ? Clientes.Max(c => c.Id) + 1 : 1;
        Clientes.Add(cliente);
        return Created("", cliente);
    }
}