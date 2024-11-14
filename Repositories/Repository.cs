using GoSakaryaApp.Data.Context;
using GoSakaryaApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GoSakaryaApp.Data.Repositories
{
    // Veritabanı işlemlerini gerçekleştiren generic repository sınıfı.
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity  // TEntity tipinin BaseEntity'den türetilmiş olması gerektiğini belirtir.
    {
        // DbContext ve DbSet nesneleri.
        private readonly GoSakaryaAppDbContext _db;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(GoSakaryaAppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            entity.CreatedDate = DateTime.UtcNow;

            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreatedDate = DateTime.UtcNow;
            }
            _dbSet.AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreatedDate = DateTime.UtcNow;
            }
            await _dbSet.AddRangeAsync(entities);
        }

        public void Delete(TEntity entity, bool softDelete=true)
        {
            if(softDelete)
            {
                entity.ModifiedDate = DateTime.UtcNow;

                entity.IsDeleted = true;

                _dbSet.Update(entity);
            }
            else
                _dbSet.Remove(entity);
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);

            Delete(entity);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate is null ? _dbSet : _dbSet.Where(predicate);
        }

        public TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Update(TEntity entity)
        {
            entity.ModifiedDate= DateTime.UtcNow;

            _dbSet.Update(entity);
        }
    }
}
