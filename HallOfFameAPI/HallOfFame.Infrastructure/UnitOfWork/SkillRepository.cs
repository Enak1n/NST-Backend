using HallOfFame.Domain.Entities;
using HallOfFame.Domain.Exceptions;
using HallOfFame.Domain.Interfaces.Repositories;
using HallOfFame.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace HallOfFame.Infrastructure.UnitOfWork
{
    public class SkillRepository : GenericRepository<Skill>, ISkillRepository
    {
        private readonly Context _context;

        public SkillRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task EditAsync(long id, string name, string description, byte level)
        {
            var skill = await _context.Persons.FindAsync(id);

            if (skill == null)
                throw new NotFoundException("Skill not found!");


            await _context.Skills.Where(p => p.Id == id).
                                    ExecuteUpdateAsync(p => p.SetProperty(p => p.Name, name)
                                                             .SetProperty(p => p.Level, level)
                                                             .SetProperty(p => p.Description, description));
        }
    }
}
