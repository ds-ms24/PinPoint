﻿using AutoMapper;
using PinPoint.Data;
using PinPoint.Models.Locations;
using PinPoint.Models.PainEntries;
using PinPoint.Models.PainEntry;
using PinPoint.Models.Symptoms;
using PinPoint.Models.Triggers;

namespace LeaveManagementSystem.Web.MappingProfiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile() 
    {
        CreateMap<Symptom, SymptomReadOnlyVM>();
        CreateMap<SymptomCreateVM, Symptom>();
        CreateMap<SymptomEditVM, Symptom>().ReverseMap();

        CreateMap<PainEntry, PainEntryReadOnlyVM>();
        CreateMap<PainEntryCreateVM, PainEntry>();
        CreateMap<PainEntryEditVM, PainEntry>().ReverseMap();

        CreateMap<Trigger, TriggerReadOnlyVM>();
        CreateMap<TriggerCreateVM, Trigger>();
        CreateMap<TriggerEditVM, Trigger>().ReverseMap();

        CreateMap<Location, LocationReadOnlyVM>();
        CreateMap<LocationCreateVM, Location>();
        CreateMap<LocationEditVM, Location>().ReverseMap();
        
    }
}