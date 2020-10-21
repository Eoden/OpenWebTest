using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OpenWebData.Database;

namespace OpenWebData.Repository.v1
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly MyDbContext _myDbContext;

        public Repository(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(Add)} entity is null");
            }

            try
            {
                _myDbContext.Add(entity);
                await _myDbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} can't be saved {ex.Message}");
            }
        }
   
        public async Task<List<TEntity>> GetAll()
        {
            return await _myDbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(Update)} entity must not be null");
            }

            try
            {
                _myDbContext.Update(entity);
                await _myDbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated {ex.Message}");
            }
        }


        /*public async Task setDatabaseWithEntitiesAsync()
        {
            var listContacts = new[]
            {
                new Contact { Id = 1, Firstname = "Arthur", Lastname = "Picaud",Fullname = "Arthur Picaud", Address = "1 avenue de champs elysés", Email = "arthur.picaud@gmail.com", MobilePhoneNumber = "+33 7 89 73 46 90"},
                new Contact { Id = 2, Firstname = "Charlotte", Lastname = "Lepoutre",Fullname = "Charlotte Lepoutre", Address = "1 avenue de champs elysés", Email = "charlotte.lepoutre@gmail.com", MobilePhoneNumber = "+33 7 49 73 36 90"},
                new Contact { Id = 3, Firstname = "Pierre", Lastname = "Lebeau", Fullname = "Pierre Lebeau", Address = "4 rue des merveilles", Email = "pierre.lebeau@gmail.com", MobilePhoneNumber = "+33 7 80 73 46 30"},
                new Contact { Id = 4 , Firstname = "Jonathan", Lastname = "Guelou",Fullname = "Jonathan Guelou", Address = "432 rue victor hugo" , Email = "jonathan.guelou@gmail.com", MobilePhoneNumber = "+33 6 89 72 46 91"},
           };
            if (await _myDbContext.Contact.CountAsync() == 0)
            {
                try
                {
                    _myDbContext.AddRange(listContacts);
                    _myDbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception($"{nameof(setDatabaseWithEntitiesAsync)} can't initialize data {ex.Message}");
                }
            }
        }*/

    }
}
