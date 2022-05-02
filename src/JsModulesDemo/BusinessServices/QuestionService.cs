using JsModulesDemo.Model;
using System.Collections.Concurrent;

namespace JsModulesDemo.BusinessServices
{
    public class QuestionService : IService
    {
        private readonly ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, QuestionModel>> dashboards = new ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, QuestionModel>>();

        public List<QuestionModel> GetQuestions(Guid dashboardId)
        {
            if(dashboards.TryGetValue(dashboardId, out var models))
            {
                return models.Values.ToList();
            }
            return new List<QuestionModel>();
        }
    }
}
