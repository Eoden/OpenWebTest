using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OpenWebData.Data.Contact;
using OpenWebData.Database;

namespace OpenWebData.Repository.v1
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {

        public ContactRepository(MyDbContext myDbContext) : base(myDbContext)
        {
        }

        public async Task<Contact> GetContactById(int id, CancellationToken cancellationToken)
        {
            return await _myDbContext.Contact.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<Contact> DeleteById(int id, CancellationToken cancellationToken)
        {
            var entityToDelete = await _myDbContext.Contact.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
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
