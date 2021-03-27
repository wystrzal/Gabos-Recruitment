using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using Gabos_Recruitment.Module.BusinessObjects;
using Gabos_Recruitment.Module.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Gabos_Recruitment.Module.Enums.SpecificationEnums;

namespace Gabos_Recruitment.Module.Controllers.SearchSubjectControllers
{
    public partial class SelectSearchSpecificationActionController : ViewController
    {
        public SingleChoiceAction SelectSearchSpecificationAction { get; private set; }
        public SearchBySpecification SelectedSpecification { get; private set; }

        private const string searchSubjectActionVisibility = "SearchSubjectActionVisibility";
        private IList<PropertyEditor> propertyEditors;
        private SearchSubjectActionController searchSubjectActionController;

        public SelectSearchSpecificationActionController()
        {
            InitializeComponent();
            TargetObjectType = typeof(SearchSubject);
            TargetViewType = ViewType.DetailView;

            ConfigureSelectSearchSpecificationAction();
        }

        private void ConfigureSelectSearchSpecificationAction()
        {
            SelectSearchSpecificationAction = new SingleChoiceAction(this, "SelectSearchSpecificationAction", PredefinedCategory.View)
            {
                Caption = "Search By",
                ItemType = SingleChoiceActionItemType.ItemIsOperation,
                SelectionDependencyType = SelectionDependencyType.Independent
            };

            AddChoiceActionItems(SelectSearchSpecificationAction);
        }

        private void AddChoiceActionItems(SingleChoiceAction ChooseSearchSpecification)
        {
            foreach (SearchBySpecification item in Enum.GetValues(typeof(SearchBySpecification)))
            {
                ChooseSearchSpecification.Items.Add(new ChoiceActionItem(item.ToString().ChangeUnderlineToSpace(), item));
            }
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            SelectSearchSpecificationAction.Execute += SelectSearchSpecificationAction_Execute;

            searchSubjectActionController = Frame.GetController<SearchSubjectActionController>();
            propertyEditors = ((DetailView)View).GetItems<PropertyEditor>();

            HideAllSearchSpecifications();
        }

        private void SelectSearchSpecificationAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            ClearSearchSpecifications();
            HideAllSearchSpecifications();

            var selectedData = e.SelectedChoiceActionItem.Data;
            SelectedSpecification = (SearchBySpecification)selectedData;

            var selectedSearchSpecification = selectedData.ToString().RemoveUnderline();
            ChangeVisibilityOfSelectedSearchSpecification($"{selectedSearchSpecification}s", true);
        }

        private void ClearSearchSpecifications()
        {
            IObjectSpace objectSpace = View.ObjectSpace;
            View.CurrentObject = objectSpace.CreateObject<SearchSubject>();
            var currentView = View.CurrentObject as SearchSubject;

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

        private void HideAllSearchSpecifications()
        {
            ChangeSearchSubjectActionVisibility(false);

            foreach (var editor in propertyEditors)
            {
                if (editor is ListPropertyEditor)
                {
                    ChangeVisibilityOfSelectedSearchSpecification(editor.PropertyName, false);
                }
            }
        }

        private void ChangeVisibilityOfSelectedSearchSpecification(string propertyName, bool isVisible)
        {
            PropertyEditor selectedPropertyEditor = propertyEditors.Where(x => x.PropertyName == propertyName).First();

            var newObjectViewController = ((ListPropertyEditor)selectedPropertyEditor).Frame.GetController<NewObjectViewController>();
            newObjectViewController.NewObjectAction.Enabled.Clear();

            if (isVisible)
            {
                ((IAppearanceVisibility)selectedPropertyEditor).Visibility = ViewItemVisibility.Show;
                ChangeSearchSubjectActionVisibility(isVisible);
            }
            else
            {
                ((IAppearanceVisibility)selectedPropertyEditor).Visibility = ViewItemVisibility.Hide;
            }
        }

        private void ChangeSearchSubjectActionVisibility(bool isVisible)
        {
            if (isVisible)
            {
                searchSubjectActionController.Active.RemoveItem(searchSubjectActionVisibility);
            }
            else
            {
                searchSubjectActionController.Active.SetItemValue(searchSubjectActionVisibility, isVisible);
            }
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }

        protected override void OnDeactivated()
        {
            SelectSearchSpecificationAction.Execute -= SelectSearchSpecificationAction_Execute;
            ChangeSearchSubjectActionVisibility(true);
            base.OnDeactivated();
        }
    }
}