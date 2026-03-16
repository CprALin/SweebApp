using Microsoft.AspNetCore.Routing.Constraints;
using System;

namespace SweebAppAPIs.Data.Repositories
{
	public interface IAlertRepository
	{
		Task AddAlert(int userId, int deviceId, int threatEventId, string severity, int isRead);
		Task<Models.Alerts?> GetHotAlert(int userId , int deviceId , int threatEventId);
		Task<List<Models.Alerts>> GetAllAlertsForDevice(int userId, int deviceId);
		Task UpdateAsReadAlert(int alertId, int userId);
		Task<List<Models.AlertsFeed>> GetAlertsFeedByUser(int userId);
		Task<List<Models.AlertsFeed>> GetAlertsFeedByDevice(int deviceId , int userId);
		Task<List<Models.AlertsFeed>> GetUnreadAlertsCount(int userId);
	
	}
}
