using System.ComponentModel;
using System.Text.Json.Serialization;

namespace NationalDysphagiaCareGuid.Models;

public partial class PatientCurrentState
{
    public int CurrentStateId { get; set; }

    [DisplayName("Do you frequently cough/choke while eating?")]
    public int CoughingChokingWhileEating { get; set; }
    
    [DisplayName("Is your food falling out from your mouth while trying to eat and unless to clear the food?")]
    public int DroolingWhileEating { get; set; }

    [DisplayName("Do you feel like any burning sensation in your chest?")]
    public int FeelingChestBurningAfterEating { get; set; }
    
    [DisplayName("Do you feel like food is stucking in your middle of your chest after every swallow?")]
    public int FeelingFoodStuckingInChest { get; set; }

    [DisplayName("Are you feeling like food sticking in your throat and unless to swallow it down?")]
    public int FeelingLikeFoodStuckedInThroat { get; set; }
    
    [DisplayName("Do you feel vomiting sensation after eating?")]
    public int FeelingVomitingAfterEating { get; set; }

    [DisplayName("Are you taking more time to swallow your food from mouth to throat?")]
    public int HoldingFoodInMouthForLongTime { get; set; }
    
    [DisplayName("Is there anything else about your condition/swallowing difficulty that you want to mention?")]
    public string? Other { get; set; }

    [DisplayName("Do you have any difficulty in swallowing medicines (tablets), solids, blended food or liquids?")]
    public int PainWhileSwallowing { get; set; }
    
    public int Patient { get; set; }

    [DisplayName("Do you feel any respiratory distress (breathing difficulty) while eating?")]
    public int RespiratoryDistressWhileEating { get; set; }

    public int UnableToSwallowTheMedication { get; set; }

    [JsonIgnore]
    public virtual Patient? PatientNavigation { get; set; }
}
