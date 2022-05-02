export default moduleContext => new DefaultModule(moduleContext);

class DefaultModule {
    private moduleContext;

    constructor(moduleContext) { 
        this.moduleContext = moduleContext;
    }
} 