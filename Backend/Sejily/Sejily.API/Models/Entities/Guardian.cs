namespace Sejily.API.Models.Entities;

public class Guardian
{
    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public ICollection<GuardianPatient> Patients { get; set; }
        = new List<GuardianPatient>();
}