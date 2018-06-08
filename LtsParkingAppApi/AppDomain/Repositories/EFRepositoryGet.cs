//---------------------------------------------------------------------------------------
// Description: entity framework repository for all database operations
//---------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query.Internal;
using AppDomain.Models.Interfaces;
using AppDomain.Contexts;
using AppDomain.Interfaces;

namespace AppDomain.Repositories
{
    public class EFRepositoryGet<TContext> : IRepositoryGet
    where TContext : DbContext, IContext
    {

        protected readonly TContext context;

        public EFRepositoryGet(TContext context)
        {

            this.context = context;

        }

        public DbContext Context { get => this.context; }

        public virtual IQueryable<TEntity> GetQueryable<TEntity>(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        int? skip = null,
        int? take = null,
        params Expression<Func<TEntity, object>>[] includes)
        where TEntity : class, IEntity
        {
            try
            {
                IQueryable<TEntity> query = context.Set<TEntity>();
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (includes != null)
                    foreach (var include in includes)
                    {

                        if (include.Body is MemberExpression)
                            query = query.Include(include);
                        else if (include.Body is MethodCallExpression)
                        {
                            var sds = ((MethodCallExpression)include.Body).Arguments;

                            var sdsa = sds.First();
                            var sdsb = sds.Last();


                        }
                    }

                if (orderBy != null)
                {
                    query = orderBy(query);
                }

                if (skip.HasValue)
                {
                    query = query.Skip(skip.Value);
                }

                if (take.HasValue)
                {
                    query = query.Take(take.Value);
                }


                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public virtual IEnumerable<TEntity> GetAll<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includes
            )
            where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(null, orderBy, skip, take, includes).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null)
            where TEntity : class, IEntity
        {
            return await GetQueryable<TEntity>(null, orderBy, skip, take).ToListAsync();
        }

        public virtual IEnumerable<TEntity> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter, orderBy, skip, take, includes).ToList();
        }



        public virtual async Task<IEnumerable<TEntity>> GetAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IEntity
        {
            return await GetQueryable<TEntity>(filter, orderBy, skip, take, includes).ToListAsync();
        }

        public virtual TEntity GetOne<TEntity>(
            Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter, null).FirstOrDefault();
        }

        public virtual async Task<TEntity> GetOneAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IEntity
        {
            return await GetQueryable<TEntity>(filter, null, null, null, includes).FirstOrDefaultAsync();
        }

        public virtual TEntity GetFirst<TEntity>(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
           where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter, orderBy).FirstOrDefault();
        }

        public virtual async Task<TEntity> GetFirstAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
            where TEntity : class, IEntity
        {
            return await GetQueryable<TEntity>(filter, orderBy).FirstOrDefaultAsync();
        }

        public virtual TEntity GetById<TEntity>(object id)
            where TEntity : class, IEntity
        {
            //return context.Set<TEntity>().FirstOrDefault(m => m.Id == id);
            return context.Find<TEntity>(id);
        }

        public virtual Task<TEntity> GetByIdAsync<TEntity>(object id)
            where TEntity : class, IEntity
        {
            //return context.Set<TEntity>().FirstOrDefaultAsync(m => m.Id == id);
            return context.FindAsync<TEntity>(id);
        }

        public virtual int GetCount<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter).Count();
        }

        public virtual Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter).CountAsync();
        }

        public virtual bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter).Any();
        }

        public virtual Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter).AnyAsync();
        }

        public IEnumerable<T> FromStoredProc<T>() where T : class
        {

            return new List<T>();

        }

        public async Task<T> SPScalarAsync<T>(string storedProc, params SqlParameter[] parameters)
        {
            T result = default(T);

            using (DbConnection connection = new SqlConnection(this.context.ConnectionString))
            using (DbCommand command = connection.CreateCommand())
            {

                command.CommandText = storedProc;
                command.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                var dbResult = await command.ExecuteScalarAsync();

                result = (dbResult ?? DBNull.Value) == DBNull.Value ? default(T) : (T)dbResult;

                connection.Close();

            }

            return result;
        }

        public T SPScalar<T>(string storedProc, params SqlParameter[] parameters)
        {

            return SPScalarAsync<T>(storedProc, parameters).Result;

        }

        public async Task<IEnumerable<T>> SPAsync<T>(string storedProc, params SqlParameter[] parameters) where T : class
        {

            var propertyNames = typeof(T).GetProperties();

            List<T> results = new List<T>();

            using (DbConnection connection = new SqlConnection(this.context.ConnectionString))
            using (DbCommand command = connection.CreateCommand())
            {

                command.CommandText = storedProc;
                command.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                DbDataReader reader = await command.ExecuteReaderAsync();

                IEnumerable<string> dbColumnNames;

                if (reader.CanGetColumnSchema())
                    dbColumnNames = reader.GetColumnSchema().Select(x => x.ColumnName);
                else
                    dbColumnNames = propertyNames.Select(x => x.Name);

                while (reader.Read())
                {

                    T template = Activator.CreateInstance<T>();

                    propertyNames.Where(x =>
                            x.CanWrite &&
                            dbColumnNames.Contains(x.Name)
                        ).ToList(
                        ).ForEach(x =>
                        {
                            try
                            {
                                x.SetValue(
                                    template,
                                    x.PropertyType.GetTypeInfo().IsEnum
                                        ? typeof(Enum).GetMethod("Parse", new Type[] { typeof(Type), typeof(string) }).Invoke(null, new object[] { x.PropertyType, reader[x.Name] == DBNull.Value ? null : reader[x.Name].ToString() })
                                        : reader[x.Name] == DBNull.Value ? null : reader[x.Name]
                                );
                            }
                            catch { }
                        }
                        );

                    results.Add(template);

                }

                connection.Close();

            }

            return results;

        }

        public IEnumerable<T> SP<T>(string storedProc, params SqlParameter[] parameters) where T : class
        {

            return SPAsync<T>(storedProc, parameters).Result;

        }

        public IEnumerable<T> FromText<T>(string sql, params SqlParameter[] parameters) where T : class
        {

            return FromTextAsync<T>(sql, parameters).Result;

        }


        public async Task<IEnumerable<T>> FromTextAsync<T>(string sql, params SqlParameter[] parameters) where T : class
        {

            var propertyNames = typeof(T).GetProperties();

            List<T> results = new List<T>();

            using (DbConnection connection = new SqlConnection(this.context.ConnectionString))
            using (DbCommand command = connection.CreateCommand())
            {

                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                DbDataReader reader = await command.ExecuteReaderAsync();

                IEnumerable<string> dbColumnNames;

                if (reader.CanGetColumnSchema())
                    dbColumnNames = reader.GetColumnSchema().Select(x => x.ColumnName);
                else
                    dbColumnNames = propertyNames.Select(x => x.Name);

                while (reader.Read())
                {

                    T template = Activator.CreateInstance<T>();

                    propertyNames.Where(x =>
                            x.CanWrite &&
                            dbColumnNames.Contains(x.Name)
                        ).ToList(
                        ).ForEach(x =>
                        {
                            try { x.SetValue(template, reader[x.Name] == DBNull.Value ? null : reader[x.Name]); }
                            catch (Exception) { }
                        }
                        );

                    results.Add(template);

                }

                connection.Close();

            }

            return results;
        }

        public IQueryable<TEntity> GetEntityViaStoreProc<TEntity>(string storeProcName, Dictionary<string, object> parameters = null)
            where TEntity : class, IEntity
        {
            string sqlQuery = storeProcName;

            if (parameters == null)
                parameters = new Dictionary<string, object>();

            object[] paramValues = new object[parameters.Count];

            int i = 1;
            foreach (var item in parameters)
            {
                if (i != parameters.Count)
                    sqlQuery += " @p" + (i - 1).ToString() + ",";
                else
                    sqlQuery += "  @p" + (i - 1).ToString();

                paramValues[i - 1] = item.Value;

                i++;

            }

            Type contextType = context.GetType();

            DbSet<TEntity> entities = context.Set<TEntity>();

            var results = entities.FromSql(sqlQuery, paramValues);

            return results;
        }

        public void ExecWithStoreProcedure(string storeProcNameWithParamNames, params object[] parameters) //, params object[] parameters)
                                                                                                           //public IQueryable<TEntity> ExecWithStoreProcedure(string query,params object[] parameters) : where TEntity : class, IEntity
        {
            try
            {
                context.Database.ExecuteSqlCommand(storeProcNameWithParamNames, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<int> ExecuteStoredProcWithSQLParameters(string storedProcName, IList<SqlParameter> SqlParams)
        {
            int executeNonQueryReturn;
            var connection = context.Database.GetDbConnection();
            var currentConnectionState = connection.State;
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storedProcName;
                foreach (var par in SqlParams)
                {
                    command.Parameters.Add(par);
                }
                executeNonQueryReturn = command.ExecuteNonQuery();
            }
            if (currentConnectionState == ConnectionState.Closed)
                connection.Close();

            return Task.FromResult(executeNonQueryReturn);
        }

        public IQueryable GetQueryableFromType(Type classType)
        {

            return (IQueryable)context.GetType().GetMethod("Set").MakeGenericMethod(classType).Invoke(context, null);

        }

        public IQueryable<T> GetQueryableFromExpression<T>(Expression expression)
        {

            return this.GetService<IAsyncQueryProvider>().CreateQuery<T>(expression);

        }

        public IQueryable GetQueryableFromExpression(Expression expression)
        {

            return this.GetService<IAsyncQueryProvider>().CreateQuery(expression);

        }

        public Microsoft.EntityFrameworkCore.Metadata.IEntityType FindEntityType(string name)
        {

            return this.context.Model.FindEntityType(name);

        }

        public Microsoft.EntityFrameworkCore.Metadata.IEntityType FindEntityType(Type type)
        {

            return this.context.Model.FindEntityType(type);

        }

        public T GetService<T>()
        {

            return this.context.GetService<T>();

        }

        public string GetTableName(Type type)
        {

            return this.FindEntityType(type).SqlServer().TableName;

        }

        public string GetProperyName(Type type, string name)
        {

            return this.FindEntityType(type).FindProperty(name).SqlServer().ColumnName;

        }

    }

}
