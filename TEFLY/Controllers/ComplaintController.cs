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
    public class ComplaintController : Controller
    {
        private readonly IComplaintService _service;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public ComplaintController(IComplaintService service, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var data = await _service.GetByUserAsync(user!.Id);

            return View(_mapper.Map<IEnumerable<ComplaintViewModel>>(data));
        }

        public IActionResult Submit() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(ComplaintViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = await _userManager.GetUserAsync(User);

            var dto = _mapper.Map<ComplaintDto>(vm);
            dto.UserID = user!.Id;

            await _service.SubmitAsync(dto);

            TempData["Success"] = "Complaint submitted successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}
