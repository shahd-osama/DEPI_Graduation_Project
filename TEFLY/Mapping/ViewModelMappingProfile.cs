using AutoMapper;
using TEFLY.BLL.DTOs;
using TEFLY.ViewModels;


namespace TEFLY.Mapping
{
    public class ViewModelMappingProfile : Profile
    {
        public ViewModelMappingProfile()
        {
            // Example mapping configuration:
            // CreateMap<SourceModel, DestinationViewModel>();

            // Notification
            CreateMap<NotificationDto, NotificationViewModel>().ReverseMap();
            CreateMap<VaccinationRecordDto, VaccinationRecordViewModel>().ReverseMap();
            CreateMap<VaccineDto, VaccineViewModel>().ReverseMap();
            CreateMap<AppointmentDto, AppointmentViewModel>().ReverseMap();
            CreateMap<HealthcareProviderDto, HealthcareProviderViewModel>().ReverseMap();
            CreateMap<AdverseReactionDto, AdverseReactionViewModel>().ReverseMap();
        }
    }
}
