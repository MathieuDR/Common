using System;
using System.Collections;
using System.Collections.Generic;

namespace Common.Helpers.Internal {
     internal class ChunkedEnumerable<T> : IEnumerable<T> {
        private readonly int _chunkSize;
        private readonly int _start;

        private readonly EnumeratorWrapper<T> _wrapper;

        public ChunkedEnumerable(EnumeratorWrapper<T> wrapper, int chunkSize, int start) {
            _wrapper = wrapper;
            _chunkSize = chunkSize;
            _start = start;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator() {
            return new ChildEnumerator(this);
        }

        private class ChildEnumerator : IEnumerator<T> {
            private readonly ChunkedEnumerable<T> _parent;
            private T _current;
            private bool _done;
            private int _position;


            public ChildEnumerator(ChunkedEnumerable<T> parent) {
                _parent = parent;
                _position = -1;
                parent._wrapper.AddRef();
            }

            public void Dispose() {
                if (!_done) {
                    _done = true;
                    _parent._wrapper.RemoveRef();
                }
            }

            object IEnumerator.Current => Current;

            public bool MoveNext() {
                _position++;

                if (_position + 1 > _parent._chunkSize) {
                    _done = true;
                }

                if (!_done) {
                    _done = !_parent._wrapper.Get(_position + _parent._start, out _current);
                }

                return !_done;
            }

            public void Reset() {
                // per http://msdn.microsoft.com/en-us/library/system.collections.ienumerator.reset.aspx
                throw new NotSupportedException();
            }

            public T Current {
                get {
                    if (_position == -1 || _done) {
                        throw new InvalidOperationException();
                    }

                    return _current;
                }
            }
        }
    }
}
