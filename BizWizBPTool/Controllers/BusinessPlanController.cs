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

        [HttpGet("{planId}")]
        public async Task<ActionResult<BusinessPlan>> GetBusinessPlanById(int planId)
        {
            var businessPlan = await _businessPlanRepository.GetByIdAsync(planId);
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
            return CreatedAtAction(nameof(GetBusinessPlanById), new {planId = businessPlan.PlanId}, businessPlan);
        }

        [HttpDelete("{planId}")]
        public async Task<ActionResult> DeleteBusinessPlanById(int planId)
        {
            await _businessPlanRepository.DeleteBusinessPlanAsync(planId);
            return NoContent();
        }

        [HttpPut("{planId}")]
        public async Task<ActionResult<BusinessPlan>> UpdateBusinessPlan(int planId, BusinessPlan businessPlan)
        {
            if (planId != businessPlan.PlanId)
            {
                return BadRequest();
            }
            await _businessPlanRepository.UpdateBusinessPlanAsync(businessPlan);
            return CreatedAtAction(nameof(GetBusinessPlanById), new { planId = businessPlan.PlanId }, businessPlan);
        }
    }
}
