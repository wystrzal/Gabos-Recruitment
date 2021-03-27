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
using Gabos_Recruitment.Module.BusinessObjects.ApiModels;
using Gabos_Recruitment.Module.BusinessObjects.ApiModels.Error;
using Gabos_Recruitment.Module.BusinessObjects.ApiModels.Responses;
using Gabos_Recruitment.Module.BusinessObjects.Specification;
using Gabos_Recruitment.Module.Extensions;
using Gabos_Recruitment.Module.Services;
using Gabos_Recruitment.Module.Services.SubjectService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gabos_Recruitment.Module.Controllers.SearchSubjectControllers
{
    public partial class SearchSubjectActionController : ViewController
    {
        public SimpleAction SearchSubjectAction { get; private set; }

        private SelectSearchSpecificationActionController selectSearchSpecificationActionController;

        private ISubjectService subjectService;

        public SearchSubjectActionController()
        {
            InitializeComponent();
            TargetViewType = ViewType.DetailView;
            TargetObjectType = typeof(SearchSubject);

            SearchSubjectAction = new SimpleAction(this, "SearchSubjectAction", PredefinedCategory.View)
            {
                Caption = "Search",
                ImageName = "Action_Search"
            };
        }

        private async void SearchSubjectAction_ExecuteAsync(Object sender, SimpleActionExecuteEventArgs e)
        {
            var selectedSpecificationEnum = selectSearchSpecificationActionController.SelectedSpecification;
            var selectedSpecificationName = selectedSpecificationEnum.ToString().RemoveUnderline();

            SearchSubject currentObject = ((DetailView)View).CurrentObject as SearchSubject;

            if (currentObject.Date == new DateTime())
            {
                currentObject.Date = DateTime.Now;
            }

            var selectedSpecificationData = currentObject.GetType().GetProperty($"{selectedSpecificationName}s").GetValue(currentObject) as IList;
            if (!ModelIsValid(selectedSpecificationData))
            {
                return;
            }

            subjectService = new SubjectService();
            var response = await subjectService.SearchSubject(selectedSpecificationData, selectedSpecificationEnum, currentObject.Date);
            ShowNewDetailView(response);
        }
        private bool ModelIsValid(IList selectedSpecificationData)
        {
            if (selectedSpecificationData == null || selectedSpecificationData.Count == 0)
            {
                ErrorService.ShowError(this, selectedSpecificationData.GetType().GetGenericArguments().First().Name, "Pole nie może być puste");
                return false;
            }
            return true;
        }

        private void ShowNewDetailView(object response)
        {
            var objectSpace = View.ObjectSpace.CreateNestedObjectSpace();
            var detailView = Application.CreateDetailView(objectSpace, response, true);
            Frame.SetView(detailView);
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            SearchSubjectAction.Execute += SearchSubjectAction_ExecuteAsync;

            selectSearchSpecificationActionController = Frame.GetController<SelectSearchSpecificationActionController>();
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }

        protected override void OnDeactivated()
        {
            SearchSubjectAction.Execute -= SearchSubjectAction_ExecuteAsync;
            base.OnDeactivated();
        }
    }
}