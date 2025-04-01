using BizWizBPTool.Models;

namespace BizWizBPTool.Repositories
{
    public interface IBusinessPlanRepository
    {
        Task<IEnumerable<BusinessPlan>> GetAllAsync();

        Task<BusinessPlan?> GetByIdAsync(int planId);

        Task AddBusinessPlanAsync(BusinessPlan businessPlan);

        Task UpdateBusinessPlanAsync(BusinessPlan businessPlan);

        Task DeleteBusinessPlanAsync(int planId);
    }
}
