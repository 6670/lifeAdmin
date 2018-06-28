using EntityDataAccess.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EntityDataAccess.GenericRepository
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal EFDbContext _context;
        internal DbSet<TEntity> _dbSet;

        public GenericRepository()
        {
            this._context = new EFDbContext();
            this._dbSet = _context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get()
        {
            IQueryable<TEntity> query = _dbSet;
            return query.ToList();
        }

        public virtual TEntity GetByID(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual TEntity Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public virtual IEnumerable<TEntity> GetMany(Func<TEntity, bool> where)
        {
            return _dbSet.Where(where).ToList();
        }

        public virtual IQueryable<TEntity> GetManyQueryable(Func<TEntity, bool> where)
        {
            return _dbSet.Where(where).AsQueryable();
        }

        public TEntity Get(Func<TEntity, Boolean> where)
        {
            return _dbSet.Where(where).FirstOrDefault<TEntity>();
        }

        public void Delete(Func<TEntity, Boolean> where)
        {
            IQueryable<TEntity> objects = _dbSet.Where<TEntity>(where).AsQueryable();
            foreach (TEntity obj in objects)
                _dbSet.Remove(obj);
        }


        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return _dbSet.Where(predicate).AsQueryable();
            }
            return _dbSet.AsQueryable();
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = GetAll(predicate);

            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
      



        public IQueryable<TEntity> GetWithInclude(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate, params string[] include)
        {
            IQueryable<TEntity> query = this._dbSet;
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query.Where(predicate);
        }

        public bool Exists(object primaryKey)
        {
            return _dbSet.Find(primaryKey) != null;
        }

        public TEntity GetSingle(Func<TEntity, bool> predicate)
        {
            return _dbSet.Single<TEntity>(predicate);
        }

        public TEntity GetFirst(Func<TEntity, bool> predicate)
        {
            return _dbSet.First<TEntity>(predicate);
        }

    
        
        //=======================================  Async  ================================

        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }


        public async Task<TEntity> GetAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }


        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
        {
            return await _context.Set<TEntity>().SingleOrDefaultAsync(match);
        }


        public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
        {
            return await _context.Set<TEntity>().Where(match).ToListAsync();
        }


        public async Task<TEntity> AddAsync(TEntity t)
        {
            _context.Set<TEntity>().Add(t);
            await _context.SaveChangesAsync();
            return t;
        }



        public async Task<TEntity> UpdateAsync(TEntity updated, int key)
        {
            if (updated == null)
                return null;

            TEntity existing = await _context.Set<TEntity>().FindAsync(key);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(updated);
                await _context.SaveChangesAsync();
            }
            return existing;
        }



        public async Task<int> DeleteAsync(TEntity t)
        {
            _context.Set<TEntity>().Remove(t);
            return await _context.SaveChangesAsync();
        }


        public async Task<int> CountAsync()
        {
            return await _context.Set<TEntity>().CountAsync();
        }

    }
}
