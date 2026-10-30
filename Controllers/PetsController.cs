using Microsoft.AspNetCore.Mvc;
using LhPets.Models;

namespace LhPets.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PetsController : ControllerBase
{
    // Lista em memória. É static para sobreviver entre requisições,
    // já que o controller é criado novamente a cada requisição.
    public static List<Pet> Pets = new();

    // GET: /api/pets
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(Pets);
    }

    // POST: /api/pets
    // Cadastra um pet vinculado a um cliente existente.
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Post([FromBody] Pet pet)
    {
        bool clienteExiste = ClientesController.Clientes.Any(c => c.Id == pet.ClienteId);

        if (!clienteExiste)
        {
            return BadRequest(new { mensagem = "O cliente informado não existe." });
        }

        pet.Id = Pets.Any() ? Pets.Max(p => p.Id) + 1 : 1;
        Pets.Add(pet);
        return Created("", pet);
    }

    // GET: /api/pets/cliente/1
    // Se o cliente não tiver pets, retorna 200 com lista vazia.
    [HttpGet("cliente/{clienteId:int}")]
    public IActionResult GetPorCliente(int clienteId)
    {
        var pets = Pets.Where(p => p.ClienteId == clienteId).ToList();
        return Ok(pets);
    }
}
