using ErrorOr;
using FluentValidation;
using MediatR;

namespace Application.Common.Behavior
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
    {
        private IValidator<TRequest>? _validator;

        public ValidationBehavior(IValidator<TRequest>? validator = null)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(_validator is null)
            {
                return await next();
            }

            var validatorResult = await _validator.ValidateAsync(request, cancellationToken);

            if (validatorResult.IsValid)
            {
                return await next();
            }

            var errors = validatorResult.Errors.ConvertAll(validatorFailure => Error.Validation(
                validatorFailure.PropertyName,
                validatorFailure.ErrorMessage
            ));

            return (dynamic)errors;
        }
    }
}
