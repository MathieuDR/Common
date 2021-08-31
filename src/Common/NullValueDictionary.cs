using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common {
    public class NullValueDictionary<TKey, TValue> : Dictionary<TKey, TValue> {
        public new TValue this[TKey key] => TryGetValue(key, out var value) ? value : default;
    }
}
