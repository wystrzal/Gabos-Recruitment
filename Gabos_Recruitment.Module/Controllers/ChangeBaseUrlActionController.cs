using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using Gabos_Recruitment.Module.Services.ApiUrlAssigners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gabos_Recruitment.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ChangeBaseUrlActionController : ViewController
    {
        private bool isTestApi;
        private readonly SimpleAction changeBaseUrlAction;
        private readonly SearchApiUrlAssigner searchApiUrlAssigner = SearchApiUrlAssigner.GetInstance();
        private readonly CheckApiUrlAssigner checkApiUrlAssigner = CheckApiUrlAssigner.GetInstance();

        public ChangeBaseUrlActionController()
        {
            InitializeComponent();
            TargetViewId = "CheckSubject_DetailView;SearchSubject_DetailView";

            changeBaseUrlAction = new SimpleAction(this, "ChangeBaseUrlAction", PredefinedCategory.View)
            {
                Caption = "",
                ImageName = "Action_ResetViewSettings"
            };
        }

        private void ChangeBaseUrlAction_Execute(Object sender, SimpleActionExecuteEventArgs e)
        {
            if (isTestApi)
            {
                ChangeSelectedApi(ApiStorage.ProductionApiUrl);
            }
            else
            {
                ChangeSelectedApi(ApiStorage.TestApiUrl);
            }
        }

        protected override void OnActivated()
        {
            changeBaseUrlAction.Execute += ChangeBaseUrlAction_Execute;
            ChangeSelectedApi(ApiStorage.TestApiUrl);

            base.OnActivated();
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            changeBaseUrlAction.Execute -= ChangeBaseUrlAction_Execute;

            base.OnDeactivated();
        }

        private void ChangeSelectedApi(string selectedUrl)
        {
            if (View.Id == "CheckSubject_DetailView")
            {
                SetNewBaseApi(selectedUrl, checkApiUrlAssigner);
            }
            else
            {
                SetNewBaseApi(selectedUrl, searchApiUrlAssigner);
            }
        }

        private void SetNewBaseApi(string selectedUrl, ApiUrlAssigner apiUrlAssigner)
        {
            if (selectedUrl == ApiStorage.TestApiUrl)
            {
                changeBaseUrlAction.Caption = "Use Production API";
                isTestApi = true;
            }
            else
            {
                changeBaseUrlAction.Caption = "Use Test API";
                isTestApi = false;
            }

            apiUrlAssigner.BaseUrl = selectedUrl;
        }
    }
}
