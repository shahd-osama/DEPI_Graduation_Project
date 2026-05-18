using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TEFLY.BLL.DTOs;
using TEFLY.BLL.Services.Interfaces;
using TEFLY.ViewModels;

namespace TEFLY.Controllers
{
    [Authorize]
    public class AdverseReactionController : Controller
    {
        private readonly IAdverseReactionService _service;
        private readonly IChildService _childService;
        private readonly IVaccineService _vaccineService;
        private readonly IMapper _mapper;

        public AdverseReactionController(
            IAdverseReactionService service,
            IChildService childService,
            IVaccineService vaccineService,
            IMapper mapper)
        {
            _service = service;
            _childService = childService;
            _vaccineService = vaccineService;
            _mapper = mapper;
        }

        // GET: /AdverseReaction/ByChild?childId=1
        public async Task<IActionResult> ByChild(int childId)
        {
            var data = await _service.GetByChildAsync(childId);
            ViewBag.ChildID = childId;

            return View(_mapper.Map<IEnumerable<AdverseReactionViewModel>>(data));
        }

        // GET: Create
        public async Task<IActionResult> Create(int childId)
        {
            var vaccines = await _vaccineService.GetAllAsync();

            ViewBag.Vaccines = new SelectList(vaccines, "VaccineID", "Name");
            ViewBag.ChildID = childId;

            return View(new AdverseReactionViewModel
            {
                ChildID = childId
            });
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdverseReactionViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var vaccines = await _vaccineService.GetAllAsync();
                ViewBag.Vaccines = new SelectList(vaccines, "VaccineID", "Name");

                return View(vm);
            }
         

            await _service.CreateAsync(_mapper.Map<AdverseReactionDto>(vm));

            TempData["Success"] = "Reaction reported successfully.";

            return RedirectToAction(nameof(ByChild), new { childId = vm.ChildID });
        }

        // DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int childId)
        {
            await _service.DeleteAsync(id);

            TempData["Success"] = "Reaction deleted.";

            return RedirectToAction(nameof(ByChild), new { childId });
        }
    }
}
