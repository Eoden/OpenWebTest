using System.Threading;
using System.Threading.Tasks;
using OpenWebData.Data.Skill;

namespace OpenWebData.Repository.v1
{
    public interface ISkillRepository : IRepository<Skill>
    {
        Task<Skill> GetSkillById(int id, CancellationToken cancellationToken);

        Task<Skill> DeleteById(int id, CancellationToken cancellationToken);
    }
}
