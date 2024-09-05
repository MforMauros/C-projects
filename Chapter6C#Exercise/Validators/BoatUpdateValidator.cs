using FluentValidation;

namespace Chapter6C_Exercise;

public class BoatUpdateValidator : AbstractValidator<BoatUpdateDTO>
{
    public BoatUpdateValidator()
    {
        RuleFor(c => c.Boatname)
        .NotEmpty()
        .WithMessage("The field boatname can't be empty")
        .Length(2, 20)
        .WithMessage("The field boatname must be between 2-20 characters");

        RuleFor(c => c.Price)
        .NotEmpty()
        .WithMessage("The field price can't be empty");

        RuleFor(c => c.Region)
        .NotEmpty()
        .WithMessage("The field region can't be empty")
        .Length(3, 20)
        .WithMessage("The field boatname must be between 3-20 characters");
    }
}
