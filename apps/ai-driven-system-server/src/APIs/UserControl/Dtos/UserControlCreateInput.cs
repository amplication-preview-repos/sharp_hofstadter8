namespace AiDrivenSystem.APIs.Dtos;

public class UserControlCreateInput
{
    public string? ControlType { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public Node? Node { get; set; }

    public string? Parameters { get; set; }

    public DateTime UpdatedAt { get; set; }
}
