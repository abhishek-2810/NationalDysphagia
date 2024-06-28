using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NationalDysphagiaCareGuid.Models;

public partial class Patient
{
    public int PatientId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string Gender { get; set; } = null!;

    public int Age { get; set; }

    public string? Email { get; set; }

    public long PhoneNumber { get; set; }

    [JsonIgnore]
    public virtual ICollection<PatientCurrentState> PatientCurrentStates { get; set; } = new List<PatientCurrentState>();

    [JsonIgnore]
    public virtual ICollection<PatientHistory> PatientHistories { get; set; } = new List<PatientHistory>();
}
