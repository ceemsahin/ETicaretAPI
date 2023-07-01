using ETicaretAPI.Application.Features.Commands.AppUser.LoginUser;
using ETicaretAPI.Application.Features.Commands.AppUser.LoginWithFacebook;
using ETicaretAPI.Application.Features.Commands.AppUser.LoginWithGoogle;
using ETicaretAPI.Application.Features.Commands.AppUser.PasswordReset;
using ETicaretAPI.Application.Features.Commands.AppUser.RefreshTokenLogin;
using ETicaretAPI.Application.Features.Commands.VerifyResetToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);

            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogin([FromBody] RefreshTokenLoginCommandRequest refreshTokenLoginCommandRequest)
        {
            RefreshTokenLoginCommandResponse response = await _mediator.Send(refreshTokenLoginCommandRequest);

            return Ok(response);
        }




        [HttpPost("google-login")]

        public async Task<IActionResult> LoginWithGoogle(LoginWithGoogleCommandRequest loginWithGoogleCommandRequest)
        {

            LoginWithGoogleCommandResponse response = await _mediator.Send(loginWithGoogleCommandRequest);

            return Ok(response);
        }


        [HttpPost("facebook-login")]

        public async Task<IActionResult> LoginWithFacebook(LoginWithFacebookCommandRequest loginWithFacebookCommandRequest)
        {

            LoginWithFacebookCommandResponse response = await _mediator.Send(loginWithFacebookCommandRequest);

            return Ok(response);
        }



        [HttpPost("password-reset")]

        public async Task<IActionResult> PasswordReset([FromBody] PasswordResetCommandRequest passwordResetCommandRequest)
        {
            PasswordResetCommandResponse response = await _mediator.Send(passwordResetCommandRequest);


            return Ok(response);
        }

        [HttpPost("verify-reset-token")]

        public async Task<IActionResult> VerifyResetToken([FromBody] VerifyResetTokenCommandRequest verifyResetTokenCommandRequest)
        {

            VerifyResetTokenCommandResponse response = await _mediator.Send(verifyResetTokenCommandRequest);


            return Ok(response);



        }





    }
}
