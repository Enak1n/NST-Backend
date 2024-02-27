using FluentValidation;
using HallOfFame.Domain.Entities;

namespace HallOfFame.Domain.Validators
{
    public class SkillValidator : AbstractValidator<Skill>
    {
        public SkillValidator()
        {
            RuleFor(skill => skill.Name).NotNull().NotEmpty();

            RuleFor(skill => skill.Level).GreaterThan((byte)0).LessThan((byte)11);
        }
    }
}
