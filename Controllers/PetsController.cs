using Microsoft.AspNetCore.Mvc;
using LhPets.Models;

namespace LhPets.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PetsController : ControllerBase
{
    public static List<Pet> Pets = new();

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(Pets);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Pet pet)
    {
        bool clienteExiste = ClientesController.Clientes.Any(c => c.Id == pet.ClienteId);

        if (!clienteExiste)
        {
            return BadRequest ("Cliente não encontrado.");
        }

        pet.Id = Pets.Any() ? Pets.Max(p => p.Id) + 1 : 1;
        Pets.Add(pet);
        return Created("", pet);
    }

    [HttpGet("cliente/{clienteId}")]
    public IActionResult GetPorCliente(int clienteId)
    {
        var pets = Pets.Where(p => p.ClienteId == clienteId).ToList();
        return Ok(pets);
    }
}