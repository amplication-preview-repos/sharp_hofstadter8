using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AiDrivenSystem.Infrastructure.Models;

[Table("UserControls")]
public class UserControlDbModel
{
    [StringLength(1000)]
    public string? ControlType { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? NodeId { get; set; }

    [ForeignKey(nameof(NodeId))]
    public NodeDbModel? Node { get; set; } = null;

    public string? Parameters { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
