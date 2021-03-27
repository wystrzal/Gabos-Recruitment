using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;
using Gabos_Recruitment.Module.Services;

namespace Gabos_Recruitment.Module.Controllers.SearchSubjectControllers
{
    public partial class DisableNewButtonInNestedSearchSpecificationController : ViewController
    {
        private const int specificCount = 30;
        private const string disableNewAction = "DisableNewObjectAction";
        private NewObjectViewController newObjectViewController;
        private DeleteObjectsViewController deleteObjectsViewController;

        public DisableNewButtonInNestedSearchSpecificationController()
        {
            InitializeComponent();
            TargetViewId = "SearchSubject_BankAccounts_ListView;SearchSubject_Nips_ListView;SearchSubject_Regons_ListView";
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            newObjectViewController = Frame.GetController<NewObjectViewController>();
            deleteObjectsViewController = Frame.GetController<DeleteObjectsViewController>();

            newObjectViewController.NewObjectAction.Executing += NewObjectAction_Executing;
            deleteObjectsViewController.DeleteAction.ExecuteCompleted += DeleteAction_ExecuteCompleted;
        }

        private void NewObjectAction_Executing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (((ListView)View).CollectionSource.GetCount() == specificCount)
            {
                newObjectViewController.NewObjectAction.Enabled.SetItemValue(disableNewAction, false);

                var selectedItemName = ((SingleChoiceAction)sender).SelectedItem.ToString();

                ErrorService.ShowError(this, selectedItemName, "Pole może posiadać maksymalnie 30 numerów.");

                e.Cancel = true;
            }
        }

        private void DeleteAction_ExecuteCompleted(object sender, ActionBaseEventArgs e)
        {
            newObjectViewController.NewObjectAction.Enabled.RemoveItem(disableNewAction);
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }

        protected override void OnDeactivated()
        {
            newObjectViewController.NewObjectAction.Executing -= NewObjectAction_Executing;
            deleteObjectsViewController.DeleteAction.ExecuteCompleted -= DeleteAction_ExecuteCompleted;
            newObjectViewController.NewObjectAction.Enabled.RemoveItem(disableNewAction);
            base.OnDeactivated();
        }
    }
}