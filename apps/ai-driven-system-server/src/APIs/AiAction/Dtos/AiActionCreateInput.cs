using AiDrivenSystem.Core.Enums;

namespace AiDrivenSystem.APIs.Dtos;

public class AiActionCreateInput
{
    public string? ActionName { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public Node? Node { get; set; }

    public string? Output { get; set; }

    public StatusEnum? Status { get; set; }

    public DateTime UpdatedAt { get; set; }
}
