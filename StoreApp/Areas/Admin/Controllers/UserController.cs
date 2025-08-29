using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Services.Contracts;

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IServiceManager _manager;

        public UserController(IServiceManager manager)
        {
            _manager = manager;
        }

        public ActionResult Index()
        {
            return View(_manager.AuthService.GetUsers());
        }
        public IActionResult Create()
        {
            return View(new UserDtoForInsertion()
            {
                Roles = new HashSet<string>
                (_manager.AuthService
                .GetRoles()
                .Select(x => x.Name)
                .ToList())
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] UserDtoForInsertion userDto)
        {
            var result = await _manager.AuthService.CreateUserAsync(userDto);
            return result.Succeeded
                ? RedirectToAction("Index")
                : View();
        }


        public async Task<IActionResult> Update([FromRoute(Name = "id")] string id)
        {
            var user = await _manager.AuthService.GetOneUserForUpdateAsync(id);
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] UserDtoForUpdate userDtoForUpdate)
        {
            if (ModelState.IsValid)
            {
                await _manager.AuthService.UpdateUserAsync(userDtoForUpdate);
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult ResetPassword([FromRoute(Name = "id")] string id)
        {
            return View(new ResetPasswordDto() { UserName = id });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordDto resetPasswordDto)
        {
            if (ModelState.IsValid)
            {
                await _manager.AuthService.ResetPasswordAsync(resetPasswordDto);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOneUser([FromForm] UserDto userDto)
        {
            await _manager.AuthService.DeleteAsync(userDto.UserName);
            return RedirectToAction("Index");
        }
    }
    
 }