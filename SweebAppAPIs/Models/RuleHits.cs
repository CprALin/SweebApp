using System.ComponentModel.DataAnnotations;

namespace SweebAppAPIs.Models
{
    public class RuleHits
    {
        [Key]
        public int IdRuleHit { get; set; }
        public int RuleId { get; set; }
        public int ThreatEventId { get; set; }
        public string Timestamp { get; set; }
    }
}
