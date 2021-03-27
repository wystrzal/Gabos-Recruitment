using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.SystemModule;
using Gabos_Recruitment.Module.BusinessObjects.Specification;
using System;
using System.Linq;
using System.Reflection;

namespace Gabos_Recruitment.Module.Controllers.SearchSubjectControllers
{
    public partial class EditNestedListController : ViewController
    {
        private ListViewProcessCurrentObjectController listViewProcessCurrentObjectController;
        private object oldObject;

        public EditNestedListController()
        {
            InitializeComponent();
            TargetViewNesting = Nesting.Nested;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            listViewProcessCurrentObjectController = Frame.GetController<ListViewProcessCurrentObjectController>();

            listViewProcessCurrentObjectController.ProcessCurrentObjectAction.Executing += ProcessCurrentObjectAction_Executing;
        }

        private void ProcessCurrentObjectAction_Executing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var selectedObject = ((SimpleAction)sender).SelectionContext.SelectedObjects[0];
            oldObject = selectedObject;

            ((ListView)View).CollectionSource.Remove(selectedObject);

            var svp = CreateShowViewParameters(selectedObject, out DialogController dialogController);

            Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(Application.MainWindow, null));

            dialogController.AcceptAction.Execute += AcceptAction_Execute;
            dialogController.CancelAction.Execute += CancelAction_Execute;
            e.Cancel = true;
        }

        private ShowViewParameters CreateShowViewParameters(object selectedObject, out DialogController dialogController)
        {
            var objectSpace = Application.CreateObjectSpace(selectedObject.GetType());
            var newObject = Activator.CreateInstance(selectedObject.GetType());

            //Copy properties from selectedObject to newObject.
            foreach (PropertyInfo property in selectedObject.GetType().GetProperties().Where(p => p.CanWrite))
            {
                property.SetValue(newObject, property.GetValue(selectedObject, null), null);
            }
            selectedObject.GetType().GetProperty("Oid").SetValue(selectedObject, Guid.NewGuid());

            var view = Application.CreateDetailView(objectSpace, newObject);

            var svp = new ShowViewParameters(view)
            {
                TargetWindow = TargetWindow.NewModalWindow,
                Context = TemplateContext.PopupWindow
            };
            dialogController = Application.CreateController<DialogController>();
            svp.Controllers.Add(dialogController);

            return svp;
        }

        private void CancelAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ((ListView)View).CollectionSource.Add(oldObject);
        }

        private void AcceptAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ((ListView)View).CollectionSource.Add(e.SelectedObjects[0]);
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }

        protected override void OnDeactivated()
        {
            listViewProcessCurrentObjectController.ProcessCurrentObjectAction.Executing -= ProcessCurrentObjectAction_Executing;
            base.OnDeactivated();
        }
    }
}