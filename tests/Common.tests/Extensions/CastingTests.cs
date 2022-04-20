using System;
using FluentAssertions;
using MathieuDR.Common.Extensions;
using Xunit;

namespace MathieuDR.Common.tests.Extensions {
    public class CastingTests {
        class MyBaseClass {
            
        }

        class MyDerivedClass : MyBaseClass {
            
        }
        
        [Fact]
        public void AsCastReturnsCorrectBaseType() {
            //Arrange
            var derived = new MyDerivedClass();
            
            //Act
            var result = Casting.As<MyBaseClass>(derived);
            
            //Assert
            result.Should().NotBeNull();
        }
        
        [Fact]
        public void AsCastReturnsCorrectDerivedType() {
            //Arrange
            var derivedAsBase = new MyDerivedClass() as MyBaseClass;
            
            //Act
            var result = Casting.As<MyDerivedClass>(derivedAsBase);
            
            //Assert
            result.Should().NotBeNull();
        }
        
        [Fact]
        public void AsCastReturnsNullForDerivedFromBase() {
            //Arrange
            var baseClass = new MyBaseClass();
            
            //Act
            var result = Casting.As<MyDerivedClass>(baseClass);
            
            //Assert
            result.Should().BeNull();
        }
        
        [Fact]
        public void CastReturnsCorrectBaseType() {
            //Arrange
            var derived = new MyDerivedClass();
            
            //Act
            var result = derived.Cast<MyBaseClass>();
            
            //Assert
            result.Should().NotBeNull();
        }
        
        [Fact]
        public void CastReturnsCorrectDerivedType() {
            //Arrange
            var derivedAsBase = new MyDerivedClass() as MyBaseClass;
            
            //Act
            var result = derivedAsBase.Cast<MyDerivedClass>();
            
            //Assert
            result.Should().NotBeNull();
        }
        
        [Fact]
        public void CastThrowsExceptionForDerivedFromBase() {
            //Arrange
            var baseClass = new MyBaseClass();
            
            //Act
            Action act = () => baseClass.Cast<MyDerivedClass>();
            
            //Assert
            act.Should().Throw<InvalidCastException>();
        }
    }
}
