import { DashboardSignalRBase } from "../../Resources/Scripts/SignalR/DashboardSignalRBase.js";
export default moduleContext => new DashboardModule(moduleContext);
class DashboardModule extends DashboardSignalRBase {
    moduleContext;
    constructor(moduleContext) {
        super();
        this.moduleContext = moduleContext;
    }
    registerHub(dashboardId, userId, userName) {
        this.registerSignalRHub(dashboardId, userId, userName);
    }
    onUsersChanged = () => {
        this.moduleContext.namedCommands["RefreshUserInfo"]();
    };
    onQuestionsChanged = () => {
        this.moduleContext.namedCommands["RefreshQuestions"]();
    };
}
//# sourceMappingURL=Dashboard.js.map