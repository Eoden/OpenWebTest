using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OpenWebData.Data.LinkContactSkill;
using OpenWebData.Database;

namespace OpenWebData.Repository.v1
{
    public class LinkContactSkillRepository : Repository<LinkContactSkill>, ILinkContactSkillRepository
    {
        public LinkContactSkillRepository(MyDbContext myDbContext) : base(myDbContext)
        {
        }
        public async Task<LinkContactSkill> DeleteById(int id, CancellationToken cancellationToken)
        {
            var entityToDelete = await _myDbContext.LinkContactSkill.FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
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

        public async Task<List<LinkContactSkill>> GetAllLinkByContactId(int id, CancellationToken cancellationToken)
        {
            return await _myDbContext.LinkContactSkill.Where(c => c.IdContact == id).ToListAsync();
        }
    }
}
