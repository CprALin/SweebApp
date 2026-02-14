namespace SweebAppAPIs.Models
{
    public class Rules
    {
        public int IdRule { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public  bool IsEnabled { get; set; }
        public int Priority { get; set; }
        public string Action { get; set; }
        public string MatchType { get; set; }
        public string Pattern { get; set; }
        public string CreatedAt { get; set; }
    }
}
