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
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IChildService _childService;
        private readonly IVaccineService _vaccineService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public AppointmentController(
            IAppointmentService appointmentService,
            IChildService childService,
            IVaccineService vaccineService,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _appointmentService = appointmentService;
            _childService = childService;
            _vaccineService = vaccineService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> ByChild(int childId)
        {
            var appointments = await _appointmentService.GetByChildAsync(childId);
            ViewBag.ChildID = childId;
            return View(_mapper.Map<IEnumerable<AppointmentViewModel>>(appointments));
        }

        public async Task<IActionResult> Book(int childId)
        {
             
            var vaccines = await _vaccineService.GetAllAsync();
            ViewBag.Vaccines = new SelectList(vaccines, "VaccineID", "Name");
            ViewBag.ChildID = childId;
            return View(new AppointmentViewModel { ChildID = childId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book(AppointmentViewModel vm)
        {
            if (vm.ChildID <= 0)
            {
                ModelState.AddModelError("", "Invalid child.");
                return View(vm);
            }


            if (!ModelState.IsValid) return View(vm);
            await _appointmentService.BookAsync(_mapper.Map<AppointmentDto>(vm));
            TempData["Success"] = "Appointment booked successfully.";
            return RedirectToAction(nameof(ByChild), new { childId = vm.ChildID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id, int childId)
        {
            await _appointmentService.CancelAppointmentAsync(id);
            TempData["Success"] = "Appointment cancelled.";
            return RedirectToAction(nameof(ByChild), new { childId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "HealthcareProvider,Admin")]
        public async Task<IActionResult> Confirm(int id, int childId)
        {
            await _appointmentService.ConfirmAppointmentAsync(id);
            TempData["Success"] = "Appointment confirmed.";
            return RedirectToAction(nameof(ByChild), new { childId });
        }
    }

}
