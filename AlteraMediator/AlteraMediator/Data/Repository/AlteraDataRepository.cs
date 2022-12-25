using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AlteraMediator.Data.Repository
{
    internal class AlteraDataRepository<TEntity> : IAlteraDataRepository<TEntity> where TEntity : class
    {
        private readonly AlteraDbContext _dbContext;

        public AlteraDataRepository(AlteraDbContext dbContext)
        {
              _dbContext= dbContext;
        }

        // CRUD using CQRS
        public void Create(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void CreateRange(List<TEntity> entities)
        {
            _dbContext.Set<TEntity>().AddRange(entities);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void DeleteRange(List<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public void UpdateRange(List<TEntity> entities)
        {
            _dbContext.Set<TEntity>().UpdateRange(entities);
        }

        // GET Methods
        public IQueryable<TEntity> GetAll()
        {
            var entites = _dbContext.Set<TEntity>();

            return entites;

        }

        public IQueryable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> expression)
        {
            var entities = _dbContext.Set<TEntity>().Where(expression);

            return entities;
        }

        public IQueryable<TEntity> GetById(Expression<Func<TEntity, bool>> expression)
        {
            var entity = _dbContext.Set<TEntity>().Where(expression);

            return entity;
        }
    }
}
