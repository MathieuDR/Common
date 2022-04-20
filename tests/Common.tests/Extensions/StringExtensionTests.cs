using System;
using FluentAssertions;
using MathieuDR.Common.Extensions;
using Xunit;

namespace MathieuDR.Common.tests.Extensions {
    public class StringExtensionTests {
        [Fact]
        public void PrependShouldPrepend() {
            //Arrange
            var @base = "Text";
            var toPrepend = "my";

            //Act
            var result = @base.Prepend(toPrepend);

            //Assert
            result.Should().Be("myText");
        }
        
        [Fact]
        public void PrependShouldPrependFromEmptyBase() {
            //Arrange
            var @base = "";
            var toPrepend = "my";

            //Act
            var result = @base.Prepend(toPrepend);

            //Assert
            result.Should().Be("my");
        }
        
        [Fact]
        public void PrependShouldThrowException() {
            //Arrange
            string @base = null;
            var toPrepend = "my";

            //Act
            Action act = () => @base.Prepend(toPrepend);

            //Assert
            act.Should().Throw<NullReferenceException>();
        }
        
        [Fact]
        public void ReverseShouldReverse() {
            //Arrange
            var @base = "Text";

            //Act
            var result = @base.Reverse();

            //Assert
            result.Should().Be("txeT");
        }
        
        [Fact]
        public void ReverseShouldReverseFromEmptyBase() {
            //Arrange
            var @base = "";

            //Act
            var result = @base.Reverse();

            //Assert
            result.Should().Be("");
        }
        
        [Fact]
        public void ReverseShouldThrowException() {
            //Arrange
            string @base = null;

            //Act
            Action act = () => @base.Reverse();

            //Assert
            act.Should().Throw<NullReferenceException>();
        }
    }
}
