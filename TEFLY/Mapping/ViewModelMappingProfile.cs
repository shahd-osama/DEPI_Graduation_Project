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
            CreateMap<ChildDto, ChildViewModel>().ReverseMap();
            CreateMap<AwarenessDto, AwarenessViewModel>().ReverseMap();
            CreateMap<ComplaintDto, ComplaintViewModel>().ReverseMap();

            CreateMap<NotificationDto, NotificationViewModel>().ReverseMap();
            CreateMap<VaccinationRecordDto, VaccinationRecordViewModel>().ReverseMap();


        }
    }
}
