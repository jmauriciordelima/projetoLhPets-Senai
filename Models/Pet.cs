using System.ComponentModel.DataAnnotations;

namespace LhPets.Models;

/// <summary>
/// Representa um pet, vinculado a um cliente por <see cref="ClienteId"/>.
/// </summary>
public class Pet
{
    /// <summary>Identificador do pet. Gerado automaticamente pela API.</summary>
    public int Id { get; set; }

    /// <summary>Nome do pet (3 a 100 caracteres).</summary>
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Nome { get; set; } = string.Empty;

    /// <summary>Espécie do pet (por exemplo, Cão ou Gato).</summary>
    [Required]
    public string Especie { get; set; } = string.Empty;

    /// <summary>Raça do pet.</summary>
    [Required]
    public string Raca { get; set; } = string.Empty;

    /// <summary>Id do cliente responsável pelo pet. Deve ser maior que zero.</summary>
    [Range(1, int.MaxValue)]
    public int ClienteId { get; set; }
}
