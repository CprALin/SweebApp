using System;

namespace SweebAppAPIs.Data.Repositories
{
	public interface IRuleRepository
	{
		Task AddRule(int userId, string ruleName, int isEnabled, int priority, string action, string matchType, string pattern);
		Task<Models.Rules?> GetUserRulesByUserId(int userId);
		Task DeleteRuleById(int ruleId);
		Task AddRuleHit(int ruleId, int threatEventId);
		Task<Models.RuleHits?> GetRulesHit(int threatEventId);
		Task<Models.RuleHitsActivity?> GetRuleHitsByActivity(int userId);
		Task<Models.RuleHitsActivity?> GetRuleHitsByDevice(int userId, int deviceId);
		Task<Models.RuleHitsActivity?> GetRuleHitsByRule(int userId, int ruleId);
	}
}
