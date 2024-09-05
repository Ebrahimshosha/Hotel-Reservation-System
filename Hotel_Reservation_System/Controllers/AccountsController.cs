using Hotel_Reservation_System.Consts;
using Hotel_Reservation_System.DTO.Authorization;
using Hotel_Reservation_System.Exceptions.Error;
using Hotel_Reservation_System.Services.Authorization;
using Hotel_Reservation_System.ViewModels.ResultViewModel;
using Hotel_Reservation_System.ViewModels.Room;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Reservation_System.Controllers;

public class AccountsController : BaseApiController
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IAuthService _authService;

    public AccountsController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 IAuthService authService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authService = authService;
    }

    [HttpPost("Register")]
    public async Task<ResultViewModel<AuthResponse>> Register(RegisterRequest model)
    {
        if (CheckEmailExists(model.Email).Result.Value)
            return ResultViewModel<AuthResponse>.Faliure(ErrorCode.BadRequest, "This email is already exists");

        var User = new ApplicationUser
        {
            Email = model.Email,
            UserName = model.Email.Split('@')[0],
            FirstName = model.FName,
            LastName = model.LName,
        };

        var Result = await _userManager.CreateAsync(User, model.Password);

        if (!Result.Succeeded)
          
            return ResultViewModel<AuthResponse>.Faliure(ErrorCode.BadRequest, "This email is already exists");
        await _userManager.AddToRoleAsync(User, DefaultRoles.Member);

        var authResult = await _authService.GetTokenAsync(model.Email, model.Password);
        if (authResult is null)
        {
            return ResultViewModel<AuthResponse>.Faliure(ErrorCode.TokenGenerationError, "There is a problem when generating the token");
        }

        return ResultViewModel<AuthResponse>.Sucess(authResult!);
    }



    [HttpPost("Login")]
    public async Task<ResultViewModel<AuthResponse>> Login(LoginRequest model)
    {
        var User = await _userManager.FindByEmailAsync(model.Email);
        if (User == null)
        {
            return ResultViewModel<AuthResponse>.Faliure(ErrorCode.UareNotAuthorized, "You are not Authorized, please register first");
        }

        var Result = await _signInManager.CheckPasswordSignInAsync(User, model.Password, false);
        if (!Result.Succeeded)
        {
            return ResultViewModel<AuthResponse>.Faliure(ErrorCode.UareNotAuthorized, "You are not Authorized, please register first");
        }

        var authResult = await _authService.GetTokenAsync(model.Email, model.Password);
        if (authResult is null)
        {
            return ResultViewModel<AuthResponse>.Faliure(ErrorCode.TokenGenerationError, "There is a problem when generating the token");
        }

        return ResultViewModel<AuthResponse>.Sucess(authResult!);
    }

    [HttpGet("CheckEmailExists")]
    public async Task<ActionResult<bool>> CheckEmailExists(string email)
    {
        return await _userManager.FindByEmailAsync(email) is not null;
    }

}
