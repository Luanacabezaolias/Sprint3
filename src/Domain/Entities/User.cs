namespace Challenge_sprint.src.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<SelfAssessment> Assessments { get; set; } = new List<SelfAssessment>();
    public ICollection<JournalEntry> JournalEntries { get; set; } = new List<JournalEntry>();
    public ICollection<Goal> Goals { get; set; } = new List<Goal>();
    public ICollection<Alert> Alerts { get; set; } = new List<Alert>();
}
