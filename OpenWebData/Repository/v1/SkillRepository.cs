using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OpenWebData.Data.Skill;
using OpenWebData.Database;
namespace OpenWebData.Repository.v1
{
    public class SkillRepository : Repository<Skill>, ISkillRepository
    {

        public SkillRepository(MyDbContext myDbContext) : base(myDbContext)
        {
        }

        public async Task<Skill> GetSkillById(int id, CancellationToken cancellationToken)
        {
            return await _myDbContext.Skill.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<Skill> DeleteById(int id, CancellationToken cancellationToken)
        {
            var entityToDelete = await _myDbContext.Skill.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
            if (entityToDelete == null)
            {
                throw new ArgumentNullException($"{nameof(DeleteById)} entity do not exist");
            }

            try
            {
                _myDbContext.Remove(entityToDelete);
                await _myDbContext.SaveChangesAsync();

                return entityToDelete;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entityToDelete)} could not be deleted {ex.Message}");
            }
        }
    }
}
