using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TEFLY.BLL.DTOs;
using TEFLY.BLL.Services.Interfaces;
using TEFLY.ViewModels;

namespace TEFLY.Controllers
{
    public class VaccineController : Controller
    {
        private readonly IVaccineService _vaccineService;
        private readonly IMapper _mapper;

        public VaccineController(IVaccineService vaccineService, IMapper mapper)
        {
            _vaccineService = vaccineService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var vaccines = await _vaccineService.GetAllAsync();
            return View(_mapper.Map<IEnumerable<VaccineViewModel>>(vaccines));
        }

        public IActionResult Create() => View(new VaccineViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VaccineViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            await _vaccineService.AddAsync(_mapper.Map<VaccineDto>(vm));
            TempData["Success"] = "Vaccine added successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var vaccine = await _vaccineService.GetByIdAsync(id);
            if (vaccine is null) return NotFound();
            return View(_mapper.Map<VaccineViewModel>(vaccine));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VaccineViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            await _vaccineService.UpdateAsync(_mapper.Map<VaccineDto>(vm));
            TempData["Success"] = "Vaccine updated.";
            return RedirectToAction(nameof(Index));
        }
    }
}

