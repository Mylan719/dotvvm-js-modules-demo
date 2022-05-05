using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;
using JsModulesDemo.BusinessServices;
using JsModulesDemo.Model;

namespace JsModulesDemo.Pages.Dashboard
{
    public class DashboardViewModel : MasterPageViewModel
    {
        private readonly QuestionService questionService;
        private readonly ActiveUserService userService;

        [FromRoute("dashboardId")]
        public string? DashboardIdQueryParameter { get; set; }
        public Guid? DashboardId => Guid.TryParse(DashboardIdQueryParameter, out Guid guid) ? guid : (Guid?)null;

        public string NewQuestionText { get; set; } = "";

        public UserModel CurentUser { get; set; } = new UserModel();
        public List<QuestionModel> Questions { get; set; } = new List<QuestionModel>();
        public UserInfoModel UserInfo { get; set; } = new UserInfoModel();

        public bool ShowLogin { get; set; } = true;

        [Bind(Direction.ServerToClient)]
        public string Location { get; set; }

        public DashboardViewModel(QuestionService questionService, ActiveUserService userService)
        {
            this.questionService = questionService;
            this.userService = userService;
        }

        public override async Task PreRender()
        {
            Location = Context.HttpContext.Request.Url.ToString(); 

            if (!Context.IsPostBack && DashboardId is not null)
            {
                Questions = questionService.GetQuestions(DashboardId.Value);
                UserInfo = userService.GetUsersInfo(DashboardId.Value);
            }
            await base.PreRender();
        }

    }
}

