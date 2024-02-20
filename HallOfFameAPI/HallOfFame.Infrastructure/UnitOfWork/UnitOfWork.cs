using HallOfFame.Domain.Interfaces.Repositories;
using HallOfFame.Infrastructure.DataBase;

namespace HallOfFame.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly Context _context;

        public ISkillRepository Skills { get; private set; }
        public IPersonRepository Persons { get; private set; }

        public UnitOfWork(Context context)
        {
            _context = context;
            Skills = new SkillRepository(context);
            Persons = new PersonRepository(context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
