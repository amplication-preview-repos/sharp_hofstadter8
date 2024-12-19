using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AiDrivenSystem.Core.Enums;

namespace AiDrivenSystem.Infrastructure.Models;

[Table("AiActions")]
public class AiActionDbModel
{
    [StringLength(1000)]
    public string? ActionName { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? NodeId { get; set; }

    [ForeignKey(nameof(NodeId))]
    public NodeDbModel? Node { get; set; } = null;

    public string? Output { get; set; }

    public StatusEnum? Status { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
