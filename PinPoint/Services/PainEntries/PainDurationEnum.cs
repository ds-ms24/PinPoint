using Microsoft.CodeAnalysis.Options;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace PinPoint.Services.PainEntries
{   
    public enum PainDurationEnum
    {   
        [Display(Name = "Less than 1 minute")]
        OneMinute = 0,
        [Display(Name = "1 - 5 minutes")]
        FiveMinutes = 1,
        [Display(Name = "5 - 15 minutes")]
        ThirtyMinutes = 2,
        [Display(Name = "15 - 30 minutes")]
        SixtyMinutes = 3,
        [Display(Name = "30 - 60 minutes")]
        OneHour = 4,
        [Display(Name = "1 - 2 hours")]
        TwoHours = 5,
        [Display(Name = "2 - 4 hours")]
        FourHours = 6,
        [Display(Name = "4 - 8 hours")]
        EightHours = 7,
        [Display(Name = "8 - 12 hours")]
        TwelveHours = 8,
        [Display(Name = "More than 12 hours")]
        OverTwelveHours = 9,
        [Display(Name = "All day (constant)")]
        AllDayConstant = 10
    }
}
