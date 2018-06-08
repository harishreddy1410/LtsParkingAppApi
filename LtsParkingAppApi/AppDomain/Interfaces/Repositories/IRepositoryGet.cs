//---------------------------------------------------------------------------------------
// Description: entity framework repository interface
//---------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Threading.Tasks;
using AppDomain.Interfaces;

namespace AppDomain.Models.Interfaces
{
    public interface IRepositoryGet
    {

        Microsoft.EntityFrameworkCore.DbContext Context { get; }

        IQueryable<TEntity> GetQueryable<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IEntity;
        IEnumerable<TEntity> GetAll<TEntity>(
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          int? skip = null,
          int? take = null,
          params Expression<Func<TEntity, object>>[] includes)
          where TEntity : class, IEntity;

        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null)
            where TEntity : class, IEntity;

        IEnumerable<TEntity> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IEntity;

        Task<IEnumerable<TEntity>> GetAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IEntity;

        TEntity GetOne<TEntity>(
            Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity;

        Task<TEntity> GetOneAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IEntity;

        TEntity GetFirst<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
            where TEntity : class, IEntity;

        Task<TEntity> GetFirstAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
            where TEntity : class, IEntity;

        TEntity GetById<TEntity>(object id)
            where TEntity : class, IEntity;

        Task<TEntity> GetByIdAsync<TEntity>(object id)
            where TEntity : class, IEntity;

        int GetCount<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity;

        Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity;

        bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity;

        Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity;

        IQueryable<TEntity> GetEntityViaStoreProc<TEntity>(string storeProcName, Dictionary<string,object> parameters = null)
            where TEntity : class, IEntity;
        
        void ExecWithStoreProcedure(string storeProcNameWithParamNames, params object[] parameters);

        Task<IEnumerable<T>> SPAsync<T>(string storedProc, params SqlParameter[] parameters) where T : class;

        IEnumerable<T> SP<T>(string storedProc, params SqlParameter[] parameters) where T : class;
        
        T SPScalar<T>(string storedProc, params SqlParameter[] parameters);
        
        Task<int> ExecuteStoredProcWithSQLParameters(string storedProcName, IList<SqlParameter> SqlParams);

        IQueryable<T> GetQueryableFromExpression<T>(Expression expression);

        IQueryable GetQueryableFromType(Type classType);

        T GetService<T>();

        Microsoft.EntityFrameworkCore.Metadata.IEntityType FindEntityType(string name);
        
        Microsoft.EntityFrameworkCore.Metadata.IEntityType FindEntityType(Type type);
        IQueryable GetQueryableFromExpression(Expression expression);

        Task<IEnumerable<T>> FromTextAsync<T>(string sql, params SqlParameter[] parameters) where T : class;
        IEnumerable<T> FromText<T>(string sql, params SqlParameter[] parameters) where T : class;

        string GetTableName(Type type);
        
        string GetProperyName(Type type, string name);
        
    }
}
