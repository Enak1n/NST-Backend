using HallOfFame.Domain.Entities;
using HallOfFame.Domain.Exceptions;
using HallOfFame.Domain.Interfaces.Repositories;
using HallOfFame.Service.Interfaces;

namespace HallOfFame.Service.Business
{
    public class SkillService : ISkillService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SkillService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Skill> Create(Skill skill)
        {
            var expectedRes = await _unitOfWork.Skills.FindAsync(s => s.Name == skill.Name);

            if (expectedRes != null)
                throw new UniqueException($"Skill with name {expectedRes.Name} already exist!");

            skill.Id = skill.NextId();
            await _unitOfWork.Skills.AddAsync(skill);
            await _unitOfWork.SaveChangesAsync();

            return skill;
        }

        public async Task DeleteById(long id)
        {
            var expectedRes = await _unitOfWork.Skills.GetByIdAsync(id);

            if (expectedRes == null)
                throw new NotFoundException($"Skill with id {id} not found!");
        }

        public async Task<List<Skill>> GetAll()
        {
            return await _unitOfWork.Skills.GetAllAsync();
        }

        public async Task<Skill> GetById(long id)
        {
            var expectedSkill = await _unitOfWork.Skills.GetByIdAsync(id);

            if (expectedSkill == null)
                throw new NotFoundException($"Skill with id {id} not found!");

            await _unitOfWork.Skills.RemoveAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return expectedSkill;
        }

        public async Task Update(long id, string name, string description, byte level)
        {
            await _unitOfWork.Skills.EditAsync(id, name, description, level);
        }
    }
}
