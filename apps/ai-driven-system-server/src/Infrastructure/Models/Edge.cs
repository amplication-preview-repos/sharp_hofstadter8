using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AiDrivenSystem.Infrastructure.Models;

[Table("Edges")]
public class EdgeDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [StringLength(1000)]
    public string? FromNode { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public bool? IsOrthogonal { get; set; }

    [StringLength(1000)]
    public string? ToNode { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
