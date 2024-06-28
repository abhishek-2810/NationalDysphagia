using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NationalDysphagiaCareGuid.Models;

public partial class PatientCurrentState
{
    public int CurrentStateId { get; set; }

    public int Patient { get; set; }

    public int PainWhileSwallowing { get; set; }

    public int UnableToSwallowTheMedication { get; set; }

    public int CoughingChokingWhileEating { get; set; }

    public int RespiratoryDistressWhileEating { get; set; }

    public int HoldingFoodInMouthForLongTime { get; set; }

    public int DroolingWhileEating { get; set; }

    public int FeelingLikeFoodStuckedInThroat { get; set; }

    public int FeelingVomitingAfterEating { get; set; }

    public int FeelingChestBurningAfterEating { get; set; }

    public int FeelingFoodStuckingInChest { get; set; }
    
    [JsonIgnore]
    public virtual Patient? PatientNavigation { get; set; }
}
