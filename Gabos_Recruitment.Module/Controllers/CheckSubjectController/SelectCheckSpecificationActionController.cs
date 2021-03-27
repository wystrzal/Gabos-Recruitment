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
using Gabos_Recruitment.Module.BusinessObjects;
using Gabos_Recruitment.Module.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using static Gabos_Recruitment.Module.Enums.SpecificationEnums;

namespace Gabos_Recruitment.Module.Controllers.CheckSubjectController
{
    public partial class SelectCheckSpecificationActionController : ViewController
    {
        public SingleChoiceAction SelectCheckSpecificationAction { get; private set; }
        public CheckBySpecification SelectedSpecification { get; private set; }

        private const string checkSubjectActionVisibility = "CheckSubjectActionVisibility";
        private IList<PropertyEditor> propertyEditors;
        private CheckSubjectActionController checkSubjectActionController;

        public SelectCheckSpecificationActionController()
        {
            InitializeComponent();
            TargetObjectType = typeof(CheckSubject);
            TargetViewType = ViewType.DetailView;

            ConfigureSelectCheckSpecificationAction();
        }

        private void ConfigureSelectCheckSpecificationAction()
        {
            SelectCheckSpecificationAction = new SingleChoiceAction(this, "SelectCheckSpecificationAction", PredefinedCategory.View)
            {
                Caption = "Check By",
                ItemType = SingleChoiceActionItemType.ItemIsOperation,
                SelectionDependencyType = SelectionDependencyType.Independent
            };

            AddChoiceActionItems(SelectCheckSpecificationAction);
        }

        private void AddChoiceActionItems(SingleChoiceAction ChooseCheckSpecification)
        {
            foreach (CheckBySpecification item in Enum.GetValues(typeof(CheckBySpecification)))
            {
                ChooseCheckSpecification.Items.Add(new ChoiceActionItem(item.ToString().ChangeUnderlineToSpace(), item));
            }
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            SelectCheckSpecificationAction.Execute += SelectCheckSpecificationAction_Execute;

            checkSubjectActionController = Frame.GetController<CheckSubjectActionController>();
            propertyEditors = ((DetailView)View).GetItems<PropertyEditor>();

            HideAllCheckSpecifications();
        }

        private void SelectCheckSpecificationAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            ClearCheckSpecifications();
            HideAllCheckSpecifications();

            var selectedData = e.SelectedChoiceActionItem.Data;
            SelectedSpecification = (CheckBySpecification)selectedData;

            var selectedCheckSpecification = selectedData.ToString().RemoveUnderline();
            ChangeVisibilityOfSelectedCheckSpecification($"{selectedCheckSpecification}", true);
        }

        private void ClearCheckSpecifications()
        {
            IObjectSpace objectSpace = View.ObjectSpace;
            View.CurrentObject = objectSpace.CreateObject<CheckSubject>();
            var currentView = View.CurrentObject as CheckSubject;

            //Create new instances of specification lists for the current view.
            foreach (PropertyInfo prop in currentView.GetType().GetProperties())
            {
                var type = prop.PropertyType;
                if (type.IsGenericType)
                {
                    prop.SetValue(currentView, Activator.CreateInstance(type));
                }
            }
        }

        private void HideAllCheckSpecifications()
        {
            ChangeCheckSubjectActionVisibility(false);

            foreach (var editor in propertyEditors)
            {
                foreach (CheckBySpecification item in Enum.GetValues(typeof(CheckBySpecification)))
                {
                    if (editor.PropertyName == item.ToString().RemoveUnderline())
                    {
                        ChangeVisibilityOfSelectedCheckSpecification(editor.PropertyName, false);
                    }
                }
            }
        }

        private void ChangeVisibilityOfSelectedCheckSpecification(string propertyName, bool isVisible)
        {
            PropertyEditor selectedPropertyEditor = propertyEditors.Where(x => x.PropertyName == propertyName).First();

            var newObjectViewController = ((ListPropertyEditor)selectedPropertyEditor).Frame.GetController<NewObjectViewController>();
            newObjectViewController.NewObjectAction.Enabled.Clear();

            if (isVisible)
            {
                ((IAppearanceVisibility)selectedPropertyEditor).Visibility = ViewItemVisibility.Show;
                ChangeCheckSubjectActionVisibility(isVisible);
            }
            else
            {
                ((IAppearanceVisibility)selectedPropertyEditor).Visibility = ViewItemVisibility.Hide;
            }
        }

        private void ChangeCheckSubjectActionVisibility(bool isVisible)
        {
            if (isVisible)
            {
                checkSubjectActionController.Active.RemoveItem(checkSubjectActionVisibility);
            }
            else
            {
                checkSubjectActionController.Active.SetItemValue(checkSubjectActionVisibility, isVisible);
            }
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            SelectCheckSpecificationAction.Execute -= SelectCheckSpecificationAction_Execute;
            ChangeCheckSubjectActionVisibility(true);
            base.OnDeactivated();
        }
    }
}
