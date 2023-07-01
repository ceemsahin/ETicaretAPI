using ETicaretAPI.Application.Abstractions.Services;

using MediatR;

namespace ETicaretAPI.Application.Features.Commands.AppUser.LoginWithGoogle
{
    public class LoginWithGoogleCommandHandler : IRequestHandler<LoginWithGoogleCommandRequest, LoginWithGoogleCommandResponse>
    {

        readonly IAuthService _authService;
        public LoginWithGoogleCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginWithGoogleCommandResponse> Handle(LoginWithGoogleCommandRequest request, CancellationToken cancellationToken)
        {

            var token = await _authService.GoogleLoginAsync(request.IdToken, 20);
            return new()
            {
                Token = token
            };

        }
    }
}
