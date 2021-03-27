using Gabos_Recruitment.Module.BusinessObjects.Specification;
using Gabos_Recruitment.Module.Extensions;
using Gabos_Recruitment.Module.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using static Gabos_Recruitment.Module.Enums.SpecificationEnums;

namespace Gabos_Recruitment.Module.Services.ApiUrlAssigners
{
    public class CheckApiUrlAssigner: ApiUrlAssigner
    {
        private CheckApiUrlAssigner() { }
        private static CheckApiUrlAssigner instance;
        public static CheckApiUrlAssigner GetInstance()
        {
            if (instance == null)
            {
                instance = new CheckApiUrlAssigner();
            }

            return instance;
        }

        public override string GetActionUrl(object selectedSpecificationData, int specification)
        {
            var selectedSpecificationObject = (CheckSpecificationData)selectedSpecificationData;

            var selectedSpecification = (CheckBySpecification)specification;

            string specificationName = selectedSpecification.ToString().ChangeUnderlineToDash().ToLower();

            return $"{BaseUrl}check/{specificationName}/{selectedSpecificationObject.SelectedSpecification}/bank-account/{selectedSpecificationObject.BankAccount}";
        }
    }
}
