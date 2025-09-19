﻿using AutoMapper;
using PinPoint.Data;
using PinPoint.Models.Symptoms;

namespace LeaveManagementSystem.Web.MappingProfiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile() 
    {
        CreateMap<Symptom, SymptomReadOnlyVM>();
        CreateMap<SymptomCreateVM, Symptom>();
        CreateMap<SymptomEditVM, Symptom>().ReverseMap();
          
        
    }
}