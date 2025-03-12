using BizWizBPTool.Models;
using BizWizBPTool.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BizWizBPTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessPlanController : ControllerBase
    {
        private readonly IBusinessPlanRepository _businessPlanRepository;

        public BusinessPlanController(IBusinessPlanRepository businessPlanRepository)
        {
            _businessPlanRepository = businessPlanRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusinessPlan>>> GetAllAsync()
        {
            return Ok(await _businessPlanRepository.GetAllAsync());
        }

        [HttpGet("{PlanId}")]
        public async Task<ActionResult<BusinessPlan>> GetByIdAsync(int PlanId)
        {
            var businessPlan = await _businessPlanRepository.GetByIdAsync(PlanId);
            if (businessPlan == null)
            {
                return NotFound();
            }
            return Ok(businessPlan);
        }

        [HttpPost]
        public async Task<ActionResult<BusinessPlan>> CreateBusinessPlan(BusinessPlan businessPlan)
        {
            await _businessPlanRepository.AddBusinessPlanAsync(businessPlan);
            return Created();
        }
    }
}
