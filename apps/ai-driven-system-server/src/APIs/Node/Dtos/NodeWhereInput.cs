using AiDrivenSystem.Core.Enums;

namespace AiDrivenSystem.APIs.Dtos;

public class NodeWhereInput
{
    public List<string>? AiActions { get; set; }

    public string? AiResponse { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Description { get; set; }

    public string? Id { get; set; }

    public StatusEnum? Status { get; set; }

    public string? Title { get; set; }

    public string? TypeField { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public List<string>? UserControls { get; set; }
}
