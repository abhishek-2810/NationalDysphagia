using System.ComponentModel;
using System.Text.Json.Serialization;

namespace NationalDysphagiaCareGuid.Models;

public partial class PatientHistory
{
    public int HistoryId { get; set; }

    [DisplayName("Any past history or current diagnosis of Head and Neck cancer?")]
    public int Cancer { get; set; }
    
    [DisplayName("Are you compromising with your diet/cut downing your meals?")]
    public int CompromisingDiet { get; set; }
    
    [DisplayName("Any past history of Dysphagia (swallowing difficulty)?")]
    public int Dysphagia { get; set; }

    [DisplayName("How do you currently manage the nutrition?")]
    public string FeedingMode { get; set; } = null!;

    [DisplayName("Any Gastrointestinal issues like GERD?")]
    public int GastrologicalIssue { get; set; }

    [DisplayName("Are there any other medial issues please mention below.?")]
    public string? Other { get; set; }

    public int Patient { get; set; }

    [DisplayName("Any Psychological issues and are you under any psychological medication?")]
    public int PsychologicalIssues { get; set; }

    [DisplayName("History of Aspiration or any respiratory illness present?")]
    public int RespiratoryIssue { get; set; }

    [DisplayName("Any past history or current diagnosis of Brain stroke?")]
    public int Stroke { get; set; }

    [DisplayName("History of any previous head and neck surgeries/brain surgeries?")]
    public int SurgeriesInPast { get; set; }

    [JsonIgnore]
    public virtual Patient? PatientNavigation { get; set; }
}
