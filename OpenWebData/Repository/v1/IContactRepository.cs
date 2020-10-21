using System.Threading;
using System.Threading.Tasks;
using OpenWebData.Data.Contact;

namespace OpenWebData.Repository.v1
{
    public interface IContactRepository : IRepository<Contact>
    {
        Task<Contact> GetContactById(int id, CancellationToken cancellationToken);

        Task<Contact> DeleteById(int id, CancellationToken cancellationToken);
    }
}
