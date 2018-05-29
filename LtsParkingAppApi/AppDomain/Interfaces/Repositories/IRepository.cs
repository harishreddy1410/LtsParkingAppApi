using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore.Storage;
using AppDomain.Interfaces;

namespace AppDomain.Models.Interfaces
{
    public interface IRepository : IRepositoryGet
    {
        
        void CreateNonEntity<T>(T entity) where T : class;

        TEntity Create<TEntity>(TEntity entity, string createdBy = null)
         where TEntity : class, IEntity;

        void Update<TEntity>(TEntity entity, string modifiedBy = null)
            where TEntity : class, IEntity;

        void Delete<TEntity>(object id)
            where TEntity : class, IEntity;

        void Delete<TEntity>(TEntity entity)
           where TEntity : class, IEntity;

        void Save();
        Task SaveAsync(CancellationToken token);
       
        IDbContextTransaction CreateTransaction();

        void RollbackTransaction(IDbContextTransaction transaction);
        
        void CommitTransaction(IDbContextTransaction transaction);
        
    }
}
