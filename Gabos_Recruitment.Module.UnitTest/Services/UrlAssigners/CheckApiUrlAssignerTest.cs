using FluentAssertions;
using Gabos_Recruitment.Module.BusinessObjects.Specification;
using Gabos_Recruitment.Module.Helpers;
using Gabos_Recruitment.Module.Services.ApiUrlAssigners;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static Gabos_Recruitment.Module.Enums.SpecificationEnums;

namespace Gabos_Recruitment.Module.UnitTest.Services.UrlAssigners
{
    public class CheckApiUrlAssignerTest
    {
        private readonly CheckApiUrlAssigner checkApiUrlAssigner = CheckApiUrlAssigner.GetInstance();

        [Fact]
        public void ShouldGetUrlToCheckByNip()
        {
            //Assert
            var selectedSpecificationData = new CheckSpecificationData { BankAccount = "10521327305908100261101820", SelectedSpecification = "3245174504" };

            //Act
            var actionUrl = checkApiUrlAssigner.GetActionUrl(selectedSpecificationData, (int)CheckBySpecification.Nip);

            //Assert
            actionUrl.Should().NotBeNullOrEmpty();
            actionUrl.Should().Contain("3245174504");
            actionUrl.Should().Contain("10521327305908100261101820");
            actionUrl.Should().Contain("nip");
        }

        [Fact]
        public void ShouldGetUrlToCheckByRegon()
        {
            //Assert
            var selectedSpecificationData = new CheckSpecificationData { BankAccount = "10521327305908100261101820", SelectedSpecification = "79156739856513" };

            //Act
            var actionUrl = checkApiUrlAssigner.GetActionUrl(selectedSpecificationData, (int)CheckBySpecification.Regon);

            //Assert
            actionUrl.Should().NotBeNullOrEmpty();
            actionUrl.Should().Contain("79156739856513");
            actionUrl.Should().Contain("10521327305908100261101820");
            actionUrl.Should().Contain("regon");
        }
    }
}
