using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;
using Gabos_Recruitment.Module.Controllers.CheckSubjectController;
using Gabos_Recruitment.Module.Services;

namespace Gabos_Recruitment.Module.Controllers.CheckSubjectControllers
{
    public partial class DisableNewButtonInNestedCheckSpecificationController : ViewController
    {
        private const int specificCount = 1;
        private const string disableNewAction = "DisableNewObjectAction";
        private NewObjectViewController newObjectViewController;
        private DeleteObjectsViewController deleteObjectsViewController;

        public DisableNewButtonInNestedCheckSpecificationController()
        {
            InitializeComponent();
            TargetViewId = "CheckSubject_BankAccount_ListView;CheckSubject_Nip_ListView;CheckSubject_Regon_ListView";
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

                ErrorService.ShowError(this, selectedItemName, "Pole może posiadać tylko 1 numer.");

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