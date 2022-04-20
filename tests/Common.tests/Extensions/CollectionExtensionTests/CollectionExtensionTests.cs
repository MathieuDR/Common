using System;
using System.Collections.Generic;

namespace MathieuDR.Common.tests.Extensions.CollectionExtensionTests {
    public partial class CollectionExtensionTests {
        private List<int> GetList(int count) {
            var r = new Random();
            var result = new List<int>();
            
            for (int i = 0; i < count; i++) {
                result.Add(r.Next());
            }

            return result;
        }
    }
}
