//---------------------------------------------------------------------------------------
// Description: entity framework repository for all database operations
//---------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Storage;
using AppDomain.Contexts;
using AppDomain.Models.Interfaces;
using AppDomain.Interfaces;

namespace AppDomain.Repositories
{
    public class EFRepository<TContext> : EFRepositoryGet<TContext>, IRepository
    where TContext : DbContext, IContext
    {
        public EFRepository(TContext context)
        : base(context)
        {
            this.context.Database.SetCommandTimeout(360);

        }
        //Don't want to call CommandTimeout when Unit Testing. As mock repository doesn't allow these operations.
        public EFRepository(TContext context, bool testing)
        : base(context)
        {

            //this.context.Database.SetCommandTimeout(360);

        }

        public virtual void CreateNonEntity<T>(T entity) where T : class
        {

            context.Set<T>().Add(entity);

        }

        public virtual IDbContextTransaction CreateTransaction()
        {

            return context.Database.BeginTransaction();

        }

        public virtual void RollbackTransaction(IDbContextTransaction transaction)
        {

            transaction.Dispose();

            transaction.Rollback();

        }

        public virtual void CommitTransaction(IDbContextTransaction transaction)
        {

            transaction.Commit();

            transaction.Dispose();

        }

        public virtual TEntity Create<TEntity>(TEntity entity, string createdBy = null)
            where TEntity : class, IEntity
        {
            if (entity is ICreatedInfo && createdBy != null)
            {
                PropertyInfo prop = entity.GetType().GetProperty("CreatedBy", BindingFlags.Public | BindingFlags.Instance);
                if (null != prop && prop.CanWrite)
                {
                    prop.SetValue(entity, createdBy, null);
                }

                PropertyInfo propCreated = entity.GetType().GetProperty("CreatedDate", BindingFlags.Public | BindingFlags.Instance);
                if (null != propCreated && propCreated.CanWrite)
                {
                    propCreated.SetValue(entity, System.DateTime.UtcNow, null);
                }
            }
            context.Set<TEntity>().Add(entity);

            return entity;
        }

        public virtual void Update<TEntity>(TEntity entity, string modifiedBy = null)
            where TEntity : class, IEntity
        {
            if (entity is IModifiedInfo && modifiedBy != null)
            {
                PropertyInfo prop = entity.GetType().GetProperty("ModifiedBy", BindingFlags.Public | BindingFlags.Instance);
                if (null != prop && prop.CanWrite)
                {
                    prop.SetValue(entity, modifiedBy, null);
                }

                PropertyInfo propModified = entity.GetType().GetProperty("ModifiedDate", BindingFlags.Public | BindingFlags.Instance);
                if (null != propModified && propModified.CanWrite)
                {
                    propModified.SetValue(entity, System.DateTime.UtcNow, null);
                }
            }

            context.Set<TEntity>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete<TEntity>(object id)
            where TEntity : class, IEntity
        {
            TEntity entity = context.Set<TEntity>().SingleOrDefault(m => m.Id == id);
            Delete(entity);
        }

        public virtual void Delete<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            var dbSet = context.Set<TEntity>();
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public virtual void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw e;
            }
        }

        public virtual Task SaveAsync(CancellationToken cancellationToken)
        {
            try
            {
                return context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException e)
            {
                throw e;
            }

            //return Task.FromResult(0);
        }

       

    }
}
