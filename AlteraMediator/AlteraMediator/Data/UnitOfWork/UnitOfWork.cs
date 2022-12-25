using AlteraMediator.Data.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlteraMediator.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AlteraDbContext _dbContext;
        private Hashtable _repositories;

        public UnitOfWork(AlteraDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public IAlteraDataRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if(_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;
            if(!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(AlteraDataRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dbContext);

                _repositories.Add(type, repositoryInstance);
            }

            return (AlteraDataRepository<TEntity>)_repositories[type];
        }

        public bool Complete()
        {
            var numberOfAffecterRows = _dbContext.SaveChanges();
            return numberOfAffecterRows > 0;
        }
    }
}
