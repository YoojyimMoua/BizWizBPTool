using BizWizBPTool.Models;

namespace BizWizBPTool.Repositories
{
    public interface IBusinessPlanRepository
    {
        Task<IEnumerable<BusinessPlan>> GetAllAsync();

        Task<BusinessPlan?> GetByIdAsync(int PlanId);

        Task AddBusinessPlanAsync(BusinessPlan businessPlan);

        Task UpdateBusinessPlanAsync(BusinessPlan businessPlan);

        Task DeleteBusinessPlanAsync(int PlanId);
    }
}
