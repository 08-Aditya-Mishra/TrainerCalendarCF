using Calendar.Models;
using Calendar.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ApplicationDbContext _Context;
        RoleManager<IdentityRole> _roleManager;

        public RolesController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _Context = context;
            _roleManager = roleManager;
        }

        //Add Roles
        [HttpPost]
        [Route("RegisterRoles")]
        public async Task<IActionResult> AddRoles([FromBody] RoleViewDto value)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole
                {
                    Name = value.RoleName
                };

                var roleExist = await _Context.Roles.AnyAsync(u => u.Name == value.RoleName);
                if (roleExist)
                {
                    return Conflict();
                }
                else
                {
                    var result = await _roleManager.CreateAsync(role);
                }

            }
            return Ok();
        }
    }
}
