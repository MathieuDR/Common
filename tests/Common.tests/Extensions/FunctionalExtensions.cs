using System;
using MathieuDR.Common.Extensions;
using NSubstitute;
using Xunit;

namespace MathieuDR.Common.tests.Extensions {
    public class FunctionalExtensions {
        [Fact]
        public void ApplyShouldApply() {
            //Arrange
            var act = Substitute.For<Action<string>>();
            var param = "";
            
            //Act
            param.Apply(act);

            //Assert
            act.Received(1).Invoke(Arg.Any<string>());
        }

        [Fact]
        public void ApplyPassesParameter() {
            //Arrange
            var act = Substitute.For<Action<string>>();
            var param = "test";
            
            //Act
            param.Apply(act);

            //Assert
            act.Received(1).Invoke(Arg.Is("test"));
        }
    }
}
