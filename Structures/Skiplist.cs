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
        private Node<T> head = new Skiplist<T>.Node<T>(default(T));
        private Node<T> tail = new Skiplist<T>.Node<T>(default(T));
        private int maxHeight = 1;
        public Skiplist()
        {
            head = new Node<T>();
            tail = new Node<T>();
        }
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

            public Node()
            {
                _value = default(T);
            }

            public Node(T value, Node<T> right)
            {
                this._value = value;
                this._right = right;
            }

            public new string ToString()
            {
                return this._value.ToString();
            }
        }
        
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
            if (head._right == null)
            {
                var newNode = new Skiplist<T>.Node<T>(item);
                head._right = newNode;
                newNode._left = head;
                newNode._right = tail;
                tail._left = newNode;
            }
            else
            {
                var newNode = AddNodeToBottomLevel(item);

                int curHeight = 1;
                while (WeakRandom.XorShift128(2) % 4 == 0)
                {
                    curHeight++;
                    if (curHeight > maxHeight) //need to add a new head/tail level
                    {
                        AddLevel();
                    }
                    AddUpperNode(item, newNode);
                }
            }
        }

        private static void AddUpperNode(T item, Node<T> newNode)
        {
            var bookmarkNode = newNode;
            while (bookmarkNode._left != null && bookmarkNode._up == null)
            {
                bookmarkNode = bookmarkNode._left;
            }
            bookmarkNode = bookmarkNode._up;
            var upperNode = new Node<T>(item)
            {
                _right = bookmarkNode._right,
                _left = bookmarkNode,
                _down = newNode
            };
            newNode._up = upperNode;
            bookmarkNode._right = upperNode;
            bookmarkNode._right._left = upperNode;


            bookmarkNode = newNode;
            while (bookmarkNode._right != null && bookmarkNode._up == null)
            {
                bookmarkNode = bookmarkNode._right;
            }
            bookmarkNode = bookmarkNode._up;

            bookmarkNode._right = upperNode;
            bookmarkNode._right._left = upperNode;
        }

        private Node<T> AddNodeToBottomLevel(T item)
        {
            var bookmarkNode = this.FindEntry(item);
            var newNode = new Skiplist<T>.Node<T>(item)
            {
                _right = bookmarkNode._right,
                _left = bookmarkNode
            };
            bookmarkNode._right = newNode;
            newNode._right._left = newNode;
            return newNode;
        }

        private void AddLevel()
        {
            var newHead = new Skiplist<T>.Node<T>();
            var newTail = new Skiplist<T>.Node<T>();
            head._up = newHead;
            tail._up = newTail;
            newHead._right = newTail;
            newTail._left = newHead;
            newHead._down = head;
            newTail._down = tail;
            head = newHead;
            tail = newTail;
            maxHeight++;
        }

        private Node<T> FindEntry(T value)
        {
            var cur = head;
            do
            {
                while (cur._right._right != null && cur._right._value.CompareTo(value) < 1)
                    cur = cur._right;
                
                if(cur._down != null) cur = cur._down;
            } while (cur._down != null);
            while (cur._right._right != null && cur._right._value.CompareTo(value) < 1)
                cur = cur._right;
            return cur;
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

        public new string ToString()
        {
            StringBuilder sb = new StringBuilder();
            var cur = head;
            while (cur._down != null) cur = cur._down;
            while (cur != null)
            {
                sb.Append(cur.ToString() + " ");
                cur = cur._right;
            }
            return sb.ToString();
        }
    }
}
