export abstract class DashboardSignalRBase {
    connection: any;

    protected abstract onUsersChanged: () => void
    protected abstract onQuestionsChanged: () => void

    protected registerSignalRHub = (dashboardId :string, userId: string, userName: string ) => {

        const signalR: any = (window as any).signalR;


        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(`/hub?dashboardId=${dashboardId}&userId=${userId}&name=${userName}`)
            .build();

        this.connection.on("refreshDashboard", () => this.onQuestionsChanged());
        this.connection.on("refreshUserInfo", () => this.onUsersChanged());
        this.connection.start();
    }
}