using HallOfFame.Domain.Entities;

namespace HallOfFame.Service.Interfaces
{
    public interface IPersonService
    {
        Task<List<Person>> GetAll();
        Task<Person> GetById(long id);
        Task<Person> Create(Person person);
        Task Update(long id, string name, string displayName, string description, ICollection<Skill> skills);
        Task DeleteById(long id);
    }
}
