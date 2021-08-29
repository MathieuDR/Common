using System.Collections.Generic;

namespace Common.Helpers.Internal {
    internal class EnumeratorWrapper<T> {
        private Enumeration _currentEnumeration;

        private int _refs;

        public EnumeratorWrapper(IEnumerable<T> source) {
            SourceEumerable = source;
        }

        private IEnumerable<T> SourceEumerable { get; }

        public bool Get(int pos, out T item) {
            if (_currentEnumeration != null && _currentEnumeration.Position > pos) {
                _currentEnumeration.Source.Dispose();
                _currentEnumeration = null;
            }

            if (_currentEnumeration == null) {
                _currentEnumeration = new Enumeration {Position = -1, Source = SourceEumerable.GetEnumerator(), AtEnd = false};
            }

            item = default;
            if (_currentEnumeration.AtEnd) {
                return false;
            }

            while (_currentEnumeration.Position < pos) {
                _currentEnumeration.AtEnd = !_currentEnumeration.Source.MoveNext();
                _currentEnumeration.Position++;

                if (_currentEnumeration.AtEnd) {
                    return false;
                }
            }

            item = _currentEnumeration.Source.Current;

            return true;
        }

        // needed for dispose semantics 
        public void AddRef() {
            _refs++;
        }

        public void RemoveRef() {
            _refs--;
            if (_refs == 0 && _currentEnumeration != null) {
                var copy = _currentEnumeration;
                _currentEnumeration = null;
                copy.Source.Dispose();
            }
        }

        private class Enumeration {
            public IEnumerator<T> Source { get; set; }
            public int Position { get; set; }
            public bool AtEnd { get; set; }
        }
    }
}
