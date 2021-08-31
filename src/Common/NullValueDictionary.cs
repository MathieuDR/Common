using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common {
    public class NullValueDictionary<TKey, TValue> : Dictionary<TKey, TValue> {
        public NullValueDictionary() { }
        public NullValueDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary) { }
        public NullValueDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey>? comparer) : base(dictionary, comparer) { }
        public NullValueDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection) : base(collection) { }
        public NullValueDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey>? comparer) : base(collection, comparer) { }
        public NullValueDictionary(IEqualityComparer<TKey>? comparer) : base(comparer) { }
        public NullValueDictionary(int capacity) : base(capacity) { }
        public NullValueDictionary(int capacity, IEqualityComparer<TKey>? comparer) : base(capacity, comparer) { }
        protected NullValueDictionary(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public new TValue this[TKey key] => TryGetValue(key, out var value) ? value : default;
    }
}
