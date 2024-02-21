using HallOfFame.Domain.Entities;
using HallOfFame.Domain.Exceptions;
using HallOfFame.Domain.Interfaces.Repositories;
using HallOfFame.Service.Interfaces;

namespace HallOfFame.Service.Business
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PersonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Person> Create(Person person)
        {
            var expectedRes = await _unitOfWork.Persons.FindAsync(p => p.DisplayName == person.DisplayName);

            if (expectedRes != null)
                throw new UniqueException($"User with display name {person.DisplayName} already exist!");

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

        public async Task<Person> GetById(long id)
        {
            var expectedPerson = _unitOfWork.Persons.Include(p => p.Skills).FirstOrDefault(x => x.Id == id);

            if (expectedPerson == null)
                throw new NotFoundException($"User with id {id} not found!");

            return expectedPerson;
        }

        public async Task Update(long id, string name, string displayName, string description, ICollection<Skill> skills)
        {
            await _unitOfWork.Persons.EditAsync(id, name, displayName, description, skills);
        }
    }
}
