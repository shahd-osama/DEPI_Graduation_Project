using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TEFLY.BLL.Services.Interfaces;
using TEFLY.DAL.Models;
using TEFLY.ViewModels;

namespace TEFLY.Controllers
{
    [Authorize]
    public class HealthcareProviderController : Controller
    {
        private readonly IHealthcareProviderService _service;
        private readonly IMapper _mapper;

        public HealthcareProviderController(IHealthcareProviderService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();

            return View(_mapper.Map<IEnumerable<HealthcareProviderViewModel>>(data));
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _service.GetByIdAsync(id);

            if (item == null) return NotFound();

            return View(_mapper.Map<HealthcareProviderViewModel>(item));
        }
    }
}
