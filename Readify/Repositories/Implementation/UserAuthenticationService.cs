using Readify.Models.Authentication;
using Readify.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Readify.Repositories.Implementation
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserAuthenticationService(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager
        )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #region LoginAsync
        public async Task<Status> LoginAsync(Login login)
        {
            var status = new Status();
            var user = await _userManager.FindByNameAsync(login.strUsername);
            if (user == null)
            {
                status.intStatusCode = 0;
                status.strMessage = "Invalid Username.";
                return status;
            }

            if(!await _userManager.CheckPasswordAsync(user, login.strPassword))
            {
                status.intStatusCode = 0;
                status.strMessage = "Invalid Password.";
                return status;
            }

            var result = await _signInManager.PasswordSignInAsync(user, login.strPassword, false, true);
            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var auth = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                };

                foreach (var role in roles)
                {
                    auth.Add(new Claim(ClaimTypes.Role, role));
                }
                status.intStatusCode = 1;
                status.strMessage = "Logged in successfully.";
                return status;
            }
            else if(result.IsLockedOut)
            {
                status.intStatusCode = 1;
                status.strMessage = "User locked out.";
                return status;
            }
            else
            {
                status.intStatusCode = 1;
                status.strMessage = "Unknown error encountered.";
                return status;
            }
        }
        #endregion

        #region LogoutAsync
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        #endregion

        #region RegistrationAsync
        public async Task<Status> RegistrationAsync(Registration registration)
        {
            var status = new Status();
            var userExists = await _userManager.FindByNameAsync(registration.strUsername);
            if (userExists != null) 
            {
                status.intStatusCode = 0;
                status.strMessage = "User already exists.";
                return status;
            }

            ApplicationUser user = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = registration.strEmail,
                UserName = registration.strUsername,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registration.strPassword);
            if (!result.Succeeded)
            {
                status.intStatusCode = 0;
                status.strMessage = "User creation failed.";
                return status;
            }

            if (!await _roleManager.RoleExistsAsync(registration.strRole))
                await _roleManager.CreateAsync(new ApplicationRole(registration.strRole));

            if (await _roleManager.RoleExistsAsync(registration.strRole))
            {
                await _userManager.AddToRoleAsync(user, registration.strRole);
            }

            status.intStatusCode = 1;
            status.strMessage = "User has registered successfully.";
            return status;
        }
        #endregion
    }
}
