using HallOfFame.Domain.Entities;
using HallOfFame.Domain.Exceptions;
using HallOfFame.Domain.Interfaces.Repositories;
using HallOfFame.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace HallOfFame.Infrastructure.UnitOfWork
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        private readonly Context _context;

        public PersonRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task EditAsync(long id, string name, string displayName, string description, ICollection<Skill> skills)
        {
            var person = await _context.Persons
                .Include(p => p.Skills)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person == null)
                throw new NotFoundException("Person not found!");

            await _context.Persons
                .Where(p => p.Id == id)
                .ExecuteUpdateAsync(p => p.SetProperty(x => x.Name, name)
                                          .SetProperty(x => x.DisplayName, displayName)
                                          .SetProperty(x => x.Description, description));

            foreach (var skill in skills)
            {
                var existingSkill = person.Skills.FirstOrDefault(s => s.Name == skill.Name);

                if (existingSkill != null)
                {
                    await _context.Skills
                        .Where(s => s.Persons.Any(p => p.Id == id) && s.Name == skill.Name)
                        .ExecuteUpdateAsync(s => s.SetProperty(x => x.Name, skill.Name)
                                                  .SetProperty(x => x.Description, skill.Description)
                                                  .SetProperty(x => x.Level, skill.Level));
                }
                else
                {
                    person.Skills.Add(skill);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
