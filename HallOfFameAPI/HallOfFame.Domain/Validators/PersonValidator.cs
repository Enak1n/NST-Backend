using FluentValidation;
using HallOfFame.Domain.Entities;

namespace HallOfFame.Domain.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator() 
        {
            RuleFor(person => person.Name).NotEmpty().NotNull();

            RuleFor(person => person.DisplayName).MaximumLength(15);

            RuleFor(person => person.Skills).NotEmpty().NotNull();

            RuleForEach(person => person.Skills)
                                  .SetValidator(new SkillValidator());
        }
    }
}
