using JsModulesDemo.Error;
using JsModulesDemo.Model;
using System.Collections.Concurrent;

namespace JsModulesDemo.BusinessServices
{
    public class ActiveUserService : IService
    {
        private static ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, DashboardUser>> usersByDashboard { get; set; }
            = new ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, DashboardUser>>();

        public Guid AddActiveUser(Guid dashboardId, Guid userId, string name)
        {
            var userById = EnsureBag(dashboardId);
            var user = new DashboardUser(userId, dashboardId, name);
            userById.AddOrUpdate(userId, user, (id, oldUser) => user);
            return user.DashboardId;
        }

        public Guid RemoveDashboardUser(Guid dashboardId, Guid userId)
        {
            var bag = EnsureBag(dashboardId);
            bag.TryRemove(userId, out DashboardUser _);
            return dashboardId;
        }

        private static ConcurrentDictionary<Guid, DashboardUser> EnsureBag(Guid dashboardId)
        {
            return usersByDashboard.GetOrAdd(dashboardId, mId => new ConcurrentDictionary<Guid, DashboardUser>());
        }

        public UserModel GetActiveUser(Guid dashboardId, Guid userId)
        {
            var dashboardUser = EnsureBag(dashboardId).TryGetValue(userId, out var user)
                ? user
                : throw ClientErrorResultException.Create("User not found.");

            return new UserModel { Id = dashboardUser.UserId, Name = dashboardUser.Name };
        }

        public UserInfoModel GetUsersInfo(Guid dashboardId)
        {
            var bag = EnsureBag(dashboardId);
            var distinct = bag.Values.Distinct().ToList();

            return new UserInfoModel
            {
                Count = distinct.Count,
                Names = distinct.Select(u => u.Name).ToList()
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
