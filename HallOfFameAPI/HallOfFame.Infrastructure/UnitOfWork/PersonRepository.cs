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

        public async Task EditAsync(long id, string name, string displayName, ICollection<Skill> skills)
        {
            var product = await _context.Persons.FindAsync(id);

            if (product == null)
                throw new NotFoundException("Product not found!");


            await _context.Persons.Where(p => p.Id == id).
                                    ExecuteUpdateAsync(p => p.SetProperty(p => p.Name, name)
                                                             .SetProperty(p => p.DisplayName, displayName)
                                                             .SetProperty(p => p.Skills, skills));
        }
    }
}
