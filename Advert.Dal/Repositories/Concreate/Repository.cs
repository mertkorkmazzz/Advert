using Advert.Core;
using Advert.Dal.Context;
using Advert.Dal.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Advert.Dal.Repositories.Concreate
{
    public class Repository<T> : IRepository<T> where T : class, IEntityBase, new()
    {


        private readonly AppDbContext dbContext;
        public Repository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        private DbSet<T> Table { get => dbContext.Set<T>(); }




        //tüm kayıtları getirmek için kullanılır
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;
            if (predicate != null)
                query = query.Where(predicate);

            if (includeProperties.Any())
                foreach (var item in includeProperties)
                    query = query.Include(item);

            return await query.ToListAsync();
        }

        //Ekleme yapmak için kullanılır
        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
        }

        //Koşula uyan tek bir kaydı almak için kullanılır
        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;
            query = query.Where(predicate);

            if (includeProperties.Any())
                foreach (var item in includeProperties)
                    query = query.Include(item);

            return await query.SingleAsync();
        }

        //Belirli bir guid değerine göre varlık döndürür
        public async Task<T> GetByGuidAsync(Guid id)
        {
            return await Table.FindAsync(id);
        }

        //Güncelleme işlemi 
        public async Task<T> UpdateAsync(T entity)
        {
            await Task.Run(() => Table.Update(entity));
            return entity;
        }

        //Silme eişlemi 
        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() => Table.Remove(entity));
        }

        //Koşul sağlanıyorsa true döner aksi takdirde false döner koşulan göre varlık varmı diye kontrol eder
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await Table.AnyAsync(predicate);
        }

        //Belirli bir koşula göre kaç tane kayıt olduğunu sayar. Eğer koşul verilmezse tüm kayıtları sayar.
        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate is not null)
                return await Table.CountAsync(predicate);
            return await Table.CountAsync();
        }
    }
}
