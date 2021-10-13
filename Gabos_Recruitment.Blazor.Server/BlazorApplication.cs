using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.SystemModule;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DevExpress.ExpressApp.Xpo;
using Gabos_Recruitment.Blazor.Server.Services;

namespace Gabos_Recruitment.Blazor.Server
{
    public partial class Gabos_RecruitmentBlazorApplication : BlazorApplication
    {
        public Gabos_RecruitmentBlazorApplication()
        {
            InitializeComponent();
        }
        protected override void OnSetupStarted()
        {
            base.OnSetupStarted();
            IConfiguration configuration = ServiceProvider.GetRequiredService<IConfiguration>();
            if (configuration.GetConnectionString("ConnectionString") != null)
            {
                ConnectionString = configuration.GetConnectionString("ConnectionString");
            }
#if EASYTEST
            if(configuration.GetConnectionString("EasyTestConnectionString") != null) {
                ConnectionString = configuration.GetConnectionString("EasyTestConnectionString");
            }
#endif
#if DEBUG
            DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
#endif
        }

        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args)
        {
            IXpoDataStoreProvider dataStoreProvider = GetDataStoreProvider(args.ConnectionString, args.Connection);
            args.ObjectSpaceProviders.Add(new XPObjectSpaceProvider(dataStoreProvider, true));
            args.ObjectSpaceProviders.Add(new NonPersistentObjectSpaceProvider(TypesInfo, null));
        }

        private IXpoDataStoreProvider GetDataStoreProvider(string connectionString, System.Data.IDbConnection connection)
        {
            XpoDataStoreProviderAccessor accessor = ServiceProvider.GetRequiredService<XpoDataStoreProviderAccessor>();
            lock (accessor)
            {
                if (accessor.DataStoreProvider == null)
                {
                    accessor.DataStoreProvider = XPObjectSpaceProvider.GetDataStoreProvider(connectionString, connection, true);
                }
            }
            return accessor.DataStoreProvider;
        }

        private void Gabos_RecruitmentBlazorApplication_DatabaseVersionMismatch(object sender, DatabaseVersionMismatchEventArgs e)
        {
#if EASYTEST
            e.Updater.Update();
            e.Handled = true;
#else
            e.Updater.Update();
            e.Handled = true;
#endif
        }
    }
}
