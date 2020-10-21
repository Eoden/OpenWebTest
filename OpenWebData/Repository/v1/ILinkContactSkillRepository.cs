using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OpenWebData.Data.LinkContactSkill;

namespace OpenWebData.Repository.v1
{
    public interface ILinkContactSkillRepository : IRepository<LinkContactSkill>
    {
        Task<LinkContactSkill> DeleteById(int id, CancellationToken cancellationToken);

        Task<List<LinkContactSkill>> GetAllLinkByContactId(int id, CancellationToken cancellationToken);
    }
}
