using FluentValidation;
using MovieStore.Api.Models.Requests;

namespace MovieStore.Api.Validations
{
    public class CreateActorRequestValidator : AbstractValidator<CreateActorRequest>
    {
        public CreateActorRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        }
    }
}
