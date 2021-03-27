using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Gabos_Recruitment.Module.Enums.SpecificationEnums;

namespace Gabos_Recruitment.Module.Services.SubjectService
{
    public interface ISubjectService
    {
        Task<object> SearchSubject(IList data, SearchBySpecification searchBySpecification, DateTime date);
        Task<object> CheckSubject(object data, CheckBySpecification checkBySpecification, DateTime date);
    }
}
