using FluentValidation;
using MediatR;

namespace DroneManager.Core.Abstractions.Validators
{
    public abstract class BaseValidator<TQueryCommand> : AbstractValidator<TQueryCommand>
        where TQueryCommand : class, IBaseRequest
    {
        protected BaseValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
        }
    }
}
