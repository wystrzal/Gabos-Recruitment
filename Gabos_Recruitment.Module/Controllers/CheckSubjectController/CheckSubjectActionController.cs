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
using Gabos_Recruitment.Module.BusinessObjects.Specification;
using Gabos_Recruitment.Module.Extensions;
using Gabos_Recruitment.Module.Helpers;
using Gabos_Recruitment.Module.Services;
using Gabos_Recruitment.Module.Services.SubjectService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gabos_Recruitment.Module.Controllers.CheckSubjectController
{
    public partial class CheckSubjectActionController : ViewController
    {
        public SimpleAction CheckSubjectAction { get; private set; }

        private SelectCheckSpecificationActionController selectCheckSpecificationActionController;

        private ISubjectService subjectService;

        public CheckSubjectActionController()
        {
            InitializeComponent();
            TargetViewType = ViewType.DetailView;
            TargetObjectType = typeof(CheckSubject);

            CheckSubjectAction = new SimpleAction(this, "CheckSubjectAction", PredefinedCategory.View)
            {
                Caption = "Check",
                ImageName = "Action_Search"
            };
        }

        private async void CheckSubjectAction_ExecuteAsync(Object sender, SimpleActionExecuteEventArgs e)
        {
            var selectedSpecificationEnum = selectCheckSpecificationActionController.SelectedSpecification;
            var selectedSpecificationName = selectedSpecificationEnum.ToString().RemoveUnderline();

            CheckSubject currentObject = ((DetailView)View).CurrentObject as CheckSubject;

            if (currentObject.Date == new DateTime())
            {
                currentObject.Date = DateTime.Now;
            }

            var selectedSpecificationData = currentObject.GetType().GetProperty($"{selectedSpecificationName}").GetValue(currentObject) as IList;

            if (!DataIsValid(currentObject, selectedSpecificationData))
            {
                return;
            }

            subjectService = new SubjectService();

            var selectedSpecificationObject = new CheckSpecificationData
            {
                SelectedSpecification = ((BaseSpecification)selectedSpecificationData[0]).Number,
                BankAccount = currentObject.BankAccount[0].Number
            };

            var response = await subjectService.CheckSubject(selectedSpecificationObject, selectedSpecificationEnum, currentObject.Date);

            ShowNewDetailView(response);
        }

        private bool DataIsValid(CheckSubject currentObject, IList selectedSpecificationData)
        {
            if (selectedSpecificationData == null || selectedSpecificationData.Count == 0)
            {
                ErrorService.ShowError(this, selectedSpecificationData.GetType().GetGenericArguments().First().Name, "Pole nie może być puste");
                return false;
            }
            if (currentObject.BankAccount == null || currentObject.BankAccount.Count == 0)
            {
                ErrorService.ShowError(this, currentObject.BankAccount.GetType().GetGenericArguments().First().Name, "Pole nie może być puste");
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
            CheckSubjectAction.Execute += CheckSubjectAction_ExecuteAsync;
            selectCheckSpecificationActionController = Frame.GetController<SelectCheckSpecificationActionController>();
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            CheckSubjectAction.Execute -= CheckSubjectAction_ExecuteAsync;
            base.OnDeactivated();
        }
    }
}
