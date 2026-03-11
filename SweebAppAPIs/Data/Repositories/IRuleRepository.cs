using System;

namespace SweebAppAPIs.Data.Repositories
{
	public interface IRuleRepository
	{
		Task AddRule(int userId, string ruleName, int isEnabled, int priority, string action, string matchType, string pattern);
		Task<Models.Rules?> GetUserRulesByUserId(int userId);
		Task DeleteRuleById(int ruleId);
	}
}
