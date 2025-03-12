using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BizWizBPTool.Models
{
    public class BusinessPlan
    {
        [Key]
        public int PlanId { get; set; }
        public string PlanName { get; set; }
        public string PlanDescription { get; set; }
    }
}
