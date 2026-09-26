using Sejily.API.Models.Enums;
using System.Numerics;

namespace Sejily.API.Models.Entities;

public class User
{
    public int UserId { get; set; }

    public string NationalID { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string Phone { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public BloodType BloodType { get; set; }

    // Navigation properties
    public Patient? Patient { get; set; }

    public Doctor? Doctor { get; set; }

    public Guardian? Guardian { get; set; }
}