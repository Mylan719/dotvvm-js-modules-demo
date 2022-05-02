using DotVVM.Framework.ViewModel;

namespace JsModulesDemo.Pages.Default
{
    public class DefaultViewModel : MasterPageViewModel
    {
        [Bind(Direction.ServerToClient)]
        public Guid CurrentMemberTeamId { get; set; }


        public DefaultViewModel()
        {

        }
    }
}

