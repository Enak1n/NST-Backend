using HallOfFame.Domain.Entities;

namespace HallOfFame.Service.Interfaces
{
    public interface ISkillService
    {
        Task<List<Skill>> GetAll();
        Task<Skill> GetById(long id);
        Task<Skill> Create(Skill skill);
        Task Update(long id, string name, string description, byte level);
        Task DeleteById(long id);
    }
}
