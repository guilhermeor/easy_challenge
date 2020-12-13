using Hangfire.Dashboard;

namespace EasyChallenge.Bootstrap
{
    public class AllowAllConnectionsFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context) => true;
    }
}