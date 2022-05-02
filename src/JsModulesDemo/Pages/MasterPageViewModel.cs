using System.Security.Claims;
using DotVVM.Framework.Hosting;
using JsModulesDemo.Model;
using JsModulesDemo.Model.Enums;

namespace JsModulesDemo.Pages
{
    public class MasterPageViewModel : ViewModelBase
    {
        public AlertModel Alert { get; set; } = new AlertModel();
        public object Errors { get; set; } = new object();
        public EnumHolder<MemberRole> CurrentMemberRole { get; set; } = new EnumHolder<MemberRole>
        {
            IntValue = (int)MemberRole.Guest,
            Value = MemberRole.Guest
        };

        public override Task PreRender()
        {
            CurrentMemberRole = GetCurrentMemberRoleOrDefault();
            return base.PreRender();
        }

        private EnumHolder<MemberRole> GetCurrentMemberRoleOrDefault()
        {
            var roleString = ""; //(Context.GetAspNetCoreContext().User?.Identity as ClaimsIdentity).FindFirst(CustomClaims.MemberRole)?.Value;
            Enum.TryParse(roleString, out MemberRole claimRole);

            return new EnumHolder<MemberRole>
            {
                IntValue = (int)claimRole,
                Value = claimRole
            };
        }
    }
}

