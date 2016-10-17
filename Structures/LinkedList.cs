using System;
using System.Collections;
using System.Collections.Generic;

namespace Structures
{
    public class LinkedList<T> : IList<T>
    {
        private class Node<T>
        {
            public T _value;
            public Node<T> next;
            public Node(T item, Node<T> nextNode = null)
            {
                _value = item;
                next = nextNode;
            }
        }

        private Node<T> _head;

        public IEnumerator<T> GetEnumerator()
        {
            return new ListEnumerable<T>(_head);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class ListEnumerable<T> : IEnumerator<T>
        {
            public ListEnumerable(Node<T> head)
            {
                _current = head;
                _head = head;
            }

            private Node<T> _current;
            private readonly Node<T> _head;
            private bool _start = true;
            public void Dispose()
            {
                _current = null;
            }

            public bool MoveNext()
            {
                if (_start)
                {
                    _start = false;
                    return true;
                }
                _current = _current.next;
                return _current != null;
            }

            public void Reset()
            {
                _current = _head;
            }

            T IEnumerator<T>.Current => _current._value;

            private T Current()
            {
                return _current._value;
            }

            object IEnumerator.Current => Current();
        }

        public void Add(T item)
        {
            var newNode = new Node<T>(item, _head);
            _head = newNode;
        }

        public void Clear()
        {
            _head = null;
        }

        public bool Contains(T item)
        {
            if (_head == null) return false;
            var current = _head;
            do
            {
                if (item.Equals(current._value)) return true;
            } while ((current = current.next) != null);
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            int count = Count();
            if (count + arrayIndex > array.Length) throw new ArgumentOutOfRangeException();
            var cur = _head;
            for (int i = arrayIndex; i < arrayIndex + count; i++)
            {
                array[i] = cur._value;
                cur = cur.next;
            }
        }

        public bool Remove(T item)
        {
            if (_head == null) return false;
            var current = _head;
            Node<T> prev = null;
            do
            {
                if (item.Equals(current._value))
                {
                    if (prev == null)
                    {
                        _head = current.next;
                        return true;

                    }
                    prev.next = current.next;
                    return true;
                }
                prev = current;
            } while ((current = current.next) != null);
            return false;
        }

        int ICollection<T>.Count => Count();

        public int Count()
        {
            if (_head == null) return 0;
            int count = 0;
            var current = _head;
            do
            {
                count++;
            } while ((current = current.next) != null);
            return count;
        }
        public bool IsReadOnly { get; } //TODO: worry about this
        public int IndexOf(T item)
        {
            if (_head == null) return -1;
            int count = 0;
            int mark = -1;
            var current = _head;
            do
            {
                if (item.Equals(current._value)) mark = count;
                count++;
            } while ((current = current.next) != null);
            return count - mark;
        }

        public void Insert(int index, T item)
        {
            if (_head == null) throw new ArgumentOutOfRangeException();
            int count = 0;
            var current = _head;
            if (count == index)
            {
                Node<T> newHead = new Node<T>(item, _head);
                _head = newHead;
                return;
            }
            do
            {
                if (count == index - 1)
                {
                    Node<T> newNode = new Node<T>(item, current.next);
                    current.next = newNode;
                    return;
                } 
                count++;
            } while ((current = current.next) != null);
            throw new ArgumentOutOfRangeException();
        }

        public void RemoveAt(int index)
        {
            if (_head == null) throw new ArgumentOutOfRangeException();
            int count = 0;
            var current = _head;
            Node<T> prev = null;
            do
            {
                if (count == index)
                {
                    prev.next = current.next;
                    return;
                }
                prev = current;
                count++;
            } while ((current = current.next) != null);
            throw new ArgumentOutOfRangeException();
        }

        public T this[int index]
        {
            get
            {
                if (_head == null) throw new ArgumentOutOfRangeException();
                int count = 0;
                var current = _head;
                do
                {
                    if (count == index)
                    {
                        return current._value;
                    }
                    count++;
                } while ((current = current.next) != null);
                throw new ArgumentOutOfRangeException();
            }
            set
            {
                if (_head == null) throw new ArgumentOutOfRangeException();
                int count = 0;
                Node<T> current = _head;
                do
                {
                    if (count == index)
                    {
                        current._value = value;
                    }
                    count++;
                } while ((current = current.next) != null); throw new NotImplementedException();
            }
        }
    }
}
