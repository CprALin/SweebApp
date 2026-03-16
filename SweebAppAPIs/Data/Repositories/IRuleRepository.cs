using System;

namespace SweebAppAPIs.Data.Repositories
{
	public interface IRuleRepository
	{
		Task AddRule(int userId, string ruleName, int isEnabled, int priority, string action, string matchType, string pattern);
		Task<List<Models.Rules>> GetUserRulesByUserId(int userId);
		Task DeleteRuleById(int ruleId);
		Task AddRuleHit(int ruleId, int threatEventId);
		Task<List<Models.RuleHits>> GetRulesHit(int threatEventId);
		Task<List<Models.RuleHitsActivity>> GetRuleHitsByUser(int userId);
		Task<List<Models.RuleHitsActivity>> GetRuleHitsByDevice(int userId, int deviceId);
		Task<List<Models.RuleHitsActivity>> GetRuleHitsByRule(int userId, int ruleId);
	}
}
