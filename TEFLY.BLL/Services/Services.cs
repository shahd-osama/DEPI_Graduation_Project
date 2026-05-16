using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TEFLY.BLL.DTOs;
using TEFLY.BLL.Services.Interfaces;
using TEFLY.DAL.Data;
using TEFLY.DAL.Models;
using TEFLY.DAL.Repositories.Interfaces;

namespace TEFLY.BLL.Services
{
    // ─── Child ───────────────────────────────────────────────
    public class ChildService : IChildService
    {
        private readonly IGenericRepository<Child> _repo;
        private readonly IMapper _mapper;

        public ChildService(IGenericRepository<Child> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ChildDto>> GetAllAsync()
            => _mapper.Map<IEnumerable<ChildDto>>(await _repo.GetAllAsync());

        public async Task<ChildDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity is null ? null : _mapper.Map<ChildDto>(entity);
        }

        public async Task<IEnumerable<ChildDto>> GetByParentAsync(string userId)
        {
            var children = await _repo.FindAsync(c => c.UserID == userId);
            return _mapper.Map<IEnumerable<ChildDto>>(children);
        }

        public async Task CreateAsync(ChildDto dto)
        {
            await _repo.AddAsync(_mapper.Map<Child>(dto));
            await _repo.SaveAsync();
        }

