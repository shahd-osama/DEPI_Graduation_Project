using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TEFLY.BLL.DTOs;
using TEFLY.BLL.Services.Interfaces;
using TEFLY.DAL.Models;
using TEFLY.ViewModels;

namespace TEFLY.Controllerss
{
    [Authorize]
    public class ChildController : Controller
    {
        private readonly IChildService _childService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public ChildController(IChildService childService, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _childService = childService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var children = await _childService.GetByParentAsync(user!.Id);
            return View(_mapper.Map<IEnumerable<ChildViewModel>>(children));
        }

        public IActionResult Create() => View(new ChildViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChildViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = await _userManager.GetUserAsync(User);
            var dto = _mapper.Map<ChildDto>(vm);
            dto.UserID = user!.Id;

            await _childService.CreateAsync(dto);
            TempData["Success"] = "Child profile created successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var child = await _childService.GetByIdAsync(id);
            if (child is null) return NotFound();
            return View(_mapper.Map<ChildViewModel>(child));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var child = await _childService.GetByIdAsync(id);
            if (child is null) return NotFound();
            return View(_mapper.Map<ChildViewModel>(child));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ChildViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            
            if (vm.ChildID == 0)
                return BadRequest();

            await _childService.UpdateAsync(_mapper.Map<ChildDto>(vm));
            TempData["Success"] = "Child profile updated.";
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _childService.DeleteAsync(id);
            TempData["Success"] = "Child profile deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}
