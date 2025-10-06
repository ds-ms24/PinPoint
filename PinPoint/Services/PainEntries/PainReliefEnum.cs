using System.ComponentModel.DataAnnotations;

namespace PinPoint.Services.PainEntries
{
    public enum PainReliefEnum
    {   
        [Display(Name = "No Relief")]
        NoRelief = 0,
        [Display(Name = "Almost None")]
        AlmostNone = 1,
        Mild = 2,
        Uncomfortable = 3,
        Moderate = 4,
        Distracting = 5,
        Distressing = 6,
        Unmanagable = 7,
        Intense = 8,
        Severe = 9,
        [Display(Name = "Unable To Move")]
        UnableToMove = 10
    }
}
