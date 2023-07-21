﻿using AppTemplate.Data.MongoDB.Interfaces;
using AppTemplate.Shared.Interfaces;
using MongoDB.Driver;

namespace AppTemplate.Data.MongoDB.Repositories
{
    public class MongoRepository<T> : IRepository<T> where T : IEntity, new()
    {
        protected readonly IMongoCollection<T> _collection;
        public MongoRepository(IMongoConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString;
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase(configuration.DatabaseName);
            _collection = database.GetCollection<T>(typeof(T).Name);
        }
        public async Task Add(T item)
        {
            if (item.Id != Guid.Empty)
                item.Id = Guid.NewGuid();
            await _collection.InsertOneAsync(item);
        }

        public async Task DeleteById(Guid id)
        {
            _ = await _collection.FindOneAndDeleteAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var result = await _collection.FindAsync(_ => true);
            return result.ToList();
        }

        public async Task<T> GetById(Guid id)
        {
            var result = await _collection.FindAsync(i => i.Id == id);
            return result.FirstOrDefault();
        }

        public async Task Update(T entity)
        {
            await DeleteById(entity.Id);
            await Add(entity);
        }
    }

}
