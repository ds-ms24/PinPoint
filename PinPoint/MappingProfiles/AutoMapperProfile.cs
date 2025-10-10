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
        // Pain Entry
        CreateMap<PainEntry, PainEntryReadOnlyVM>()
            .ForMember(dest => dest.SymptomName,
                opt => opt.MapFrom(src => src.Symptom != null ? src.Symptom.Name : null))
            .ForMember(dest => dest.TriggerName,
                    opt => opt.MapFrom(src => src.Trigger != null ? src.Trigger.Name : null))
            .ForMember(dest => dest.LocationName,
                    opt => opt.MapFrom(src => src.Location != null ? src.Location.Name : null));

        CreateMap<PainEntry, PainEntryDetailsVM>()
            .ForMember(dest => dest.SymptomName,
                opt => opt.MapFrom(src => src.Symptom != null ? src.Symptom.Name : null))
            .ForMember(dest => dest.TriggerName,
                opt => opt.MapFrom(src => src.Trigger != null ? src.Trigger.Name : null))
            .ForMember(dest => dest.LocationName,
                opt => opt.MapFrom(src => src.Location != null ? src.Location.Name : null));

        CreateMap<PainEntryCreateVM, PainEntry>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Symptom, opt => opt.Ignore())
            .ForMember(dest => dest.Trigger, opt => opt.Ignore())
            .ForMember(dest => dest.Location, opt => opt.Ignore());

        CreateMap<PainEntry, PainEntryEditVM>()
            .ForMember(dest => dest.SymptomName,
                opt => opt.MapFrom(src => src.Symptom != null ? src.Symptom.Name : null))
            .ForMember(dest => dest.TriggerName,
                opt => opt.MapFrom(src => src.Trigger != null ? src.Trigger.Name : null))
            .ForMember(dest => dest.LocationName,
                opt => opt.MapFrom(src => src.Location != null ? src.Location.Name : null));

        CreateMap<PainEntryEditVM, PainEntry>()
            .ForMember(dest => dest.Symptom, opt => opt.Ignore())
            .ForMember(dest => dest.Trigger, opt => opt.Ignore())
            .ForMember(dest => dest.Location, opt => opt.Ignore());
        
        // Symptom
        CreateMap<Symptom, SymptomReadOnlyVM>();
        CreateMap<SymptomCreateVM, Symptom>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PainEntries, opt => opt.Ignore());
        CreateMap<Symptom, SymptomEditVM>();
        CreateMap<SymptomEditVM, Symptom>()
            .ForMember(dest => dest.PainEntries, opt => opt.Ignore());

        
        // Trigger
        CreateMap<Trigger, TriggerReadOnlyVM>();
        CreateMap<TriggerCreateVM, Trigger>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PainEntries, opt => opt.Ignore());
        CreateMap<TriggerEditVM, Trigger>()
            .ForMember(dest => dest.PainEntries, opt => opt.Ignore());

        // Location
        CreateMap<Location, LocationReadOnlyVM>();
        CreateMap<LocationCreateVM, Location>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PainEntries, opt => opt.Ignore());
        CreateMap<Location, LocationEditVM>();
        CreateMap<LocationEditVM, Location>()
            .ForMember(dest => dest.PainEntries, opt => opt.Ignore());
        
    }
}