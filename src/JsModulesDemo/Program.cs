using DotVVM.Framework.Routing;
using JsIntegrationDemo.Hubs;
using JsModulesDemo;
using JsModulesDemo.BusinessServices;
using JsModulesDemo.UiServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDotVVM<DotvvmStartup>();
builder.Services.AddBusinessServices();
builder.Services.AddUiServices();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(app.Environment.ContentRootPath);
dotvvmConfiguration.AssertConfigurationIsValid();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<QuestionHub>("/hub");
});

app.Run();
