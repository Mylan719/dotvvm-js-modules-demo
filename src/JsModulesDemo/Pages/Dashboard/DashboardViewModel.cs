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

        [FromRoute("dashboardId")]
        public string? DashboardIdQueryParameter { get; set; }
        public Guid? DashboardId => Guid.TryParse(DashboardIdQueryParameter, out Guid guid) ? guid : (Guid?)null;

        public List<QuestionModel> Questions { get; set; } = new List<QuestionModel>();

        public DashboardViewModel(QuestionService questionService)
        {
            this.questionService = questionService;
        }

        public override async Task PreRender()
        {
            if (!Context.IsPostBack && DashboardId is not null)
            {
                Questions = questionService.GetQuestions(DashboardId.Value);
            }
            await base.PreRender();
        }

    }
}

