using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Functions;

namespace Utilities
{
    public class Skiplist<T> : IList<T> where T :  IComparable<T>, IEquatable<T>
    {
        
        private class Node<T> where T : IComparable<T>, IEquatable<T>
        {
            public Node<T> _right;
            public Node<T> _left;
            public Node<T> _up;
            public Node<T> _down;
            public T _value;
            public Node(T value)
            {
                _value = value;
            }

            public Node(T value, Node<T> right)
            {
                this._value = value;
                this._right = right;
            } 
        }
        
        Node<T>[] lists = new Node<T>[10]; //max size ~= 4 ^ 10
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            if (lists[0] == null)
            {
                lists[0] = new Node<T>(item);
            }
            else
            {
                var current = lists[0];
                do
                {
                    if (current._value.CompareTo(item) >= 0)
                    {
                        Node<T> newNode = new Node<T>(item)
                        {
                            _left = current._left, //doublecheck this
                            _right = current
                        };
                        current._left = newNode;


                        int level = 0;
                        bool flipAgain = WeakRandom.XorShift128()%4 == 0;
                        Node<T> left = newNode._left;
                        while (flipAgain)
                        {
                            if (left._up != null)
                            {
                                Node<T> temp = new Node<T>(item);
                                temp._down = current;
                                temp._left = left;
                                temp._right = current;
                                left._right = temp;
                            }
                            else
                            {
                                left = left._left;
                            }
                            flipAgain = WeakRandom.XorShift128() % 4 == 0;
                        }

                    }
                    
                } while ((current = current._right) != null);
            }
        }
        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public int Count { get; }
        public bool IsReadOnly { get; }
        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public T this[int index]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
