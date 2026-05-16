using System;
using System.Collections.Generic;
using System.Text;
using TEFLY.BLL.DTOs;

namespace TEFLY.BLL.Services.Interfaces
{
    public interface IChildService
    {
        Task<IEnumerable<ChildDto>> GetAllAsync();
        Task<ChildDto?> GetByIdAsync(int id);
        Task<IEnumerable<ChildDto>> GetByParentAsync(string userId);
        Task CreateAsync(ChildDto dto);
        Task UpdateAsync(ChildDto dto);
        Task DeleteAsync(int id);
    }

    public interface IVaccineService
    {
        Task<IEnumerable<VaccineDto>> GetAllAsync();
        Task<VaccineDto?> GetByIdAsync(int id);
        Task AddAsync(VaccineDto dto);
        Task UpdateAsync(VaccineDto dto);
        Task DeleteAsync(int id);
    }

    public interface IVaccineSideEffectService
    {
        Task<IEnumerable<VaccineSideEffectDto>> GetAllAsync();
        Task<VaccineSideEffectDto?> GetByIdAsync(int id);
        Task AddAsync(VaccineSideEffectDto dto);
        Task UpdateAsync(VaccineSideEffectDto dto);
        Task DeleteAsync(int id);
    }

    public interface IVaccineInventoryService
    {
        Task<IEnumerable<VaccineInventoryDto>> GetAllAsync();
        Task<VaccineInventoryDto?> GetByIdAsync(int id);
        Task AddAsync(VaccineInventoryDto dto);
        Task UpdateAsync(VaccineInventoryDto dto);
        Task DeleteAsync(int id);
    }

    public interface IVaccinationScheduleService
    {
        Task<IEnumerable<VaccinationScheduleDto>> GetAllAsync();
        Task<VaccinationScheduleDto?> GetByIdAsync(int id);
        Task AddAsync(VaccinationScheduleDto dto);
        Task UpdateAsync(VaccinationScheduleDto dto);
        Task DeleteAsync(int id);
    }

    public interface IVaccinationRecordService
    {
        Task<IEnumerable<VaccinationRecordDto>> GetAllAsync();
        Task<VaccinationRecordDto?> GetByIdAsync(int id);
        Task<IEnumerable<VaccinationRecordDto>> GetByChildAsync(int childId);
        Task CreateAsync(VaccinationRecordDto dto);
        Task UpdateAsync(VaccinationRecordDto dto);
        Task<IEnumerable<VaccinationRecordDto>> GetUpcomingRemindersAsync(int days);
        Task<IEnumerable<VaccinationRecordDto>> GetOverdueRecordsAsync();
        Task DeleteAsync(int id);
        Task MarkAsCompletedAsync(int recordId, DateTime administeredDate, string administeredBy, string? batchNumber); // جديد
    }

    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentDto>> GetAllAsync();
        Task<AppointmentDto?> GetByIdAsync(int id);
        Task<IEnumerable<AppointmentDto>> GetByChildAsync(int childId);
        Task CreateAsync(AppointmentDto dto);
        Task UpdateAsync(AppointmentDto dto);
        Task DeleteAsync(int id);
        Task BookAsync(AppointmentDto dto);
        Task CancelAppointmentAsync(int appointmentId);
        Task ConfirmAppointmentAsync(int appointmentId);
        Task<IEnumerable<AppointmentDto>> GetUpcomingAsync(int days);
    }

    public interface IAdverseReactionService
    {
        Task<IEnumerable<AdverseReactionDto>> GetAllAsync();
        Task<AdverseReactionDto?> GetByIdAsync(int id);
        Task<IEnumerable<AdverseReactionDto>> GetByChildAsync(int childId);
        Task CreateAsync(AdverseReactionDto dto);
        Task UpdateAsync(AdverseReactionDto dto);
        Task DeleteAsync(int id);
    }

    public interface IHealthcareProviderService
    {
        Task<IEnumerable<HealthcareProviderDto>> GetAllAsync();
        Task<HealthcareProviderDto?> GetByIdAsync(int id);
        Task AddAsync(HealthcareProviderDto dto);
        Task UpdateAsync(HealthcareProviderDto dto);
        Task DeleteAsync(int id);
    }

    public interface INotificationService
    {
        Task<IEnumerable<NotificationDto>> GetAllAsync();
        Task<NotificationDto?> GetByIdAsync(int id);
        Task<IEnumerable<NotificationDto>> GetByUserAsync(string userId);
        Task CreateAsync(NotificationDto dto);
        Task UpdateAsync(NotificationDto dto);
        Task DeleteAsync(int id);
    }
    public interface IComplaintService
    {
        Task<IEnumerable<ComplaintDto>> GetAllAsync();
        Task<ComplaintDto?> GetByIdAsync(int id);
        Task<IEnumerable<ComplaintDto>> GetByUserAsync(string userId);
        Task SubmitAsync(ComplaintDto dto);
        Task CreateAsync(ComplaintDto dto);
        Task UpdateAsync(ComplaintDto dto);
        Task DeleteAsync(int id);
    }
    public interface IAwarenessService
    {
        Task<IEnumerable<AwarenessDto>> GetAllAsync();
        Task<AwarenessDto?> GetByIdAsync(int id);
        Task<IEnumerable<AwarenessDto>> GetPublishedAsync();
        Task CreateAsync(AwarenessDto dto);
        Task UpdateAsync(AwarenessDto dto);
        Task DeleteAsync(int id);
    }

}
