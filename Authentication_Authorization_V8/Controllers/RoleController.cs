using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Authentication_Authorization_V8.Controllers
{
    [Authorize(Roles ="Admin")]  //FOR ALL ACTIONS
    public class RoleController : Controller
    {

        RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }
        public IActionResult Create()
        {
            return View(new IdentityRole());
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            await roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> GetCustomers()
        {
            var users = await _userManager.Users.ToListAsync();
            var customers = new List<IdentityUser>();

            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, "Customer"))
                {
                    customers.Add(user);
                }
            }

            return View(customers); // Or return JSON if it's an API
        }
        [HttpPost]
        public async Task<IActionResult> PromoteToAdmin(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Ensure the roles exist
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            // Assign Admin role
            await _userManager.AddToRoleAsync(user, "Admin");

            // Optionally remove Customer role
            if (await _userManager.IsInRoleAsync(user, "Customer"))
            {
                await _userManager.RemoveFromRoleAsync(user, "Customer");
            }
            ViewBag.Message = "User promoted to Admin successfully!";
            return RedirectToAction("GetCustomers");
        }
    }
}
