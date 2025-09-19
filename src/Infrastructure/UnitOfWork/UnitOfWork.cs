using Challenge_sprint.src.Domain.Entities;
using Challenge_sprint.src.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_sprint.src.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IRepository<User> Users { get; }
        public IRepository<SelfAssessment> Assessments { get; }
        public IRepository<JournalEntry> JournalEntries { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Users = new Repository<User>(_context);
            Assessments = new Repository<SelfAssessment>(_context);
            JournalEntries = new Repository<JournalEntry>(_context);
        }

        public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();
    }
}
