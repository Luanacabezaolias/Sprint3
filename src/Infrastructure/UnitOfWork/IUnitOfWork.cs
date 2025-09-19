using Challenge_sprint.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_sprint.src.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<SelfAssessment> Assessments { get; }
        IRepository<JournalEntry> JournalEntries { get; } // ← adicionado
        Task<int> SaveChangesAsync();
    }

    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        IQueryable<T> Query();
    }

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        public Repository(DbContext context) => _dbSet = context.Set<T>();
        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
        public IQueryable<T> Query() => _dbSet.AsQueryable();
    }
}