        public async Task UpdateAsync(ChildDto dto)
        {
            _repo.Update(_mapper.Map<Child>(dto));
            await _repo.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is not null)
            {
                _repo.Delete(entity);
                await _repo.SaveAsync();
            }
        }
    }

    // ─── Vaccine ─────────────────────────────────────────────
    public class VaccineService : IVaccineService
    {
        private readonly IGenericRepository<Vaccine> _repo;
        private readonly IMapper _mapper;

        public VaccineService(IGenericRepository<Vaccine> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VaccineDto>> GetAllAsync()
            => _mapper.Map<IEnumerable<VaccineDto>>(await _repo.GetAllAsync());

        public async Task<VaccineDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity is null ? null : _mapper.Map<VaccineDto>(entity);
        }

        public async Task AddAsync(VaccineDto dto)
        {
            await _repo.AddAsync(_mapper.Map<Vaccine>(dto));
            await _repo.SaveAsync();
        }

        public async Task UpdateAsync(VaccineDto dto)
        {
            _repo.Update(_mapper.Map<Vaccine>(dto));
            await _repo.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is not null)
            {
                _repo.Delete(entity);
                await _repo.SaveAsync();
            }
        }
    }
    // ─── VaccineSideEffect ───────────────────────────────────
    public class VaccineSideEffectService : IVaccineSideEffectService
    {
        private readonly IGenericRepository<VaccineSideEffect> _repo;
        private readonly IMapper _mapper;

        public VaccineSideEffectService(IGenericRepository<VaccineSideEffect> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VaccineSideEffectDto>> GetAllAsync()
            => _mapper.Map<IEnumerable<VaccineSideEffectDto>>(await _repo.GetAllAsync());

        public async Task<VaccineSideEffectDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity is null ? null : _mapper.Map<VaccineSideEffectDto>(entity);
        }

        public async Task AddAsync(VaccineSideEffectDto dto)
        {
            await _repo.AddAsync(_mapper.Map<VaccineSideEffect>(dto));
            await _repo.SaveAsync();
        }

        public async Task UpdateAsync(VaccineSideEffectDto dto)
        {
            _repo.Update(_mapper.Map<VaccineSideEffect>(dto));
            await _repo.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is not null) { _repo.Delete(entity); await _repo.SaveAsync(); }
        }
    }

    // ─── VaccineInventory ────────────────────────────────────
    public class VaccineInventoryService : IVaccineInventoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public VaccineInventoryService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VaccineInventoryDto>> GetAllAsync()
        {
            var list = await _context.VaccineInventories
                .Include(v => v.Vaccine)
                .Include(v => v.Provider)
                .ToListAsync();
            return _mapper.Map<IEnumerable<VaccineInventoryDto>>(list);
        }

        public async Task<VaccineInventoryDto?> GetByIdAsync(int id)
        {
            var entity = await _context.VaccineInventories
                .Include(v => v.Vaccine)
                .Include(v => v.Provider)
                .FirstOrDefaultAsync(v => v.InventoryID == id);
            return entity is null ? null : _mapper.Map<VaccineInventoryDto>(entity);
        }

        public async Task AddAsync(VaccineInventoryDto dto)
        {
            _context.VaccineInventories.Add(_mapper.Map<VaccineInventory>(dto));
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(VaccineInventoryDto dto)
        {
            _context.VaccineInventories.Update(_mapper.Map<VaccineInventory>(dto));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.VaccineInventories.FindAsync(id);
            if (entity is not null) { _context.VaccineInventories.Remove(entity); await _context.SaveChangesAsync(); }
        }
    }

    // ─── VaccinationSchedule ─────────────────────────────────
    public class VaccinationScheduleService : IVaccinationScheduleService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public VaccinationScheduleService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VaccinationScheduleDto>> GetAllAsync()
        {
            var list = await _context.VaccinationSchedules.Include(s => s.Vaccine).ToListAsync();
            return _mapper.Map<IEnumerable<VaccinationScheduleDto>>(list);
        }

        public async Task<VaccinationScheduleDto?> GetByIdAsync(int id)
        {
            var entity = await _context.VaccinationSchedules.Include(s => s.Vaccine)
                .FirstOrDefaultAsync(s => s.ScheduleID == id);
            return entity is null ? null : _mapper.Map<VaccinationScheduleDto>(entity);
        }

        public async Task AddAsync(VaccinationScheduleDto dto)
        {
            _context.VaccinationSchedules.Add(_mapper.Map<VaccinationSchedule>(dto));
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(VaccinationScheduleDto dto)
        {
            _context.VaccinationSchedules.Update(_mapper.Map<VaccinationSchedule>(dto));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.VaccinationSchedules.FindAsync(id);
            if (entity is not null) { _context.VaccinationSchedules.Remove(entity); await _context.SaveChangesAsync(); }
        }
    }

    // ─── VaccinationRecord ───────────────────────────────────
    public class VaccinationRecordService : IVaccinationRecordService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public VaccinationRecordService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VaccinationRecordDto>> GetAllAsync()
        {
            var list = await _context.VaccinationRecords
                .Include(r => r.Child).Include(r => r.Vaccine).Include(r => r.Provider)
                .ToListAsync();
            return _mapper.Map<IEnumerable<VaccinationRecordDto>>(list);
        }

        public async Task<VaccinationRecordDto?> GetByIdAsync(int id)
        {
            var entity = await _context.VaccinationRecords
                .Include(r => r.Child).Include(r => r.Vaccine).Include(r => r.Provider)
                .FirstOrDefaultAsync(r => r.RecordID == id);
            return entity is null ? null : _mapper.Map<VaccinationRecordDto>(entity);
        }
        public async Task<IEnumerable<VaccinationRecordDto>> GetUpcomingRemindersAsync(int days)
        {
            var targetDate = DateOnly.FromDateTime(DateTime.Today.AddDays(days));
            var upcoming = await _context.VaccinationRecords
                .Where(r => r.Status == "Pending" && r.DateGiven <= targetDate)
                .Include(r => r.Child)
                .Include(r => r.Vaccine)
                .Include(r => r.Provider)
                .OrderBy(r => r.DateGiven)
                .ToListAsync();
            return _mapper.Map<IEnumerable<VaccinationRecordDto>>(upcoming);
        }

        public async Task<IEnumerable<VaccinationRecordDto>> GetOverdueRecordsAsync()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var overdue = await _context.VaccinationRecords
                .Where(r => r.Status == "Pending" && r.DateGiven < today)
                .Include(r => r.Child)
                .Include(r => r.Vaccine)
                .Include(r => r.Provider)
                .OrderBy(r => r.DateGiven)
                .ToListAsync();
            return _mapper.Map<IEnumerable<VaccinationRecordDto>>(overdue);
        }
        public async Task<IEnumerable<VaccinationRecordDto>> GetByChildAsync(int childId)
        {
            var records = await _context.VaccinationRecords
                .Where(r => r.ChildID == childId)
                .Include(r => r.Vaccine)
                .Include(r => r.Provider)
                .ToListAsync();
            return _mapper.Map<IEnumerable<VaccinationRecordDto>>(records);
        }

        public async Task CreateAsync(VaccinationRecordDto dto)
        {
            _context.VaccinationRecords.Add(_mapper.Map<VaccinationRecord>(dto));
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(VaccinationRecordDto dto)
        {
            _context.VaccinationRecords.Update(_mapper.Map<VaccinationRecord>(dto));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.VaccinationRecords.FindAsync(id);
            if (entity is not null)
            {
                _context.VaccinationRecords.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task MarkAsCompletedAsync(int recordId, DateTime administeredDate, string administeredBy, string? batchNumber)
        {
            var record = await _context.VaccinationRecords.FindAsync(recordId);
            if (record != null)
            {
                record.Status = "Completed";
                record.DateGiven = DateOnly.FromDateTime(administeredDate);
                record.GivenBy = administeredBy;
                record.BatchNumber = batchNumber;
                await _context.SaveChangesAsync();
            }
        }
    }

    // ─── Appointment ─────────────────────────────────────────
    public class AppointmentService : IAppointmentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AppointmentService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppointmentDto>> GetAllAsync()
        {
            var list = await _context.Appointments
                .Include(a => a.Child).Include(a => a.Vaccine).Include(a => a.Provider)
                .ToListAsync();
            return _mapper.Map<IEnumerable<AppointmentDto>>(list);
        }

        public async Task<AppointmentDto?> GetByIdAsync(int id)
        {
            var entity = await _context.Appointments
                .Include(a => a.Child).Include(a => a.Vaccine).Include(a => a.Provider)
                .FirstOrDefaultAsync(a => a.AppointmentID == id);
            return entity is null ? null : _mapper.Map<AppointmentDto>(entity);
        }

        public async Task<IEnumerable<AppointmentDto>> GetByChildAsync(int childId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.ChildID == childId)
                .Include(a => a.Vaccine)
                .Include(a => a.Provider)
                .ToListAsync();
            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }

        public async Task CreateAsync(AppointmentDto dto)
        {
            _context.Appointments.Add(_mapper.Map<Appointment>(dto));
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AppointmentDto dto)
        {
            _context.Appointments.Update(_mapper.Map<Appointment>(dto));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Appointments.FindAsync(id);
            if (entity is not null)
            {
                _context.Appointments.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<AppointmentDto>> GetUpcomingAsync(int days)
        {
            var targetDate = DateOnly.FromDateTime(DateTime.Today.AddDays(days));
            var appointments = await _context.Appointments
                .Where(a => a.Date <= targetDate && a.Status != "Cancelled")
                .Include(a => a.Child)
                .Include(a => a.Vaccine)
                .Include(a => a.Provider)
                .OrderBy(a => a.Date)
                .ToListAsync();
            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }
        public async Task BookAsync(AppointmentDto dto)
        {
              await CreateAsync(dto);
        }

        public async Task CancelAppointmentAsync(int appointmentId)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment != null)
            {
                appointment.Status = "Cancelled";
                await _context.SaveChangesAsync();
            }
        }

        public async Task ConfirmAppointmentAsync(int appointmentId)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment != null)
            {
                appointment.Status = "Confirmed";
                await _context.SaveChangesAsync();
            }
        }
    }

    // ─── AdverseReaction ─────────────────────────────────────
    public class AdverseReactionService : IAdverseReactionService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AdverseReactionService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AdverseReactionDto>> GetByChildAsync(int childId)
        {
            var reactions = await _context.AdverseReactions
                .Where(r => r.ChildID == childId)
                .Include(r => r.Vaccine)
                .ToListAsync();
            return _mapper.Map<IEnumerable<AdverseReactionDto>>(reactions);
        }
        public async Task CreateAsync(AdverseReactionDto dto)
        {
           
            var childExists = await _context.Children.AnyAsync(c => c.ChildID == dto.ChildID);
            if (!childExists)
                throw new InvalidOperationException("Cannot report reaction: Child does not exist.");

            var entity = _mapper.Map<AdverseReaction>(dto);
            _context.AdverseReactions.Add(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<AdverseReactionDto>> GetAllAsync()
        {
            var list = await _context.AdverseReactions
                .Include(r => r.Child).Include(r => r.Vaccine)
                .ToListAsync();
            return _mapper.Map<IEnumerable<AdverseReactionDto>>(list);
        }

        public async Task<AdverseReactionDto?> GetByIdAsync(int id)
        {
            var entity = await _context.AdverseReactions
                .Include(r => r.Child).Include(r => r.Vaccine)
                .FirstOrDefaultAsync(r => r.ReactionID == id);
            return entity is null ? null : _mapper.Map<AdverseReactionDto>(entity);
        }

        public Task UpdateAsync(AdverseReactionDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }

    // ─── HealthcareProvider ──────────────────────────────────
    public class HealthcareProviderService : IHealthcareProviderService
    {
        private readonly IGenericRepository<HealthcareProvider> _repo;
        private readonly IMapper _mapper;

        public HealthcareProviderService(IGenericRepository<HealthcareProvider> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HealthcareProviderDto>> GetAllAsync()
            => _mapper.Map<IEnumerable<HealthcareProviderDto>>(await _repo.GetAllAsync());

        public async Task<HealthcareProviderDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity is null ? null : _mapper.Map<HealthcareProviderDto>(entity);
        }

        public async Task AddAsync(HealthcareProviderDto dto)
        {
            await _repo.AddAsync(_mapper.Map<HealthcareProvider>(dto));
            await _repo.SaveAsync();
        }

        public async Task UpdateAsync(HealthcareProviderDto dto)
        {
            _repo.Update(_mapper.Map<HealthcareProvider>(dto));
            await _repo.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is not null) { _repo.Delete(entity); await _repo.SaveAsync(); }
        }
    }

    // ─── Notification ────────────────────────────────────────
    public class NotificationService : INotificationService
    {
        private readonly IGenericRepository<Notification> _repo;
        private readonly IMapper _mapper;

        public NotificationService(IGenericRepository<Notification> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NotificationDto>> GetAllAsync()
            => _mapper.Map<IEnumerable<NotificationDto>>(await _repo.GetAllAsync());

        public async Task<NotificationDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity is null ? null : _mapper.Map<NotificationDto>(entity);
        }

        public async Task<IEnumerable<NotificationDto>> GetByUserAsync(string userId)
        {
            var notifications = await _repo.FindAsync(n => n.UserID == userId);
            return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
        }

        public async Task CreateAsync(NotificationDto dto)
        {
            await _repo.AddAsync(_mapper.Map<Notification>(dto));
            await _repo.SaveAsync();
        }

        public async Task UpdateAsync(NotificationDto dto)
        {
            _repo.Update(_mapper.Map<Notification>(dto));
            await _repo.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is not null)
            {
                _repo.Delete(entity);
                await _repo.SaveAsync();
            }
        }
    }

    // ─── Complaint ───────────────────────────────────────────
    public class ComplaintService : IComplaintService
    {
        private readonly IGenericRepository<Complaint> _repo;
        private readonly IMapper _mapper;

        public ComplaintService(IGenericRepository<Complaint> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ComplaintDto>> GetAllAsync()
            => _mapper.Map<IEnumerable<ComplaintDto>>(await _repo.GetAllAsync());

        public async Task<ComplaintDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity is null ? null : _mapper.Map<ComplaintDto>(entity);
        }

        public async Task<IEnumerable<ComplaintDto>> GetByUserAsync(string userId)
        {
            var complaints = await _repo.FindAsync(c => c.UserID == userId);
            return _mapper.Map<IEnumerable<ComplaintDto>>(complaints);
        }

        public async Task SubmitAsync(ComplaintDto dto)
        {
            await CreateAsync(dto);
        }

        public async Task CreateAsync(ComplaintDto dto)
        {
            await _repo.AddAsync(_mapper.Map<Complaint>(dto));
            await _repo.SaveAsync();
        }

        public async Task UpdateAsync(ComplaintDto dto)
        {
            _repo.Update(_mapper.Map<Complaint>(dto));
            await _repo.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is not null)
            {
                _repo.Delete(entity);
                await _repo.SaveAsync();
            }
        }
    }
    // ─── Awareness ───────────────────────────────────────────
    public class AwarenessService : IAwarenessService
    {
        private readonly IGenericRepository<Awareness> _repo;
        private readonly IMapper _mapper;

        public AwarenessService(IGenericRepository<Awareness> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AwarenessDto>> GetAllAsync()
            => _mapper.Map<IEnumerable<AwarenessDto>>(await _repo.GetAllAsync());

        public async Task<AwarenessDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity is null ? null : _mapper.Map<AwarenessDto>(entity);
        }

        public async Task<IEnumerable<AwarenessDto>> GetPublishedAsync()
        {
            var published = await _repo.FindAsync(a => a.Status == "Published");  
            return _mapper.Map<IEnumerable<AwarenessDto>>(published);
        }

        public async Task CreateAsync(AwarenessDto dto)
        {
            await _repo.AddAsync(_mapper.Map<Awareness>(dto));
            await _repo.SaveAsync();
        }

        public async Task UpdateAsync(AwarenessDto dto)
        {
            _repo.Update(_mapper.Map<Awareness>(dto));
            await _repo.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is not null)
            {
                _repo.Delete(entity);
                await _repo.SaveAsync();
            }
        }
    }
}
