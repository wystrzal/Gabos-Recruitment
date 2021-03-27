using FluentAssertions;
using Gabos_Recruitment.Module.BusinessObjects.Specification;
using Gabos_Recruitment.Module.Services.ApiUrlAssigners;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static Gabos_Recruitment.Module.Enums.SpecificationEnums;

namespace Gabos_Recruitment.Module.UnitTest.Services.UrlAssigners
{
    public class SearchApiUrlAssignerTest
    {
        private readonly SearchApiUrlAssigner searchApiUrlAssigner = SearchApiUrlAssigner.GetInstance();

        [Fact]
        public void ShouldGetUrlToCheckByBankAccount()
        {
            //Assert
            var selectedSpecificationData = new List<BankAccount> { new BankAccount { Number = "70506405335016096312945164" } };

            //Act
            var actionUrl = searchApiUrlAssigner.GetActionUrl(selectedSpecificationData, (int)SearchBySpecification.Bank_Account);

            //Assert
            actionUrl.Should().NotBeNullOrEmpty();
            actionUrl.Should().Contain("70506405335016096312945164");
            actionUrl.Should().Contain("bank-account");
        }

        [Fact]
        public void ShouldGetUrlToCheckByBankAccounts()
        {
            //Assert
            var selectedSpecificationData = new List<BankAccount>
            {
                new BankAccount { Number = "70506405335016096312945164" },
                new BankAccount { Number = "70506405335016096312945164" }
            };

            //Act
            var actionUrl = searchApiUrlAssigner.GetActionUrl(selectedSpecificationData, (int)SearchBySpecification.Bank_Account);

            //Assert
            actionUrl.Should().NotBeNullOrEmpty();
            actionUrl.Should().Contain("70506405335016096312945164,70506405335016096312945164");
            actionUrl.Should().Contain("bank-accounts");
        }

        [Fact]
        public void ShouldGetUrlToCheckByNip()
        {
            //Assert
            var selectedSpecificationData = new List<Nip> { new Nip { Number = "3245174504" } };

            //Act
            var actionUrl = searchApiUrlAssigner.GetActionUrl(selectedSpecificationData, (int)SearchBySpecification.Nip);

            //Assert
            actionUrl.Should().NotBeNullOrEmpty();
            actionUrl.Should().Contain("3245174504");
            actionUrl.Should().Contain("nip");
        }

        [Fact]
        public void ShouldGetUrlToCheckByNips()
        {
            //Assert
            var selectedSpecificationData = new List<Nip> { new Nip { Number = "3245174504" }, new Nip { Number = "3245174504" } };

            //Act
            var actionUrl = searchApiUrlAssigner.GetActionUrl(selectedSpecificationData, (int)SearchBySpecification.Nip);

            //Assert
            actionUrl.Should().NotBeNullOrEmpty();
            actionUrl.Should().Contain("3245174504,3245174504");
            actionUrl.Should().Contain("nips");
        }

        [Fact]
        public void ShouldGetUrlToCheckByRegon()
        {
            //Assert
            var selectedSpecificationData = new List<Regon> { new Regon { Number = "79156739856513" } };

            //Act
            var actionUrl = searchApiUrlAssigner.GetActionUrl(selectedSpecificationData, (int)SearchBySpecification.Regon);

            //Assert
            actionUrl.Should().NotBeNullOrEmpty();
            actionUrl.Should().Contain("79156739856513");
            actionUrl.Should().Contain("regon");
        }

        [Fact]
        public void ShouldGetUrlToCheckByRegons()
        {
            //Assert
            var selectedSpecificationData = new List<Regon> { new Regon { Number = "79156739856513" }, new Regon { Number = "79156739856513" } };

            //Act
            var actionUrl = searchApiUrlAssigner.GetActionUrl(selectedSpecificationData, (int)SearchBySpecification.Regon);

            //Assert
            actionUrl.Should().NotBeNullOrEmpty();
            actionUrl.Should().Contain("79156739856513,79156739856513");
            actionUrl.Should().Contain("regons");
        }
    }
}