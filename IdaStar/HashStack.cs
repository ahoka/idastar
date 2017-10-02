using System.Collections;
using System.Collections.Generic;

namespace IdaStar
{
    public class HashStack<T> : IEnumerable<T>
    {
        private readonly Stack<T> stack = new Stack<T>();
        private readonly HashSet<T> hashSet = new HashSet<T>();

        public T Peek()
        {
            return stack.Peek();
        }

        public T Pop()
        {
            var item = stack.Pop();
            hashSet.Remove(item);

            return item;
        }

        public void Push(T item)
        {
            hashSet.Add(item);
            stack.Push(item);
        }

        public bool Contains(T item)
        {
            return hashSet.Contains(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return stack.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}