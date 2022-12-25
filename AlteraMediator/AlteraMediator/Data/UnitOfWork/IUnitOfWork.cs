using AlteraMediator.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlteraMediator.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IAlteraDataRepository<TEntity> Repository<TEntity>() where TEntity : class;
        bool Complete();
    }
}
