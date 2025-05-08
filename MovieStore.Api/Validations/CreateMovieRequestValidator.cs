using FluentValidation;
using MovieStore.Api.Models.Requests;

namespace MovieStore.Api.Validations
{
    public class CreateMovieRequestValidator : AbstractValidator<CreateMovieRequest>
    {
        public CreateMovieRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Year).InclusiveBetween(1900, DateTime.Now.Year + 1);
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.GenreId).GreaterThan(0);
            RuleFor(x => x.DirectorId).GreaterThan(0);
        }
    }
}
