import { DashboardSignalRBase } from "../../Resources/Scripts/SignalR/DashboardSignalRBase.js"

export default moduleContext => new DashboardModule(moduleContext);

class DashboardModule extends DashboardSignalRBase
{
    private moduleContext;

    constructor(moduleContext) {
        super();
        this.moduleContext = moduleContext;
    }

    public registerHub(dashboardId: string, userId: string, userName: string) {
        this.registerSignalRHub(dashboardId, userId, userName);
    }

    protected onUsersChanged: () => void = () => {
        this.moduleContext.namedCommands["RefreshUserInfo"]();
    };

    protected onQuestionsChanged: () => void = () => {
         this.moduleContext.namedCommands["RefreshQuestions"]();
    }  
} 