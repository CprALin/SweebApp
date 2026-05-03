using Microsoft.EntityFrameworkCore;
using SweebAppAPIs.Data.Repositories.Interfaces;
using SweebAppAPIs.Enum;
using SweebAppAPIs.Models;

namespace SweebAppAPIs.Data.Repositories
{
    public class ThreatRepository(AppDbContext context) : IThreatRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<ThreatEvent> CreateThreatAsync(ThreatEvent threatEvent)
        {
            _context.ThreatEvents.Add(threatEvent);

            await _context.SaveChangesAsync();

            return threatEvent;
        }
        
        public async Task<List<ThreatEvent>> GetAllThreatsAsync()
        {
           return await _context.ThreatEvents.ToListAsync();
        }
        
        public async Task<List<ThreatEvent>> GetThreatsByStatusAsync(ThreatStatus status)
        {
           return await _context.ThreatEvents.Where(t => t.ActionTaken == status).ToListAsync();
        }

        public async Task UpdateThreatAsync(int threatId, ThreatStatus newStatus)
        {
            var threat = await _context.ThreatEvents.FindAsync(threatId);
            if (threat != null)
            {
                threat.ActionTaken = newStatus;
                await _context.SaveChangesAsync();
            }
        }
    }
}
