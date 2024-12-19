namespace AiDrivenSystem.APIs.Dtos;

public class EdgeUpdateInput
{
    public DateTime? CreatedAt { get; set; }

    public string? FromNode { get; set; }

    public string? Id { get; set; }

    public bool? IsOrthogonal { get; set; }

    public string? ToNode { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
