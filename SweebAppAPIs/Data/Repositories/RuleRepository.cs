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

        public async Task<Models.Rules?> GetUserRulesByUserId(int userId)
        {
            return await _context.Rules.FromSqlInterpolated($"EXEC getUserRole {userId}").AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task DeleteRuleById(int ruleId)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC deleteRuleById @IdRule",
                new SqlParameter("@IdRule" , ruleId)
            );
        }
    }
}
