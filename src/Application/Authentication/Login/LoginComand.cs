using Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Login
{
    public record LoginComand(
        string Username,
        string Password
    ) : IRequest<ErrorOr<LoginResult>>;
}
