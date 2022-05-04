using JsModulesDemo.BusinessServices;
using JsModulesDemo.Error;
using Microsoft.AspNetCore.SignalR;

namespace JsIntegrationDemo.Hubs
{
    public class QuestionHub : Hub
    {
        private readonly ActiveUserService participantService;

        public QuestionHub(ActiveUserService participantService)
        {
            this.participantService = participantService;
        }

        public static async void SendNotification(IHubContext<QuestionHub> context, Guid dashboardId, string command)
        {
            await context.Clients.Group(dashboardId.ToString()).SendAsync(command, dashboardId);
        }

        public override async Task OnConnectedAsync()
        {
            var dashboardId = participantService.AddActiveUser(GetDashboardId(), GetUserId(), GetQueryValue("name") ?? "");

            await Groups.AddToGroupAsync(Context.ConnectionId, dashboardId.ToString());

            await base.OnConnectedAsync();

            await Clients.Group(dashboardId.ToString()).SendAsync("refreshUserInfo");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var dashboardId = participantService.RemoveDashboardUser(GetDashboardId(), GetUserId());

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, dashboardId.ToString());

            await base.OnDisconnectedAsync(exception);

            await Clients.Group(dashboardId.ToString()).SendAsync("refreshUserInfo");
        }

        private Guid GetUserId()
        {
            var value = GetQueryValue("userId") ?? throw ClientErrorResultException.Create("No user ID");
            return Guid.Parse(value);
        }

        private Guid GetDashboardId()
        {
            var value = GetQueryValue("dashboardId") ?? throw ClientErrorResultException.Create("No dashboard ID");
            return Guid.Parse(value);
        }

        private string? GetQueryValue(string name)
        {
            var httpContext = Context.GetHttpContext();

            if (!(httpContext?.Request.Query.TryGetValue(name, out var values)==true))
            {
                return null;
            }

            return values.FirstOrDefault() ?? "";
        }
    }
}