using Application.Authentication.Common;
using Application.Common.Interfaces.Authentication;
using Domain.Login;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Login
{
    public sealed class LoginCommandHandler : IRequestHandler<LoginComand, ErrorOr<LoginResult>>
    {
        private ILoginRepository _loginRepository;
        private IJwtTokenGenerator _jwtTokenGenerator;

        public LoginCommandHandler(ILoginRepository loginRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _loginRepository = loginRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<LoginResult>> Handle(LoginComand comand, CancellationToken cancellationToken)
        {
            bool loginResult = await _loginRepository.ValidateUser(comand.Username, comand.Password);

            if (!loginResult) {
                return Error.Unauthorized("Login.Failure","Invalid credentials");
            }

            var token = _jwtTokenGenerator.GenerateToken(comand.Username, comand.Username);

            return new LoginResult(token);
        }
    }
}
