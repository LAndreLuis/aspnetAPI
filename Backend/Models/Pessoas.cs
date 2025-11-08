using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Pessoas
{
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;
    [Required]
    public int Idade { get; set; }
    [Required]
    [MaxLength(16)]
    public string Telefone { get; set; } = string.Empty;
    [Required]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;
    [Required]
    [MaxLength(100)]
    public string Nascimento { get; set; } = string.Empty;


}
