using JsModulesDemo.Error;
using JsModulesDemo.Model;
using System.Collections.Concurrent;

namespace JsModulesDemo.BusinessServices
{
    public class UserService : IService
    {
        private static ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, UserModel>> usersByDashboard { get; set; }
            = new ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, UserModel>>();

        public Guid AddUser(Guid dashboardId, string name)
        {
            var userById = EnsureBag(dashboardId);
            var user = new UserModel { Id = Guid.NewGuid(), Name = name };
            userById.AddOrUpdate(user.Id, user, (id, oldUser) => user);
            return dashboardId;
        }

        public Guid RemoveUser(Guid dashboardId, Guid userId)
        {
            var bag = EnsureBag(dashboardId);
            bag.TryRemove(userId, out UserModel _);
            return dashboardId;
        }

        public UserModel GetUser(Guid dashboardId, Guid userId)
        {
            return EnsureBag(dashboardId).TryGetValue(userId, out var user)
                ? user
                : throw ClientErrorResultException.Create("User not found.");
        }
        private static ConcurrentDictionary<Guid, UserModel> EnsureBag(Guid dashboardId)
        {
            return usersByDashboard.GetOrAdd(dashboardId, mId => new ConcurrentDictionary<Guid, UserModel>());
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
