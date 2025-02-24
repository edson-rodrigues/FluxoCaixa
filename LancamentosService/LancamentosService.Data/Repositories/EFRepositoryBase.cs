using LancamentosService.Data.Context;
using LancamentosService.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LancamentosService.Data.Repositories
{
    public abstract class EFRepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly AppDbContext db;

        protected EFRepositoryBase(AppDbContext context)
        {
            db = context;
        }
        public virtual async Task AddAsync(TEntity obj)
        {
            await db.AddAsync(obj);
            await db.SaveChangesAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int? id) =>
            await db.Set<TEntity>().FindAsync(id);

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() =>
            await db.Set<TEntity>().ToListAsync();

        public virtual async Task UpdateAsync(TEntity obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public virtual async Task SaveChangesAsync()
        {
            await db.SaveChangesAsync();
        }

        public async Task RemoveAsync(TEntity obj)
        {
            db.Set<TEntity>().Remove(obj);
            await db.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<TEntity> obj)
        {
            db.Set<TEntity>().RemoveRange(obj);
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await db.Set<TEntity>().Where(predicate).ToListAsync();
        }

        private bool _disposed = false;

        ~EFRepositoryBase() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    db.Dispose();

                _disposed = true;
            }
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync() => await db.Database.BeginTransactionAsync();

        public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await db.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }
    }
}
