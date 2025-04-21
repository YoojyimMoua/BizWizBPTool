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
        public string ExecutiveSummary {get; set;}
        public string CompanyDescription { get; set; }
        public string MarketResearch { get; set; }
        public string ServiceLine { get; set; }
        public string MarketingAndSales { get; set; }
        
    }
}
