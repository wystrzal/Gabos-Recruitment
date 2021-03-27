using FluentAssertions;
using Gabos_Recruitment.Module.BusinessObjects.ApiModels.Error;
using Gabos_Recruitment.Module.BusinessObjects.ApiModels.Responses;
using Gabos_Recruitment.Module.BusinessObjects.Specification;
using Gabos_Recruitment.Module.Helpers;
using Gabos_Recruitment.Module.Services.CustomHttpClients;
using Gabos_Recruitment.Module.Services.SubjectService;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Gabos_Recruitment.Module.Enums.SpecificationEnums;

namespace Gabos_Recruitment.Module.UnitTest.Services
{
    public class SubjectServiceTest
    {
        private readonly Mock<ICustomHttpClient> customHttpClient;
        private readonly SubjectService subjectService;


        public SubjectServiceTest()
        {
            customHttpClient = new Mock<ICustomHttpClient>();
            subjectService = new SubjectService(customHttpClient.Object);
        }

        [Fact]
        public async Task ShouldReturnApiException()
        {
            //Assert
            var response = "{ 'result': null}";
            var selectedSpecificationList = new List<BankAccount> { new BankAccount() };
            customHttpClient.Setup(x => x.GetStringAsync(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>())).Returns(Task.FromResult(response));

            //Act
            var action = await subjectService.SearchSubject(selectedSpecificationList, SearchBySpecification.Bank_Account, DateTime.Today);

            //Assert
            action.Should().BeOfType(typeof(ApiException));
        }

        [Fact]
        public async Task ShouldReturnEntityListResponse()
        {
            //Assert
            var response = "{ 'result': { 'subject': null } }";
            var selectedSpecificationList = new List<BankAccount> { new BankAccount() { Number = "49635393242218223941769740" } };
            customHttpClient.Setup(x => x.GetStringAsync(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>())).Returns(Task.FromResult(response));

            //Act
            var action = await subjectService.SearchSubject(selectedSpecificationList, SearchBySpecification.Bank_Account, DateTime.Today);

            //Assert
            action.Should().BeOfType(typeof(EntityListResponse));
        }

        [Fact]
        public async Task ShouldReturnEntryListResponse()
        {
            //Assert
            var response = "{ 'result': { 'subject': null } }";
            var selectedSpecificationList = new List<BankAccount> { new BankAccount(), new BankAccount() };
            customHttpClient.Setup(x => x.GetStringAsync(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>())).Returns(Task.FromResult(response));

            //Act
            var action = await subjectService.SearchSubject(selectedSpecificationList, SearchBySpecification.Bank_Account, DateTime.Today);

            //Assert
            action.Should().BeOfType(typeof(EntryListResponse));
        }

        [Fact]
        public async Task ShouldReturnEntityResponse()
        {
            //Assert
            var response = "{ 'result': { 'subject': null } }";
            var selectedSpecificationList = new List<Nip> { new Nip() };
            customHttpClient.Setup(x => x.GetStringAsync(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>())).Returns(Task.FromResult(response));

            //Act
            var action = await subjectService.SearchSubject(selectedSpecificationList, SearchBySpecification.Bank_Account, DateTime.Today);

            //Assert
            action.Should().BeOfType(typeof(EntityResponse));
        }

        [Fact]
        public async Task ShouldReturnEntityCheckResponse()
        {
            //Assert
            var response = "{ 'result': { 'requestId': '123' } }";
            var selectedSpecificationObject = new CheckSpecificationData
            {
                SelectedSpecification = "3245174504",
                BankAccount = "49635393242218223941769740"
            };
            customHttpClient.Setup(x => x.GetStringAsync(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>())).Returns(Task.FromResult(response));

            //Act
            var action = await subjectService.CheckSubject(selectedSpecificationObject, CheckBySpecification.Nip, DateTime.Today);

            //Assert
            action.Should().BeOfType(typeof(EntityCheckResponse));
        }
    }
}
