using CompanyRegistry.DataAccess.Config;
using CompanyRegistry.DataAccess.Entitites;
using CompanyRegistry.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;

namespace CompanyRegistry.Repository.Concrete
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Constructors

        private readonly IMongoContext Context;
        
        private IMongoCollection<T> DbSet;
        
        public Repository(IMongoContext context)
        {
            Context = context;

            DbSet = Context.GetCollection<T>(typeof(T).Name);
        }

        #endregion

        #region Methods

        public async Task<IEnumerable<T>> Get()
        {
            List<T> companies = await DbSet.Find(Builders<T>.Filter.Empty).ToListAsync();

            return companies;
        }

        public async Task<T> Get(string id)
        {
            T company = await DbSet.Find(Builders<T>.Filter.Eq("_id", new ObjectId(id))).FirstOrDefaultAsync();

            return company;
        }

        public async Task<T> Update(T entity)
        {
            await DbSet.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", new ObjectId(entity.GetId().ToString())), entity);
            
            return entity;
        }

        public async Task<T> Insert(T entity)
        {
            await DbSet.InsertOneAsync(entity);

            return entity;
        }

        public async Task<IEnumerable<T>> Search(string searchTerm)
        {
            FilterDefinition<T> filter = 
                Builders<T>.Filter.Regex("Name", new BsonRegularExpression(searchTerm, "i"));

            List<T> filteredCompanies = await DbSet.Find<T>(filter).ToListAsync();//(Builders<T>.Filter.Eq("Name", searchTerm)).ToListAsync();

            return filteredCompanies;
        }

        public virtual void Delete(string id)
        {
            Context.AddCommand(() => 
                DbSet.DeleteOneAsync(Builders<T>.Filter.Eq("_id", id)));
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            Context?.Dispose();
        }

        #endregion
    }
}
