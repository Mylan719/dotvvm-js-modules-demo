export default moduleContext => new DashboardModule(moduleContext);

class DashboardModule {
    private moduleContext;
    constructor(moduleContext) {
        this.moduleContext = moduleContext; 
    }
} 