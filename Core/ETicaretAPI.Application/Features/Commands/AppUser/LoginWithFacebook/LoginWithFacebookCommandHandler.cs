using ETicaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.AppUser.LoginWithFacebook
{
    public class LoginWithFacebookCommandHandler : IRequestHandler<LoginWithFacebookCommandRequest, LoginWithFacebookCommandResponse>
    {

        readonly IAuthService _authService;

        public LoginWithFacebookCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginWithFacebookCommandResponse> Handle(LoginWithFacebookCommandRequest request, CancellationToken cancellationToken)
        {

            var token = await _authService.FacebookLoginAsync(request.AuthToken, 20);


            return new()
            {
                Token = token
            };


        }


    }
}
