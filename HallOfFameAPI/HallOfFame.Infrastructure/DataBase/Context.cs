using HallOfFame.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HallOfFame.Infrastructure.DataBase
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Skill> Skills { get; set; }
    }
}
