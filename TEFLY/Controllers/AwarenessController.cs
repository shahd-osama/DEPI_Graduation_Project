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
    public class AwarenessController : Controller
    {
        private readonly IAwarenessService _service;
        private readonly IMapper _mapper;

        public AwarenessController(IAwarenessService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetPublishedAsync();
            return View(_mapper.Map<IEnumerable<AwarenessViewModel>>(data));
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();

            return View(_mapper.Map<AwarenessViewModel>(item));
        }
    }
}
