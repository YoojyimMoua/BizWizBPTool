using BizWizBPTool.Data;
using BizWizBPTool.Models;
using Microsoft.EntityFrameworkCore;

namespace BizWizBPTool.Repositories
{
    public class BusinessPlanRepository : IBusinessPlanRepository
    {
        private readonly AppDbContext _context;
        public BusinessPlanRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddBusinessPlanAsync(BusinessPlan businessPlan)
        {
            await _context.BusinessPlans.AddAsync(businessPlan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBusinessPlanAsync(int PlanId)
        {
            var businessPlanInDb = await _context.BusinessPlans.FindAsync(PlanId);

            if (businessPlanInDb == null)
            {
                throw new KeyNotFoundException($"Business Plan with id {PlanId} was not found.");
            }

            _context.BusinessPlans.Remove(businessPlanInDb);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BusinessPlan>> GetAllAsync()
        {
            return await _context.BusinessPlans.ToListAsync();
        }

        public async Task<BusinessPlan?> GetByIdAsync(int PlanId)
        {
            return await _context.BusinessPlans.FindAsync(PlanId);
        }

        public async Task UpdateBusinessPlanAsync(BusinessPlan businessPlan)
        {
            _context.BusinessPlans.Update(businessPlan);
            await _context.SaveChangesAsync();
        }
    }
}
