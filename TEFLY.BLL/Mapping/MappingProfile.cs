using AutoMapper;
using TEFLY.DAL.Models;
using TEFLY.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEFLY.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
        // ── ApplicationUser ──────────────────────────────
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(d => d.Phone, o => o.MapFrom(s => s.PhoneNumber));
            CreateMap<UserDto, ApplicationUser>()
                .ForMember(d => d.PhoneNumber, o => o.MapFrom(s => s.Phone))
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.Email));

            
            // ── HealthcareProvider ───────────────────────────
            CreateMap<HealthcareProvider, HealthcareProviderDto>().ReverseMap();

    
            // ── AdverseReaction ──────────────────────────────
            CreateMap<AdverseReaction, AdverseReactionDto>()
                .ForMember(d => d.ChildName, o => o.MapFrom(s => s.Child != null ? s.Child.Name : string.Empty))
                .ForMember(d => d.VaccineName, o => o.MapFrom(s => s.Vaccine != null ? s.Vaccine.Name : string.Empty));
            CreateMap<AdverseReactionDto, AdverseReaction>()
                .ForMember(d => d.Child, o => o.Ignore())
                .ForMember(d => d.Vaccine, o => o.Ignore());

            // ── VaccineInventory ─────────────────────────────
            CreateMap<VaccineInventory, VaccineInventoryDto>()
                .ForMember(d => d.VaccineName, o => o.MapFrom(s => s.Vaccine != null ? s.Vaccine.Name : string.Empty))
                .ForMember(d => d.ProviderName, o => o.MapFrom(s => s.Provider != null ? s.Provider.Name : string.Empty));
            CreateMap<VaccineInventoryDto, VaccineInventory>()
                .ForMember(d => d.Vaccine, o => o.Ignore())
                .ForMember(d => d.Provider, o => o.Ignore());

            // ── VaccinationRecord ────────────────────────────
            CreateMap<VaccinationRecord, VaccinationRecordDto>()
                .ForMember(d => d.ChildName, o => o.MapFrom(s => s.Child != null ? s.Child.Name : string.Empty))
                .ForMember(d => d.VaccineName, o => o.MapFrom(s => s.Vaccine != null ? s.Vaccine.Name : string.Empty))
                .ForMember(d => d.ProviderName, o => o.MapFrom(s => s.Provider != null ? s.Provider.Name : string.Empty));
            CreateMap<VaccinationRecordDto, VaccinationRecord>()
                .ForMember(d => d.Child, o => o.Ignore())
                .ForMember(d => d.Vaccine, o => o.Ignore())
                .ForMember(d => d.Provider, o => o.Ignore());

            // ── VaccinationSchedule ──────────────────────────
            CreateMap<VaccinationSchedule, VaccinationScheduleDto>()
                .ForMember(d => d.VaccineName, o => o.MapFrom(s => s.Vaccine != null ? s.Vaccine.Name : string.Empty));
            CreateMap<VaccinationScheduleDto, VaccinationSchedule>()
                .ForMember(d => d.Vaccine, o => o.Ignore());

            // ── Appointment ──────────────────────────────────
            CreateMap<Appointment, AppointmentDto>()
                .ForMember(d => d.ChildName, o => o.MapFrom(s => s.Child != null ? s.Child.Name : string.Empty))
                .ForMember(d => d.VaccineName, o => o.MapFrom(s => s.Vaccine != null ? s.Vaccine.Name : string.Empty))
                .ForMember(d => d.ProviderName, o => o.MapFrom(s => s.Provider != null ? s.Provider.Name : string.Empty));
            CreateMap<AppointmentDto, Appointment>()
                .ForMember(d => d.Child, o => o.Ignore())
                .ForMember(d => d.Vaccine, o => o.Ignore())
                .ForMember(d => d.Provider, o => o.Ignore());

            // ── Notification ─────────────────────────────────
            CreateMap<Notification, NotificationDto>().ReverseMap();
        }
    }
}
