namespace HallOfFame.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        ISkillRepository Skills { get; }
        IPersonRepository Persons { get; }

        Task SaveChangesAsync();
    }
}
