using FluentValidation;
using FluentValidation.Results;
using HallOfFame.Domain.Entities;
using HallOfFame.Domain.Exceptions;
using HallOfFame.Domain.Interfaces.Repositories;
using HallOfFame.Domain.Validators;

public class PersonValidator : AbstractValidator<Person>
{
    private readonly IUnitOfWork _unitOfWork;

    public PersonValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(person => person.Name).NotEmpty().NotNull();

        RuleFor(person => person.DisplayName)
            .MaximumLength(15)
            .MustAsync(BeUniqueDisplayName)
            .When(person => !string.IsNullOrEmpty(person.DisplayName) && person.DisplayName.Trim() != ""); ;

        RuleFor(person => person.Skills).NotNull();

        RuleForEach(person => person.Skills)
            .SetValidator(new SkillValidator());

    }

    private async Task<bool> BeUniqueDisplayName(string displayName, CancellationToken cancellationToken)
    {
        var existingPerson = await _unitOfWork.Persons.FindAsync(p => p.DisplayName == displayName);

        if (existingPerson != null)
            throw new UniqueException($"User with {displayName} already exist!");

        return existingPerson == null;
    }
}
