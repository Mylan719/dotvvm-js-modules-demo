export class DashboardSignalRBase {
    connection;
    registerSignalRHub = (dashboardId, userId, userName) => {
        const signalR = window.signalR;
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(`/hub?dashboardId=${dashboardId}&userId=${userId}&name=${userName}`)
            .build();
        this.connection.on("refreshDashboard", () => this.onQuestionsChanged());
        this.connection.on("refreshUserInfo", () => this.onUsersChanged());
        this.connection.start();
    };
}
//# sourceMappingURL=DashboardSignalRBase.js.map