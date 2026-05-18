using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TEFLY.BLL.DTOs;
using TEFLY.BLL.Services.Interfaces;
using TEFLY.DAL.Models;
using TEFLY.ViewModels;

namespace TEFLY.Controllers
{
    [Authorize]
    public class VaccinationRecordController : Controller
    {
        private readonly IVaccinationRecordService _recordService;
        private readonly IChildService _childService;
        private readonly IVaccineService _vaccineService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public VaccinationRecordController(
            IVaccinationRecordService recordService,
            IChildService childService,
            IVaccineService vaccineService,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _recordService = recordService;
            _childService = childService;
            _vaccineService = vaccineService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> ByChild(int childId)
        {
            var records = await _recordService.GetByChildAsync(childId);
            ViewBag.ChildId = childId;
            return View(_mapper.Map<IEnumerable<VaccinationRecordViewModel>>(records));
        }

        public async Task<IActionResult> Create(int childId)
        {
            var vaccines = await _vaccineService.GetAllAsync();
            ViewBag.Vaccines = new SelectList(vaccines, "Id", "Name");
            ViewBag.ChildId = childId;
            return View(new VaccinationRecordViewModel { ChildID = childId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VaccinationRecordViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            await _recordService.CreateAsync(_mapper.Map<VaccinationRecordDto>(vm));
            TempData["Success"] = "Vaccination record created.";
            return RedirectToAction(nameof(ByChild), new { childId = vm.ChildID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "HealthcareProvider,Admin")]
        public async Task<IActionResult> MarkCompleted(int id, int childId, DateTime administeredDate, string administeredBy, string? batchNumber)
        {
            await _recordService.MarkAsCompletedAsync(id, administeredDate, administeredBy, batchNumber);
            TempData["Success"] = "Vaccination marked as completed.";
            return RedirectToAction(nameof(ByChild), new { childId });
        }
    }
}
