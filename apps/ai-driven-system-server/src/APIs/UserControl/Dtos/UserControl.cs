namespace AiDrivenSystem.APIs.Dtos;

public class UserControl
{
    public string? ControlType { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Id { get; set; }

    public string? Node { get; set; }

    public string? Parameters { get; set; }

    public DateTime UpdatedAt { get; set; }
}
