using Gibson.Auth;
using Gibson.Common.Enums;
using Gibson.Common.Models;
using Gibson.Users;
using Microsoft.AspNetCore.Mvc;

namespace Gibson.Api.Controllers
{
    [Route("clients/{clientId}/employee/auth")]
    public class EmployeeAuthController : AuthController
    {
        public EmployeeAuthController(IAuthService authService, IUserRegistrationService userRegistrationService) 
            : base(authService, userRegistrationService, GibsonUserType.ClientEmployee)
        {
        }
    }
}