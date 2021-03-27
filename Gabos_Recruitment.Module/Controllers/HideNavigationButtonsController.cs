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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gabos_Recruitment.Module.Controllers
{
    public partial class HideNavigationButtonsController : ViewController
    {
        private const string displayNavigation = "DisplayNavigation";
        private ViewNavigationController viewNavigationController;

        public HideNavigationButtonsController()
        {
            InitializeComponent();
        }
        protected override void OnActivated()
        {
            viewNavigationController = Frame.GetController<ViewNavigationController>();

            if (viewNavigationController != null)
            {
                viewNavigationController.NavigateBackAction.Active.SetItemValue(displayNavigation, false);
                viewNavigationController.NavigateForwardAction.Active.SetItemValue(displayNavigation, false);
            }

            base.OnActivated();
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            if (viewNavigationController != null)
            {
                viewNavigationController.NavigateBackAction.Active.RemoveItem(displayNavigation);
                viewNavigationController.NavigateForwardAction.Active.RemoveItem(displayNavigation);
            }
            base.OnDeactivated();
        }
    }
}
