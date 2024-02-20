using HallOfFame.Domain.Entities;

namespace HallOfFame.Domain.Interfaces.Repositories
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        Task EditAsync(long id, string name, string displayName, string description, ICollection<Skill> skills);
    }
}
