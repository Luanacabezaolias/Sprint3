using Challenge_sprint.src.Domain.Entities;
using Challenge_sprint.src.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_sprint.src.Application.Services
{
    public class JournalService
    {
        private readonly IUnitOfWork _unitOfWork;

        public JournalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Cria uma entrada de diário
        public async Task<JournalEntry> AddEntryAsync(User user, string content)
        {
            var entry = new JournalEntry
            {
                UserId = user.Id,
                Content = content
            };

            await _unitOfWork.JournalEntries.AddAsync(entry);
            await _unitOfWork.SaveChangesAsync();

            return entry;
        }

        // Lista todas as entradas de diário de um usuário
        public IQueryable<JournalEntry> GetEntries(User user)
        {
            return _unitOfWork.JournalEntries.Query().Where(e => e.UserId == user.Id);
        }
    }
}
