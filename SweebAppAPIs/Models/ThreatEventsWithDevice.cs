namespace SweebAppAPIs.Models
{
    public class ThreatEventsWithDevice
    {
       public int IdThreatEvent {  get; }
	   public int UserId { get; set; }
	   public int DeviceID { get; set; }
	   public string Name { get; set; }
	   public string URL {  get; set; }
	   public string Protocol {  get; set; }
	   public string Host { get; set; }
	   public string Path { get; set; }
	   public string Status {  get; set; }
	   public string Verdict {  get; set; }
	   public string ActionTaken {  get; set; }
	   public int Score {  get; set; }
	   public string Category {  get; set; }
	   public string Timestamp { get; set; }
    }
}
