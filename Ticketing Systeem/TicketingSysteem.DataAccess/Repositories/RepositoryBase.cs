using TicketingSysteem.Entities.Pocos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using TicketingSysteem.Entities.SearchPocos;

namespace TicketingSysteem.DataAccess.Repositories
{
    public class RepositoryBase<TEntity> : IRepository<TEntity>
                                                        where TEntity : EntityBase
    {
        private readonly IEntityFilter<TEntity> _entityFilter;

        public RepositoryBase(IEntityFilter<TEntity> entityFilter)
        {
            _entityFilter = entityFilter;
        }

        #region Get
        public async Task<TEntity> GetAsync(int id, IEnumerable<string> includes = null)
        {
            try
            {
                using (var context = new EntityContext())
                {
                    IQueryable<TEntity> query = GetDBSet(context);
                    query = SetIncludes(includes, query);
                    return await query.Where(x => x.Id == id).SingleOrDefaultAsync();
                }
            }
            catch (Exception)
            {
                //TODO
                return null;
            }
        }

        public async Task<IList<TEntity>> GetAllAsync(IEnumerable<string> includes = null)
        {
            try
            {
                using (var context = new EntityContext())
                {
                    IQueryable<TEntity> query = GetDBSet(context);
                    query = SetIncludes(includes, query);
                    return await query.ToListAsync<TEntity>();
                }
            }
            catch (Exception ex)
            {
                //TODO
                return null;
            }
        }

        public async Task<TEntity> GetDeletedAsync(int id, IEnumerable<string> includes = null)
        {
            using (var context = new EntityContext())
            {
                IQueryable<TEntity> query = GetDBSet(context);
                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }
                return await query.Where(x => x.Id == id).SingleOrDefaultAsync();
            }
        }

        public async Task<IList<TEntity>> GetAllDeletedAsync(IEnumerable<string> includes = null)
        {
            try
            {
                using (var context = new EntityContext())
                {
                    IQueryable<TEntity> query = GetDBSet(context);
                    query = SetIncludes(includes, query);
                    return await query.ToListAsync<TEntity>();
                }
            }
            catch (Exception)
            {
                //TODO
                return null;
            }
        }
        #endregion

        #region Search
        public async Task<IList<TEntity>> Search(Expression<Func<TEntity, bool>> searchExpression, IEnumerable<string> includes = null)
        {
            try
            {
                using (var context = new EntityContext())
                {
                    IQueryable<TEntity> query = GetDBSet(context);
                    query = SetIncludes(includes, query);
                    query = query.Where(searchExpression);
                    return await query.ToListAsync<TEntity>();
                }
            }
            catch (Exception ex)
            {
                //TODO
                return null;
            }
        }

        public async Task<IList<TEntity>> Search(SearchBase<TEntity> searchCriteria, IEnumerable<string> includes = null)
        {
            try
            {
                using (var context = new EntityContext())
                {
                    IQueryable<TEntity> query = GetDBSet(context);
                    query = SetIncludes(includes, query);
                    query = _entityFilter.Filter(query, searchCriteria);
                    return await query.ToListAsync<TEntity>();
                }
            }
            catch (Exception ex)
            {
                //TODO
                return null;
            }
        }
        #endregion

        #region Fields
        public IEnumerable<string> GetIncludeList()
        {
            using (var context = new EntityContext())
            {
                Type entityType = typeof(TEntity);

                var objectContext = ((IObjectContextAdapter)context).ObjectContext;
                var entityMetadata = objectContext.CreateObjectSet<TEntity>().EntitySet.ElementType;

                List<string> properties = new List<string>();
                foreach (var navigationProperty in entityMetadata.NavigationProperties)
                {
                    properties.Add(navigationProperty.Name);
                }

                return properties;
            }

        }
        #endregion

        #region Create
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            if (entity == null) throw new Exception("NotFound"); //TODO: notfoundexception maken
            using (var context = new EntityContext())
            {
                entity.CreationDate = DateTime.UtcNow;
                GetDBSet(context).Add(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }
        #endregion

        #region Update
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            using (var context = new EntityContext())
            {
                var existingEntity = await GetDBSet(context).FindAsync(entity.Id);
                if (existingEntity == null) throw new Exception("NotFound"); //TODO: notfoundexception maken
                context.Entry(existingEntity).CurrentValues.SetValues(entity);

                await context.SaveChangesAsync();
                return entity;
            }
        }
        #endregion

        #region Delete
        public async Task DeleteAsync(int id)
        {
            using (var context = new EntityContext())
            {
                var existingEntity = await GetDBSet(context).FindAsync(id);
                if (existingEntity == null) throw new Exception("NotFound"); //TODO: notfoundexception maken
                GetDBSet(context).Remove(existingEntity);

                await context.SaveChangesAsync();
            }
        }
        #endregion
        
        #region Privates
        private DbSet<TEntity> GetDBSet(DbContext context)
        {
            return context.Set<TEntity>();
        }

        private static IQueryable<TEntity> SetIncludes(IEnumerable<string> includes, IQueryable<TEntity> query)
        {
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    if (string.IsNullOrWhiteSpace(include)) continue;
                    try
                    {
                        query = query.Include(include);
                    }
                    catch (Exception)
                    {
                        //TODO:: log
                        //TODO:: return in warning
                    }
                }
            }

            return query;
        }
        #endregion


    }
}
