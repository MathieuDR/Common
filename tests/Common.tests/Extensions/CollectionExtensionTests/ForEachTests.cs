using System;
using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using NSubstitute;
using Xunit;

namespace Common.tests.Extensions.CollectionExtensionTests {
    public partial class CollectionExtensionTests {
        [Fact]
        public void ActionIsCalledSameAmountAsItemsInList() {
            //Arrange
            var list = GetList(10) as IEnumerable<int>;
            var act = Substitute.For<Action<int>>();

            //Act
            list.ForEach(act);

            //Assert
            act.Received(10).Invoke(Arg.Any<int>());
        }
        
        [Fact]
        public void ActionIsCalledWithCorrectParams() {
            //Arrange
            var list = GetList(10).Distinct().ToList() as IEnumerable<int>;
            var act = Substitute.For<Action<int>>();

            //Act
            list.ForEach(act);

            //Assert
            foreach (var integer in list) {
                act.Received(1).Invoke(Arg.Is(integer));
            }
            
        }
    }
}
