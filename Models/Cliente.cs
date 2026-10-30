using System.ComponentModel.DataAnnotations;

namespace LhPets.Models;

/// <summary>
/// Representa um cliente da LH-Pets.
/// </summary>
public class Cliente
{
    /// <summary>Identificador do cliente. Gerado automaticamente pela API.</summary>
    public int Id { get; set; }

    /// <summary>Nome completo do cliente (3 a 100 caracteres).</summary>
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// CPF do cliente. O [Required] só exige que seja informado;
    /// não valida os dígitos verificadores.
    /// </summary>
    [Required]
    public string Cpf { get; set; } = string.Empty;

    /// <summary>E-mail do cliente, em formato válido.</summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}
