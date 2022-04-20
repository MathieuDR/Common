using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MathieuDR.Common {
    public class DefaultDictionary<TKey, TValue> : Dictionary<TKey, TValue> {
        public DefaultDictionary() { }
        public DefaultDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary) { }
        public DefaultDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey>? comparer) : base(dictionary, comparer) { }
        public DefaultDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection) : base(collection) { }
        public DefaultDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey>? comparer) : base(collection, comparer) { }
        public DefaultDictionary(IEqualityComparer<TKey>? comparer) : base(comparer) { }
        public DefaultDictionary(int capacity) : base(capacity) { }
        public DefaultDictionary(int capacity, IEqualityComparer<TKey>? comparer) : base(capacity, comparer) { }
        protected DefaultDictionary(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public new TValue this[TKey key] => TryGetValue(key, out var value) ? value : default;
    }
}
