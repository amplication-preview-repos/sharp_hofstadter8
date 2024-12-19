using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AiDrivenSystem.Core.Enums;

namespace AiDrivenSystem.Infrastructure.Models;

[Table("Nodes")]
public class NodeDbModel
{
    public List<AiActionDbModel>? AiActions { get; set; } = new List<AiActionDbModel>();

    [StringLength(1000)]
    public string? AiResponse { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public StatusEnum? Status { get; set; }

    [StringLength(1000)]
    public string? Title { get; set; }

    [StringLength(1000)]
    public string? TypeField { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    public List<UserControlDbModel>? UserControls { get; set; } = new List<UserControlDbModel>();
}
