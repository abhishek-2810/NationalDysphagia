using System.ComponentModel;
using System.Text.Json.Serialization;

namespace NationalDysphagiaCareGuid.Models;

public partial class Patient
{
    public int PatientId { get; set; }

    [DisplayName("Age")]
    public int Age { get; set; }

    [DisplayName("Email ID")]
    public string? Email { get; set; }
    
    [DisplayName("First Name")]
    public string FirstName { get; set; } = null!;

    [DisplayName("Gender")]
    public string Gender { get; set; } = null!;
    
    [DisplayName("Last Name")]
    public string? LastName { get; set; }

    [DisplayName("Mobile Number")]
    public long PhoneNumber { get; set; }

    [DisplayName("Registered On")]
    public string? RegistrationDate { get; set; }

    [JsonIgnore]
    [DisplayName("Current State Of Patient")]
    public virtual ICollection<PatientCurrentState> PatientCurrentStates { get; set; } = new List<PatientCurrentState>();

    [JsonIgnore]
    [DisplayName("History Of Patient")]
    public virtual ICollection<PatientHistory> PatientHistories { get; set; } = new List<PatientHistory>();
}
