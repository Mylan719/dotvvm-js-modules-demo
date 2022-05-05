using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.Styles;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ResourceManagement;

namespace JsModulesDemo
{
    public class DotvvmStartup : IDotvvmStartup, IDotvvmServiceConfigurator
    {
        // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add("Default", "", "Pages/Default/Default.dothtml");
            config.RouteTable.Add("Dashboard", "dashboard/{dashboardId}", "Pages/Dashboard/Dashboard.dothtml");

            ConfigureControls(config, applicationPath);
            ConfigureResources(config, applicationPath);
            ConfigureImports(config);

            config.Security.VerifySecFetchForPages.Enabled = false;
        }

        private static void ConfigureImports(DotvvmConfiguration config)
        {

        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            config.Markup.AutoDiscoverControls(new DefaultControlRegistrationStrategy(config, "cc", "Controls"));
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            ConfigureStyleResources(config);
            ConfigureScriptResources(config);
            ConfigurePagesResources(config);
            ConfigureMarkupControlsResources(config);
            UseDeferUpdates(config);
        }

        private static void ConfigureStyleResources(DotvvmConfiguration config)
        {
            config.Resources.Register("style-css", new StylesheetResource(new FileResourceLocation("wwwroot/style.css")));
            config.Resources.Register("signalr-js", new ScriptResource(new FileResourceLocation("wwwroot/libs/signalr/signalr.js")));
            config.Resources.Register("qrcode-js", new ScriptResource(new FileResourceLocation("wwwroot/libs/qrcode/qrcode.js")));

        }

        private static void ConfigureScriptResources(DotvvmConfiguration config)
        {
        }

        private void ConfigureMarkupControlsResources(DotvvmConfiguration config)
        {
            var webHostEnvironment = config.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            var globalControls = config.Markup.Controls
                .Where(control => control.Src != null)
                .ToList();

            foreach (var control in globalControls)
            {
                var controlName = control.TagName;
                var controlResourceLocationPrefix = GetControlDirectoryRelativePath(webHostEnvironment, control);
                RegisterStyleAndScript(config, controlName, controlResourceLocationPrefix);
            }
        }

        private void ConfigurePagesResources(DotvvmConfiguration config)
        {
            foreach (var route in config.RouteTable)
            {
                if (route.VirtualPath.StartsWith("Pages"))
                {
                    var pageName = Path.GetFileNameWithoutExtension(route.VirtualPath);
                    var pagePath = Path.GetDirectoryName(route.VirtualPath).Replace('\\', '/');
                    RegisterStyleAndScript(config, pageName, pagePath, route.RouteName);
                }
            }
        }

        private void RegisterStyleAndScript(DotvvmConfiguration config, string fileName, string path, string resourceName = null)
        {
            resourceName ??= fileName;

            var cssUrlLocation = $"{path}/{fileName}.style.js";
            AddJavaScriptResourceIfExists(config, $"{resourceName}-css", cssUrlLocation);
            var jsUrlLocation = $"{path}/{fileName}.js";
            AddJavaScriptResourceIfExists(config, $"{resourceName}-js", jsUrlLocation);
        }

        private static string GetControlDirectoryRelativePath(IWebHostEnvironment webHostEnvironment, DotvvmControlConfiguration control)
        {
            if (Path.IsPathRooted(control.Src))
            {
                return Path.GetDirectoryName(Path.GetRelativePath(webHostEnvironment.ContentRootPath, control.Src)).Replace('\\', '/');
            }
            return Path.GetDirectoryName(control.Src).Replace('\\', '/');
        }

        private void AddStyleResourceIfExists(DotvvmConfiguration config, string resourceName, string cssUrlLocation)
        {
            if (CheckIfFileExistsInWebRootPath(config, cssUrlLocation))
            {
                config.Resources.Register(resourceName, new StylesheetResource(new UrlResourceLocation("~/" + cssUrlLocation)));
            }
        }

        private void AddJavaScriptResourceIfExists(DotvvmConfiguration config, string resourceName, string jsUrlLocation)
        {
            if (CheckIfFileExistsInWebRootPath(config, jsUrlLocation))
            {
                config.Resources.Register(resourceName, new ScriptModuleResource(new UrlResourceLocation("~/" + jsUrlLocation), defer: true)
                {
                    Dependencies = new[] { "dotvvm" }
                });
            }
        }

        private static bool CheckIfFileExistsInWebRootPath(DotvvmConfiguration config, string fileSubPath)
        {
            var webHostEnvironment = config.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            var fileInfo = webHostEnvironment.WebRootFileProvider.GetFileInfo(fileSubPath);
            return fileInfo.Exists;
        }

        private static void UseDeferUpdates(DotvvmConfiguration config)
        {
            config.Resources.Register("knockout-options", new InlineScriptResource("ko.options.deferUpdates = true") { Dependencies = new[] { "knockout" } });
            if (config.Resources.FindResource(ResourceConstants.DotvvmResourceName + ".internal") is ScriptResource dotvvmResource)
            {
                dotvvmResource.Dependencies = dotvvmResource.Dependencies.Concat(new[] { "knockout-options" }).ToArray();
            }
        }

        public void ConfigureServices(IDotvvmServiceCollection options)
        {
            options.AddUploadedFileStorage("temp");
            options.AddDefaultTempStorages("temp");
        }
    }
}
