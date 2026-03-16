using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace SweebAppAPIs.Data.Repositories
{
    public class RuleRepository(AppDbContext context) : IRuleRepository
    {
        private readonly AppDbContext _context = context;

        public async Task AddRule(int userId, string ruleName, int isEnabled, int priority, string action, string matchType, string pattern)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC addRule @UserId , @RuleName , @IsEnabled , @Priority , @Action , @MatchType , @Pattern",
                new SqlParameter("@UserId" , userId),
                new SqlParameter("@RuleName" , ruleName),
                new SqlParameter("@IsEnabled" , isEnabled),
                new SqlParameter("@Priority" , priority),
                new SqlParameter("@Action" , action),
                new SqlParameter("@MatchType" , matchType),
                new SqlParameter("@Pattern" , pattern)
            );
        }

        public async Task<List<Models.Rules>> GetUserRulesByUserId(int userId)
        {
            return await _context.Rules.FromSqlInterpolated($"EXEC getUserRole {userId}").ToListAsync();
        }

        public async Task DeleteRuleById(int ruleId)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC deleteRuleById @IdRule",
                new SqlParameter("@IdRule" , ruleId)
            );
        }

        public async Task AddRuleHit(int ruleId, int threatEventId)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC addRuleHit @RuleId , @ThreatEventId",
                new SqlParameter("@RuleId" , ruleId),
                new SqlParameter("@ThreatEventId" , threatEventId)
            );
        }

        public async Task<List<Models.RuleHits>> GetRulesHit(int threatEventId)
        {
            return await _context.RuleHits.FromSqlInterpolated($"EXEC getRulesHit {threatEventId}").ToListAsync();
        }

        public async Task<List<Models.RuleHitsActivity>> GetRuleHitsByUser(int userId)
        {
            return await _context.RuleHitsActivity.FromSqlInterpolated($"EXEC getRuleHitsByUser {userId}").ToListAsync();
        }

        public async Task<List<Models.RuleHitsActivity>> GetRuleHitsByDevice(int userId, int deviceId)
        {
            return await _context.RuleHitsActivity.FromSqlInterpolated($"EXEC getRuleHitsByDevice {userId} , {deviceId}").ToListAsync();
        }

        public async Task<List<Models.RuleHitsActivity>> GetRuleHitsByRule(int userId, int ruleId)
        {
            return await _context.RuleHitsActivity.FromSqlInterpolated($"EXEC getRuleHitsByRule {userId} , {ruleId}").ToListAsync(); 
        }
    }
}
