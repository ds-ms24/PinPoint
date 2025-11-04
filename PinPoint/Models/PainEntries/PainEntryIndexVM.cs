namespace PinPoint.Models.PainEntries
{
    public class PainEntryIndexVM
    {
        public List<PainEntryReadOnlyVM> PainEntries { get; set; } = new();
        public string? CurrentFilter { get; set; }
        public string? CurrentSort { get; set; }

        // Sort toggle properties
        public string DateSortParam => string.IsNullOrEmpty(CurrentSort) ? "date_desc" : "";
        public string TimeSortParam => CurrentSort == "time" ? "time_desc" : "time";
        public string IntensitySortParam => CurrentSort == "intensity" ? "intensity_desc" : "intensity";
        public string DescriptionSortParam => CurrentSort == "description" ? "description_desc" : "description";
        public string DurationSortParam => CurrentSort == "duration" ? "duration_desc" : "duration";
        public string ActivitiesSortParam => CurrentSort == "activities" ? "activities_desc" : "activities";
        public string ReliefMethodsSortParam => CurrentSort == "relief_methods" ? "relief_methods_desc" : "relief_methods";
        public string ReliefEffectivenessSortParam => CurrentSort == "relief_effectiveness" ? "relief_effectiveness_desc" : "relief_effectiveness";
        public string AdditionalNotesSortParam => CurrentSort == "notes" ? "notes_desc" : "notes";
    }
}
