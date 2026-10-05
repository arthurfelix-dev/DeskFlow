using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DeskFlow.API.Models.Entities;

public class Interacao
{
    public int Id { get; set; }

    public int ChamadoId { get; set; }

    [Required(ErrorMessage = "O autor é obrigatório.")]
    public string Autor { get; set; } = string.Empty;

    [Required(ErrorMessage = "A mensagem é obrigatória.")]
    public string Mensagem { get; set; } = string.Empty;

    public DateTime DataRegistro { get; set; }

    [JsonIgnore]
    public Chamado? Chamado { get; set; }
}