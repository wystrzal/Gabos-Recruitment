using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gabos_Recruitment.Module.BusinessObjects.ApiModels;
using Gabos_Recruitment.Module.BusinessObjects.ApiModels.Error;
using Gabos_Recruitment.Module.BusinessObjects.ApiModels.Responses;
using Gabos_Recruitment.Module.BusinessObjects.Specification;
using Gabos_Recruitment.Module.Services.ApiUrlAssigners;
using Gabos_Recruitment.Module.Services.CustomHttpClients;
using Newtonsoft.Json;
using static Gabos_Recruitment.Module.Enums.SpecificationEnums;

namespace Gabos_Recruitment.Module.Services.SubjectService
{
    public class SubjectService : ISubjectService
    {
        private ApiUrlAssigner apiUrlAssigner;
        private readonly ICustomHttpClient customHttpClient;

        public SubjectService()
        {
            customHttpClient = new CustomHttpClient();
        }

        public SubjectService(ICustomHttpClient customHttpClient)
        {
            this.customHttpClient = customHttpClient;
        }

        public async Task<object> SearchSubject(IList selectedSpecificationList, SearchBySpecification searchBySpecification, DateTime date)
        {
            apiUrlAssigner = SearchApiUrlAssigner.GetInstance();

            var url = apiUrlAssigner.GetActionUrl(selectedSpecificationList, (int)searchBySpecification);
            var queryParams = new Dictionary<string, string> { { "date", date.ToString("yyyy-MM-dd") } };
            var stringResponse = await customHttpClient.GetStringAsync(url, queryParams);

            return DeserializeSearchSubjectObject(selectedSpecificationList, stringResponse);
        }

        private object DeserializeSearchSubjectObject(IList selectedSpecificationList, string stringResponse)
        {
            object deserializedResponse;

            if (selectedSpecificationList.Count > 1)
            {
                deserializedResponse = JsonConvert.DeserializeObject<EntryListResponse>(stringResponse);
            }
            else if (selectedSpecificationList[0].GetType() == typeof(BankAccount))
            {
                deserializedResponse = JsonConvert.DeserializeObject<EntityListResponse>(stringResponse);
            }
            else
            {
                deserializedResponse = JsonConvert.DeserializeObject<EntityResponse>(stringResponse);
            }

            deserializedResponse = ReturnExceptionIfResultIsNull(stringResponse, deserializedResponse);

            if (deserializedResponse.GetType() != typeof(ApiException))
            {
                MapAccountNumbersToBankAccountNumbers(deserializedResponse);
            }

            return deserializedResponse;
        }

        private void MapAccountNumbersToBankAccountNumbers(object response)
        {
            if (response is EntityListResponse entityListResponse)
            {
                MapEntityListResponse(entityListResponse);
            }
            else if (response is EntityResponse entityResponse)
            {
                MapEntityResponse(entityResponse);
            }
            else if (response is EntryListResponse entryListResponse)
            {
                MapEntryListResponse(entryListResponse);
            }
        }

        private void MapEntityListResponse(EntityListResponse entityListResponse)
        {
            if (entityListResponse.Result.Subjects != null && entityListResponse.Result.Subjects.Count != 0)
            {
                entityListResponse.Result.Subjects
                    .ForEach(subject => subject.AccountNumbers
                    .ForEach(accountNumber => subject.BankAccountNumbers.Add(new AccountNumber { Number = accountNumber })));
            }
        }

        private void MapEntityResponse(EntityResponse entityResponse)
        {
            if (entityResponse.Result.Subject != null)
            {
                var bankAccountNumbers = entityResponse.Result.Subject.BankAccountNumbers;

                entityResponse.Result.Subject.AccountNumbers
                    .ForEach(accountNumber => bankAccountNumbers.Add(new AccountNumber { Number = accountNumber }));
            }
        }

        private void MapEntryListResponse(EntryListResponse entryListResponse)
        {
            if (entryListResponse.Result.Entries != null && entryListResponse.Result.Entries.Count != 0)
            {
                entryListResponse.Result.Entries
                    .ForEach(entry =>
                    {
                        if (entry.Subjects != null && entry.Subjects.Count > 0)
                        {
                            entry.Subjects
                            .ForEach(subject => subject.AccountNumbers
                            .ForEach(accountNumber => subject.BankAccountNumbers.Add(new AccountNumber { Number = accountNumber })));
                        }
                    });
            }
        }

        public async Task<object> CheckSubject(object selectedSpecificationData, CheckBySpecification checkBySpecification, DateTime date)
        {
            apiUrlAssigner = CheckApiUrlAssigner.GetInstance();

            var url = apiUrlAssigner.GetActionUrl(selectedSpecificationData, (int)checkBySpecification);
            var queryParams = new Dictionary<string, string> { { "date", date.ToString("yyyy-MM-dd") } };
            var stringResponse = await customHttpClient.GetStringAsync(url, queryParams);

            object deserializedResponse = JsonConvert.DeserializeObject<EntityCheckResponse>(stringResponse);
            deserializedResponse = ReturnExceptionIfResultIsNull(stringResponse, deserializedResponse);

            return deserializedResponse;
        }

        private static object ReturnExceptionIfResultIsNull(string stringResponse, object deserializedResponse)
        {
            if (deserializedResponse.GetType().GetProperty("Result").GetValue(deserializedResponse) == null)
            {
                deserializedResponse = JsonConvert.DeserializeObject<ApiException>(stringResponse);
            }

            return deserializedResponse;
        }
    }
}