using MediatR;

namespace ETicaretAPI.Application.Features.Commands.AppUser.LoginWithFacebook
{
    public class LoginWithFacebookCommandRequest : IRequest<LoginWithFacebookCommandResponse>
    {
        public string AuthToken { get; set; }

    }
}
