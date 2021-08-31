using FluentAssertions;
using Xunit;

namespace Common.tests {
    public class NullValueDictionaryTests {
        [Fact]
        public void CanRetrieveValueInDictionaryWithBrackets() {
            //Arrange
            var dict = new NullValueDictionary<string, string>();
            dict.Add("myVar", "myoutput");

            //Act
            var value = dict["myVar"];
            
            //Assert
            value.Should().Be("myoutput");
        }
        
        [Fact]
        public void CanRetrieveValueInDictionaryWithTryGetMethod() {
            //Arrange
            var dict = new NullValueDictionary<string, string>();
            dict.Add("myVar", "myoutput");

            //Act
            var result = dict.TryGetValue("myVar", out var value);
            
            //Assert
            value.Should().Be("myoutput");
            result.Should().BeTrue();
        }
        
        [Fact]
        public void CanRetrieveValueInFilledDictionaryWithBrackets() {
            //Arrange
            var dict = new NullValueDictionary<string, string>();
            dict.Add("myVar", "myoutput");
            dict.Add("myVar2", "sast");

            //Act
            var value = dict["myVar"];
            
            //Assert
            value.Should().Be("myoutput");
        }
        
        [Fact]
        public void CanRetrieveValueInFilledDictionaryWithTryGetMethod() {
            //Arrange
            var dict = new NullValueDictionary<string, string>();
            dict.Add("myVar", "myoutput");
            dict.Add("myVar2", "arstarst");
            
            //Act
            var result = dict.TryGetValue("myVar", out var value);
            
            //Assert
            value.Should().Be("myoutput");
            result.Should().BeTrue();
        }
        
        [Fact]
        public void RetrieveNullValueInDictionaryWithBrackets() {
            //Arrange
            var dict = new NullValueDictionary<string, string>();

            //Act
            var value = dict["myVar"];
            
            //Assert
            value.Should().BeNull();
        }
        
        [Fact]
        public void RetrieveNullValueInDictionaryWithTryGetMethod() {
            //Arrange
            var dict = new NullValueDictionary<string, string>();

            //Act
            var result = dict.TryGetValue("myVar", out var value);
            
            //Assert
            value.Should().BeNull();
            result.Should().BeFalse();
        }
        
        [Fact]
        public void RetrieveNullValueInFilledDictionaryWithBrackets() {
            //Arrange
            var dict = new NullValueDictionary<string, string>();
            dict.Add("myVar2", "sast");

            //Act
            var value = dict["myVar"];
            
            //Assert
            value.Should().BeNull();
        }
        
        [Fact]
        public void RetrieveNullValueInFilledDictionaryWithTryGetMethod() {
            //Arrange
            var dict = new NullValueDictionary<string, string>();
            dict.Add("myVar2", "arstarst");
            
            //Act
            var result = dict.TryGetValue("myVar", out var value);
            
            //Assert
            value.Should().BeNull();
            result.Should().BeFalse();
        }
        
        
        [Fact]
        public void RetrieveDefaultValueInDictionaryWithBrackets() {
            //Arrange
            var dict = new NullValueDictionary<string, int>();

            //Act
            var value = dict["myVar"];
            
            //Assert
            value.Should().Be(0);
        }
        
        [Fact]
        public void RetrieveDefaultValueInDictionaryWithTryGetMethod() {
            //Arrange
            var dict = new NullValueDictionary<string, int>();

            //Act
            var result = dict.TryGetValue("myVar", out var value);
            
            //Assert
            value.Should().Be(0);
            result.Should().BeFalse();
        }
        
        [Fact]
        public void RetrieveDefaultValueInFilledDictionaryWithBrackets() {
            //Arrange
            var dict = new NullValueDictionary<string, int>();
            dict.Add("myVar2", 123);

            //Act
            var value = dict["myVar"];
            
            //Assert
            value.Should().Be(0);
        }
        
        [Fact]
        public void RetrieveDefaultValueInFilledDictionaryWithTryGetMethod() {
            //Arrange
            var dict = new NullValueDictionary<string, int>();
            dict.Add("myVar2", 12312);
            
            //Act
            var result = dict.TryGetValue("myVar", out var value);
            
            //Assert
            value.Should().Be(0);
            result.Should().BeFalse();
        }
    }
}
