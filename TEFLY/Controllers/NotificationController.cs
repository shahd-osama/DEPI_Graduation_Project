using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TEFLY.BLL.Services.Interfaces;
using TEFLY.DAL.Models;
using TEFLY.ViewModels;

namespace TEFLY.Controllers
{
    [Authorize]
    public class NotificationController : Controller
    {
        private readonly INotificationService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public NotificationController(INotificationService service, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _service = service;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var data = await _service.GetByUserAsync(user!.Id);

            return View(_mapper.Map<IEnumerable<NotificationViewModel>>(data));
        }
    }
}
