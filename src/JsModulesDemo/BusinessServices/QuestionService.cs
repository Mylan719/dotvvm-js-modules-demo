using JsModulesDemo.Error;
using JsModulesDemo.Model;
using System.Collections.Concurrent;

namespace JsModulesDemo.BusinessServices
{
    public class QuestionService : IService
    {
        private static readonly ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, QuestionModel>> dashboards = new ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, QuestionModel>>();
        private readonly ActiveUserService activeUserService;

        public QuestionService(ActiveUserService activeUserService)
        {
            this.activeUserService = activeUserService;
        }

        public List<QuestionModel> GetQuestions(Guid dashboardId)
        {
            if (dashboards.TryGetValue(dashboardId, out var models))
            {
                return models.Values.OrderByDescending(q=> q.Likes.Count).ThenByDescending(q => q.Created).ToList();
            }
            return new List<QuestionModel>();
        }

        public void AddQuestion(Guid dashboardId, Guid userId, string text)
        {
            var user = activeUserService.GetActiveUser(dashboardId, userId);

            var questions = EnsureBag(dashboardId);

            var questionId = Guid.NewGuid();
            var question = new QuestionModel(questionId, user, text, DateTime.UtcNow);

            if (!questions.TryAdd(questionId, question))
            {
                throw ClientErrorResultException.Create("Question adding failed.");
            };
        }

        public void ToggleLike(Guid dashboardId, Guid userId, Guid questionId)
        {
            var user = activeUserService.GetActiveUser(dashboardId, userId);

            var question = GetQuestion(dashboardId, questionId);

            question.ToggleLike(user);
        }

        public void RemoveQuestion(Guid dashboardId, Guid userId, Guid questionId)
        {
            var user = activeUserService.GetActiveUser(dashboardId, userId);

            var questions = EnsureBag(dashboardId);
            var question = GetQuestion(dashboardId, questionId);

            if (question.Author.Id != questionId)
            {
                throw ClientErrorResultException.Create("Only author can remove questions.");
            }

            if (!questions.TryRemove(questionId, out var _))
            {
                throw ClientErrorResultException.Create("Question removing failed.");
            };
        }

        public QuestionModel GetQuestion(Guid dashboardId, Guid questionId)
        {
            return EnsureBag(dashboardId).TryGetValue(questionId, out var question)
                ? question
                : throw ClientErrorResultException.Create("Question not found.");
        }

        private static ConcurrentDictionary<Guid, QuestionModel> EnsureBag(Guid dashboardId)
        {
            return dashboards.GetOrAdd(dashboardId, mId => new ConcurrentDictionary<Guid, QuestionModel>());
        }
    }
}
