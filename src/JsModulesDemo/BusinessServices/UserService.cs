using JsModulesDemo.Error;
using JsModulesDemo.Model;
using System.Collections.Concurrent;

namespace JsModulesDemo.BusinessServices
{
    public class UserService : IService
    {
        private static ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, string>> usersByDashboard { get; set; } = new ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, string>>();

        public async Task<Guid> AddParticipant(Guid dashboardId, Guid userId)
        {
            var dashboardUser = await GetDashboardUser(dashboardId, userId);

            var memberNameById = EnsureBag(dashboardUser.DashboardId);
            memberNameById.AddOrUpdate(dashboardUser.DashboardId, dashboardUser.Name, (id, oldName) => dashboardUser.Name);
            return dashboardUser.DashboardId;
        }

        public async Task<Guid> RemoveParticipant(Guid dashboardId, Guid userId)
        {
            var participant = await GetDashboardUser(dashboardId, userId);

            var bag = EnsureBag(participant.DashboardId);
            bag.TryRemove(participant.UserId, out string _);
            return participant.DashboardId;
        }

        private static ConcurrentDictionary<Guid, string> EnsureBag(Guid dashboardId)
        {
            return usersByDashboard.GetOrAdd(dashboardId, mId => new ConcurrentDictionary<Guid, string>());
        }

        private async Task<DashboardUser> GetDashboardUser(Guid dashboardId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public UserInfoModel GetUsersInfo(Guid dashboardId)
        {
            var bag = EnsureBag(dashboardId);
            var distinct = bag.Values.Distinct().ToList();

            return new UserInfoModel
            {
                Count = distinct.Count,
                Names = distinct
            };
        }

        private record DashboardUser
        {
            public string Name { get; set; }
            public Guid UserId { get; set; }
            public Guid DashboardId { get; set; }

            public DashboardUser(Guid userId, Guid dashboardId, string name)
            {
                UserId = userId;
                DashboardId = dashboardId;
                Name = name;
            }

        }
    }
}
