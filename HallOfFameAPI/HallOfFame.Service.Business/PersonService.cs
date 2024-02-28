using FluentValidation;
using HallOfFame.Domain.Entities;
using HallOfFame.Domain.Exceptions;
using HallOfFame.Domain.Interfaces.Repositories;
using HallOfFame.Service.Interfaces;

namespace HallOfFame.Service.Business
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Person> _validator;

        public PersonService(IUnitOfWork unitOfWork, IValidator<Person> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Person> Create(Person person)
        {
            var expectedRes = await _unitOfWork.Persons.FindAsync(p => p.DisplayName == person.DisplayName);

            await _unitOfWork.Persons.AddAsync(person);
            await _unitOfWork.SaveChangesAsync();
            
            return person;
        }

        public async Task DeleteById(long id)
        {
            var expectedRes = await _unitOfWork.Persons.FindAsync(p => p.Id == id);

            if (expectedRes == null)
                throw new NotFoundException($"User with id {id} not found!");

            await _unitOfWork.Persons.RemoveAsync(id);
            await _unitOfWork.SaveChangesAsync();   
        }

        public async Task<List<Person>> GetAll()
        {
            return _unitOfWork.Persons.Include(p => p.Skills);
        }

        // Проверку на null не удастся вынести в валидатор так как если сущности действительно не существует то
        // валидатор будет выдавать exception что не может работать с null
        public async Task<Person> GetById(long id)
        {
            var expectedPerson = _unitOfWork.Persons.Include(p => p.Skills).FirstOrDefault(x => x.Id == id);

            if (expectedPerson == null)
                throw new NotFoundException($"User with id {id} not found!");

            return expectedPerson;
        }

        public async Task Update(long id, string name, string displayName, string description, List<Skill> skills)
        {
            var expectedPerson = await _unitOfWork.Persons.GetByIdAsync(id);

            if(!string.IsNullOrEmpty(displayName))
                await _unitOfWork.Persons.EditAsync(id, name, displayName, description, skills);
            else
                await _unitOfWork.Persons.EditAsync(id, name, expectedPerson.DisplayName, description, skills);

        }
    }
}
