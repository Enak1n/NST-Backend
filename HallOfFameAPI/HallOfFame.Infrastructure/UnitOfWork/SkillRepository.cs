using HallOfFame.Domain.Entities;
using HallOfFame.Domain.Interfaces.Repositories;
using HallOfFame.Infrastructure.DataBase;

namespace HallOfFame.Infrastructure.UnitOfWork
{
    public class SkillRepository : GenericRepository<Skill>, ISkillRepository
    {
        public SkillRepository(Context context) : base(context) { }
    }
}
