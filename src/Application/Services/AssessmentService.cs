using Challenge_sprint.src.Domain.Entities;
using Challenge_sprint.src.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_sprint.src.Application.Services
{
    public class AssessmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AssessmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Cria uma autoavaliação para um usuário
        public async Task<SelfAssessment> CreateAssessmentAsync(User user, int score, string notes = "Avaliação inicial")
        {
            var riskLevel = CalculateRiskLevel(score);

            var assessment = new SelfAssessment
            {
                UserId = user.Id,
                Score = score,
                RiskLevel = riskLevel,
                Notes = notes
            };

            await _unitOfWork.Assessments.AddAsync(assessment);
            await _unitOfWork.SaveChangesAsync();

            return assessment;
        }

        // Calcula o nível de risco baseado na pontuação
        public string CalculateRiskLevel(int score)
        {
            return score <= 7 ? "Baixo" : score <= 15 ? "Médio" : "Alto";
        }

        // Lista todas as autoavaliações de um usuário
        public IQueryable<SelfAssessment> GetAssessments(User user)
        {
            return _unitOfWork.Assessments.Query().Where(a => a.UserId == user.Id);
        }
    }
}
