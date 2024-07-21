using Core.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Base
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        readonly CourseDbContext _context;

        public Repository(CourseDbContext context)
        {
            _context = context;
        }

        DbSet<T> Table => _context.Set<T>(); 

        public IQueryable<T> GetAll()
        {
            return Table;
        }

        public T? Get(int id)
        {
            return Table.Find(id);
        }


        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return Table.Where(expression);
        }

        public void Add(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;
            Table.Add(entity);
        }

        public void Update(T entity)
        {
            entity.ModifiedDate = DateTime.Now;
            Table.Update(entity);
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            Table.Update(entity);
        }

        public void DeleteRange(ICollection<T> entities)
        {
            foreach (T entity in entities)
            {
                entity.IsDeleted = true;
            }

            Table.UpdateRange(entities);
        }
    }
}
