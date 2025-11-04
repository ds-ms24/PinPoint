﻿using AutoMapper;
using PinPoint.Data;
using PinPoint.Models.DeleteRequests;
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
            .ForMember(dest => dest.SymptomNames, opt => opt.MapFrom(src => 
                string.Join(", ", src.PainEntrySymptoms.Select(q => q.Symptom.Name))))
            .ForMember(dest => dest.LocationNames, opt => opt.MapFrom(src => 
                string.Join(", ", src.PainEntryLocations.Select(q => q.Location.Name))))
            .ForMember(dest => dest.TriggerNames, opt => opt.MapFrom(src => 
                string.Join(", ", src.PainEntryTriggers.Select(q => q.Trigger.Name))))
            .ForMember(dest => dest.SymptomIds,
                opt => opt.MapFrom(src => src.PainEntrySymptoms.Select(ps => ps.SymptomId).ToList()))
            .ForMember(dest => dest.LocationIds,
                opt => opt.MapFrom(src => src.PainEntryLocations.Select(pl => pl.LocationId).ToList()))
            .ForMember(dest => dest.TriggerIds,
                opt => opt.MapFrom(src => src.PainEntryTriggers.Select(pt => pt.TriggerId).ToList()));

        CreateMap<PainEntryCreateVM, PainEntry>()
            // Ignore to handle in service layer
            .ForMember(dest => dest.PainEntrySymptoms, opt => opt.Ignore())
            .ForMember(dest => dest.PainEntryLocations, opt => opt.Ignore())
            .ForMember(dest => dest.PainEntryTriggers, opt => opt.Ignore())    
            // Ignore properties not in CreateVM
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<PainEntry, PainEntryEditVM>()
            .ForMember(dest => dest.SymptomIds, opt => opt.MapFrom(src => 
                src.PainEntrySymptoms.Select(ps => ps.SymptomId).ToList()))
            .ForMember(dest => dest.LocationIds, opt => opt.MapFrom(src => 
                src.PainEntryLocations.Select(pl => pl.LocationId).ToList()))
            .ForMember(dest => dest.TriggerIds, opt => opt.MapFrom(src => 
                src.PainEntryTriggers.Select(pt => pt.TriggerId).ToList()));

        CreateMap<PainEntryEditVM, PainEntry>()
            // Ignore to handle in service layer
            .ForMember(dest => dest.PainEntrySymptoms, opt => opt.Ignore())
            .ForMember(dest => dest.PainEntryLocations, opt => opt.Ignore())
            .ForMember(dest => dest.PainEntryTriggers, opt => opt.Ignore())    
            // Ignore properties not in EditVM
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<PainEntry, PainEntryDetailsVM>()
            .ForMember(dest => dest.SymptomNames, opt => opt.MapFrom(src => 
                string.Join(", ", src.PainEntrySymptoms.Select(q => q.Symptom.Name))))
            .ForMember(dest => dest.LocationNames, opt => opt.MapFrom(src => 
                string.Join(", ", src.PainEntryLocations.Select(q => q.Location.Name))))
            .ForMember(dest => dest.TriggerNames, opt => opt.MapFrom(src => 
                string.Join(", ", src.PainEntryTriggers.Select(q => q.Trigger.Name))));
        
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

        // Delete Requests
        CreateMap<DeleteRequest, DeleteRequestReadOnlyVM>()
            .ForMember(dest => dest.EntryDate, opt => opt.MapFrom(src => src.PainEntry.EntryDate))
            .ForMember(dest => dest.EntryTime, opt => opt.MapFrom(src => src.PainEntry.EntryTime))
            .ForMember(dest => dest.PainDescription, opt => opt.MapFrom(src => src.PainEntry.PainDescription))
            .ForMember(dest => dest.PainIntensity, opt => opt.MapFrom(src => src.PainEntry.PainIntensity))
            .ForMember(dest => dest.DurationMinutes, opt => opt.MapFrom(src => src.PainEntry.DurationMinutes))
            .ForMember(dest => dest.ActivitiesBeforePain, opt => opt.MapFrom(src => src.PainEntry.ActivitiesBeforePain))
            .ForMember(dest => dest.ReliefMethodsTried, opt => opt.MapFrom(src => src.PainEntry.ReliefMethodsTried))
            .ForMember(dest => dest.ReliefEffectiveness, opt => opt.MapFrom(src => src.PainEntry.ReliefEffectiveness))
            .ForMember(dest => dest.AdditionalNotes, opt => opt.MapFrom(src => src.PainEntry.AdditionalNotes))
            .ForMember(dest => dest.SymptomNames, opt => opt.MapFrom(src => 
                string.Join(", ", src.PainEntry.PainEntrySymptoms.Select(pes => pes.Symptom.Name))))
            .ForMember(dest => dest.LocationNames, opt => opt.MapFrom(src => 
                string.Join(", ", src.PainEntry.PainEntryLocations.Select(pel => pel.Location.Name))))
            .ForMember(dest => dest.TriggerNames, opt => opt.MapFrom(src => 
                string.Join(", ", src.PainEntry.PainEntryTriggers.Select(pet => pet.Trigger.Name))));
    }
}