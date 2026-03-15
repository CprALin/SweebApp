namespace SweebAppAPIs.Models
{
    public class RuleHitsActivity
    {
       public int IdRuleHit { get; set; }
	   public string RuleHitTimestamp {  get; set; }
	   public int UserId { get; set; }
	   public int RuleId {  get; set; }
	   public string RuleName { get; set; }
	   public string Action { get; set; }
	   public int ThreatEventId { get; set; }
	   public string URL { get; set; }
	   public string Host { get; set; }
	   public string Status { get; set; }
	   public int DeviceId { get; set; }
	   public string DeviceName { get; set; }
	   public string ThreatTimestamp { get; set; }
    }
}
