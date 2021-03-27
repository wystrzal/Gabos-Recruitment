using Gabos_Recruitment.Module.BusinessObjects.Specification;
using Gabos_Recruitment.Module.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Gabos_Recruitment.Module.Enums.SpecificationEnums;

namespace Gabos_Recruitment.Module.Services.ApiUrlAssigners
{
    public class SearchApiUrlAssigner : ApiUrlAssigner
    {
        private SearchApiUrlAssigner() { }
        private static SearchApiUrlAssigner instance;
        public static SearchApiUrlAssigner GetInstance()
        {
            if (instance == null)
            {
                instance = new SearchApiUrlAssigner();
            }

            return instance;
        }

        public override string GetActionUrl(object selectedSpecificationData, int specification)
        {
            var selectedSpecificationList = (IList)selectedSpecificationData;
            var selectedSpecification = (SearchBySpecification)specification;

            var stringBuilder = new StringBuilder();

            string specificationName = selectedSpecification.ToString().ChangeUnderlineToDash().ToLower();

            stringBuilder.Append(((BaseSpecification)selectedSpecificationList[0]).Number);

            for (int i = 1; i < selectedSpecificationList.Count; i++)
            {
                stringBuilder.Append($",{((BaseSpecification)selectedSpecificationList[i]).Number}");
            }

            if (selectedSpecificationList.Count > 1)
            {
                specificationName += "s";
            }

            var specificationValue = stringBuilder.ToString();

            return $"{BaseUrl}search/{specificationName}/{specificationValue}";
        }
    }
}
