using Microsoft.EntityFrameworkCore;
using SweebAppAPIs.Data.Repositories.Interfaces;
using SweebAppAPIs.Models;

namespace SweebAppAPIs.Data.Repositories
{
    public class AlertRepository(AppDbContext context) : IAlertRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Alert> CreateAlertAsync(Alert alert)
        {
            _context.Alerts.Add(alert);

            await _context.SaveChangesAsync();

            return alert;
        }

        public async Task<List<Alert>> GetAllAlerts()
        {
            return await _context.Alerts.ToListAsync();
        }

        public async Task DeleteAlertAsync(int alertId)
        {
            var alert = await _context.Alerts.FindAsync(alertId);

            if (alert == null)
            {
                return;
            }

            _context.Alerts.Remove(alert);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAllAlertsAsync()
        {
            var allAlerts = await _context.Alerts.ToListAsync();

            _context.Alerts.RemoveRange(allAlerts);

            await _context.SaveChangesAsync();
        }
    }
}
