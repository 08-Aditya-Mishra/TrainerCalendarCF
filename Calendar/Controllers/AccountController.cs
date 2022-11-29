using Calendar.Models;
using Calendar.Models.Dto;
using Calendar.Models.Responses;
using Calendar.Services.TokenGenerators;
using Calendar.Services.TokenValidators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Calendar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        //private readonly IRefreshTokenRepository _refreshTokenRepository;
        //private readonly Authenticator _authenticator;
        private readonly ApplicationDbContext _Context;
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly RefreshTokenValidator _refreshTokenValidator;
        UserManager<ApplicationUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        SignInManager<ApplicationUser> _signInManager;
        public AccountController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, AccessTokenGenerator accessTokenGenerator,RefreshTokenGenerator refreshTokenGenerator, RefreshTokenValidator refreshTokenValidator)
        {
            //_refreshTokenRepository = refreshTokenRepository;
            _Context = context;
            _accessTokenGenerator = accessTokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _refreshTokenGenerator = refreshTokenGenerator;
           //_authenticator = authenticator;
            _refreshTokenValidator = refreshTokenValidator;
        }
        /*
        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterViewDto model)
        {
            var userExists = await _userManager.FindByNameAsync(model.userName);
            if (userExists != null)
                return StatusCode(409, $"User '{model.Name}' already exists.");

            var user = new ApplicationUser
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.userName,
                Name = model.Name,
                Role = model.role

            };
            var result = await _userManager.CreateAsync(user, model.password);
            if (!result.Succeeded)
                return StatusCode(409,"User creation failed! Please check user details and try again.");

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Trainer))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Trainer));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
                await _userManager.CreateAsync(user, model.password);
            }
            var t = new Trainer()
            {
                Id = user.Id
            };
            await _Context.trainers.AddAsync(t);
            _Context.SaveChanges();
            return Ok("User created successfully!");
        }
        */
        //Add User
        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> Register([FromBody] RegisterViewDto value)
        {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = value.userName,
                    Email = value.Email,
                    Name = value.Name,
                    Role = value.role
                    
                };
               
                var Trainer = await _Context.Users.AnyAsync(u => u.UserName == value.userName);
                var Trainer1 = await _Context.Users.AnyAsync(u => u.Email == value.Email);
                var Trainer2 = await _Context.Users.AnyAsync(u => u.Name == value.Name);

                var roleExist = await _Context.Users.AnyAsync(u => u.Role == "Admin");
                if (roleExist && (user.Role =="Admin"))
                {
                    return StatusCode(409, $"Role '{value.role}' already exists.");
                }
                if (Trainer && Trainer1 && Trainer2)
                {
                    return Conflict();
                }
                else if(Trainer)
                {
                    return StatusCode(409, $"UserName '{value.userName}' already exists.");
                }
                else if(Trainer1)
                {
                    return StatusCode(409, $"Email '{value.Email}' already exists.");
                }
                else
                {
                    var result = await _userManager.CreateAsync(user,value.password);
                    var outcome = await _userManager.AddToRoleAsync(user, value.role);
                    var t = new Trainer()
                    {
                        Id = user.Id
                    };
                    await _Context.trainers.AddAsync(t);
                    _Context.SaveChanges();
                }

            }
            return Ok();
        }
 
        //Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewDto value)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userName = await _Context.Users.AnyAsync(u => u.UserName == value.userName);
            if(userName == null)
            {
                return Unauthorized();
            }
            var user = await _userManager.FindByNameAsync(value.userName);
            var isCorrectPassword = await _userManager.CheckPasswordAsync(user, value.password);
            if(!isCorrectPassword)
            {
                return Unauthorized();
            }

            string accessToken = _accessTokenGenerator.GenerateToken(user);
            string refreshToken = _refreshTokenGenerator.GenerateToken();

           // await _refreshTokenRepository.Create(refreshTokenDto);

            return Ok(new AuthenticatedUserResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }
    
        /*
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshViewDto refreshRequest)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool isValidRefreshToken = _refreshTokenValidator.Validate(refreshRequest.RefreshToken);
            if(!isValidRefreshToken)
            {
                return BadRequest();
            }

            RefreshToken refreshTokenDto = await _refreshTokenRepository.GetByToken(refreshRequest.RefreshToken);
            if(refreshTokenDto == null)
            {
                return NotFound();
            }

            await _refreshTokenRepository.Delete(refreshTokenDto.Id);

            var user = await _userManager.FindByIdAsync(refreshTokenDto.UserId);
            if(user == null)
            {
                return NotFound();
            }

            AuthenticatedUserResponse response = await _authenticator.Authenticate(user);
            return Ok(response);
        }
        
    */

        //Delete User
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var users = await _Context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _Context.Users.Remove(users);
            await _Context.SaveChangesAsync();

            return Ok();
        }
    }
}
