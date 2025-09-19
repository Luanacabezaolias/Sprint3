namespace Challenge_sprint.src.Domain.Entities;

public class SelfAssessment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public int Score { get; set; }
    public string RiskLevel { get; set; } = "Unknown";
    public string? Notes { get; set; }
}
