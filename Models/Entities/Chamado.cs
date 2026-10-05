using System.ComponentModel.DataAnnotations;

namespace DeskFlow.API.Models.Entities;

public class Chamado
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O título é obrigatório.")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "A prioridade é obrigatória.")]
    [RegularExpression(
        "^(Baixa|Media|Alta)$",
        ErrorMessage = "A prioridade deve ser Baixa, Media ou Alta.")]
    public string Prioridade { get; set; } = string.Empty;

    public string Status { get; set; } = "Aberto";

    [Required(ErrorMessage = "O nome do solicitante é obrigatório.")]
    public string SolicitanteNome { get; set; } = string.Empty;

    public DateTime DataAbertura { get; set; }

    public DateTime? DataFechamento { get; set; }

    public string? Solucao { get; set; }

    [Required(ErrorMessage = "A categoria é obrigatória.")]
    public int CategoriaId { get; set; }

    public Categoria? Categoria { get; set; }

    public ICollection<Interacao> Interacoes { get; set; } = new List<Interacao>();
}