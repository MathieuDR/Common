using System;
using FluentResults;
using MathieuDR.Common.Extensions;
using Xunit;

namespace MathieuDR.Common.tests.Extensions {
    public class FluentResultsCombinerExtensionTests {

        private const string ReasonOne = "Reason one"; 
        private const string ReasonTwo = "Reason two";
        private const string Separator = ";";
   
        
        [Fact]
        public void CombineWithSuccessHasCorrectString() {
            var result = Result.Ok().WithSuccess(ReasonOne);
            var combinedResult = result.CombineMessage();
            Assert.Equal(ReasonOne, combinedResult);
        }
        
        [Fact]
        public void CombineWithSuccessAndSeparatorHasCorrectString() {
            var result = Result.Ok().WithSuccess(ReasonOne);
            var combinedResult = result.CombineMessage(Separator);
            Assert.Equal(ReasonOne, combinedResult);
        }
        
        [Fact]
        public void CombineWithErrorHasCorrectString() {
            var result = Result.Fail(ReasonOne);
            var combinedResult = result.CombineMessage();
            Assert.Equal(ReasonOne, combinedResult);
        }
        
        [Fact]
        public void CombineWithErrorAndSeparatorHasCorrectString() {
            var result = Result.Fail(ReasonOne);
            var combinedResult = result.CombineMessage(Separator);
            Assert.Equal(ReasonOne, combinedResult);
        }
        
        [Fact]
        public void CombineWithSuccessesHasCorrectString() {
            var result = Result.Ok().WithSuccess(ReasonOne).WithSuccess(ReasonTwo);
            var combinedResult = result.CombineMessage();
            Assert.Equal($"{ReasonOne}{Environment.NewLine}{ReasonTwo}", combinedResult);
        }
        
        [Fact]
        public void CombineWithSuccessesAndSeparatorHasCorrectString() {
            var result = Result.Ok().WithSuccess(ReasonOne).WithSuccess(ReasonTwo);
            var combinedResult = result.CombineMessage(Separator);
            Assert.Equal($"{ReasonOne}{Separator}{ReasonTwo}", combinedResult);
        }
        
        [Fact]
        public void CombineWithErrorsHasCorrectString() {
            var result = Result.Fail(ReasonOne).WithError(ReasonTwo);
            var combinedResult = result.CombineMessage();
            Assert.Equal($"{ReasonOne}{Environment.NewLine}{ReasonTwo}", combinedResult);
        }
        
        [Fact]
        public void CombineWithErrorsAndSeparatorHasCorrectString() {
            var result = Result.Fail(ReasonOne).WithError(ReasonTwo);
            var combinedResult = result.CombineMessage(Separator);
            Assert.Equal($"{ReasonOne}{Separator}{ReasonTwo}", combinedResult);
        }
        
        [Fact]
        public void CombineWithSuccessesAndErrorsHasCorrectString() {
            var result = Result.Ok().WithSuccess(ReasonOne).WithError(ReasonTwo);
            var combinedResult = result.CombineMessage();
            Assert.Equal($"{ReasonOne}{Environment.NewLine}{ReasonTwo}", combinedResult);
        }
        
        [Fact]
        public void CombineWithSuccessesAndErrorsAndSeparatorHasCorrectString() {
            var result = Result.Ok().WithSuccess(ReasonOne).WithError(ReasonTwo);
            var combinedResult = result.CombineMessage(Separator);
            Assert.Equal($"{ReasonOne}{Separator}{ReasonTwo}", combinedResult);
        }
        
        [Fact]
        public void CombineWithErrorsAndSuccessesHasCorrectString() {
            var result = Result.Fail(ReasonOne).WithSuccess(ReasonTwo);
            var combinedResult = result.CombineMessage();
            Assert.Equal($"{ReasonOne}{Environment.NewLine}{ReasonTwo}", combinedResult);
        }
        
        [Fact]
        public void CombineWithErrorsAndSuccessesAndSeparatorHasCorrectString() {
            var result = Result.Fail(ReasonOne).WithSuccess(ReasonTwo);
            var combinedResult = result.CombineMessage(Separator);
            Assert.Equal($"{ReasonOne}{Separator}{ReasonTwo}", combinedResult);
        }
        
    }
}