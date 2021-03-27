using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Gabos_Recruitment.Module.Extensions;
using FluentAssertions;

namespace Gabos_Recruitment.Module.UnitTest.Extensions
{
    public class StringExtensionsTests
    {
        private const string testString = "test_test";

        [Fact]
        public void ShouldChangeUnderlineToSpace()
        {
            testString.ChangeUnderlineToSpace().Should().Be("test test");
        }

        [Fact]
        public void ShouldChangeUnderlineToDash()
        {
            testString.ChangeUnderlineToDash().Should().Be("test-test");
        }

        [Fact]
        public void ShouldRemoveUnderline()
        {
            testString.RemoveUnderline().Should().Be("testtest");
        }
    }
}
