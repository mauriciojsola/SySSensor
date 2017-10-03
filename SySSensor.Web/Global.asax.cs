using System;
using System.Security.Claims;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SySSensor.Web.App_Start;
using Hangfire;
using Hangfire.SqlServer;
using SySSensor.Core.Services;

namespace SySSensor.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private BackgroundJobServer _backgroundJobServer;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            System.Web.Http.GlobalConfiguration.Configure(WebApiConfig.Register);//WEB API 1st
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);//MVC 2nd
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Name;

            var hangFireOptions = new SqlServerStorageOptions
            {
                PrepareSchemaIfNecessary = true // we install the db schema updates using migrations. See the Install.sql file from the Hangfire nuget package folder.
                //QueuePollInterval = TimeSpan.FromSeconds(15) // default
            };
            GlobalConfiguration.Configuration.UseSqlServerStorage("Repository", hangFireOptions);
            _backgroundJobServer = new BackgroundJobServer();

            RecurringJob.AddOrUpdate<RemotingService>("UpdateLogFilesJob", x => x.UpdateLogFiles(), Cron.Hourly);
            RecurringJob.AddOrUpdate<RemotingService>("RetrieveLogsContent", x => x.RetrieveLogsContent(), Cron.Hourly);
        }

        protected void Application_End(object sender, EventArgs e)
        {
            _backgroundJobServer.Dispose();
        }
    }
}
