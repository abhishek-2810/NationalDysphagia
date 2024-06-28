using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NationalDysphagiaCareGuid.Models;

public partial class PatientHistory
{
    public int HistoryId { get; set; }

    public int Patient { get; set; }

    public int Dysphagia { get; set; }

    public int RespiratoryIssue { get; set; }

    public int Stroke { get; set; }

    public int Cancer { get; set; }

    public int GastrologicalIssue { get; set; }

    public int SurgeriesInPast { get; set; }

    public string FeedingMode { get; set; } = null!;

    public string? PastSurgeriesDescription { get; set; }

    [JsonIgnore]
    public virtual Patient? PatientNavigation { get; set; }
}
