using System.Collections.Generic;

namespace algorithms.structures
{
    // ----- Linked List Array -------------------------------------------------
    //
    // Doubly linked lists (generic) inplemented with sharing arrays
    //
    // LinkedListHeap(int capacity)
    // int Pop()
    // void Push(int index)
    //
    // LinkedListArraySet<T>(int capacity)
    // LinkedListArray<T> CreateLinkedListArray()
    // LinkedListArray<T> CreateLinkedListArray(IEnumerable<T> items)
    //
    // LinkedListArray(LinkedListHeap<T> heap)
    // LinkedListArray(LinkedListHeap<T> heap, IEnumerable<T> items)
    // int Count
    // T Current
    // void First()
    // void Last()
    // bool Next()
    // bool Prev()
    // bool AddAfter(T item)
    // bool AddBefore(T item)
    // void Clear()
    // bool Remove()
    // -------------------------------------------------------------------------
    public class LinkedListHeap<T>
    {
        public T[] Heap { get; private set; }
        public int[] Next { get; private set; }
        public int[] Prev { get; private set; }
        int first;
        public LinkedListHeap(int capacity)
        {
            Heap = new T[capacity];
            Next = new int[capacity];
            Prev = new int[capacity];
            for (int i = 0; i < capacity - 1; i++)
                Next[i] = i + 1;
            Next[capacity - 1] = -1;
            first = 0;
        }
        public int Pop()
        {
            if (first == -1) return -1;
            int pop = first;
            first = Next[pop];
            return pop;
        }
        public void Push(int index)
        {
            Next[index] = first;
            first = index;
        }
    }

    public class LinkedListArraySet<T>
    {
        LinkedListHeap<T> heap;
        public LinkedListArraySet(int capacity)
        {
            heap = new LinkedListHeap<T>(capacity);
        }
        public LinkedListArray<T> CreateLinkedListArray()
        {
            return new LinkedListArray<T>(heap);
        }
        public LinkedListArray<T> CreateLinkedListArray(IEnumerable<T> items)
        {
            return new LinkedListArray<T>(heap, items);
        }
    }

    public class LinkedListArray<T>
    {
        LinkedListHeap<T> heap;
        int first;
        int last;
        int current;
        int count;
        public LinkedListArray(LinkedListHeap<T> heap)
        {
            this.heap = heap;
            first = -1;
            last = -1;
            current = -1;
            count = 0;
        }
        public LinkedListArray(LinkedListHeap<T> heap, IEnumerable<T> items) : this(heap)
        {
            foreach (T item in items) AddAfter(item);
        }
        public int Count { get { return count; } }
        public T Current { get { return current > -1 ? heap.Heap[current] : default(T); } }
        public void First() { current = first; }
        public void Last() { current = last; }
        public bool Next()
        {
            if (current == -1 || heap.Next[current] == -1) return false;
            current = heap.Next[current];
            return true;
        }
        public bool Prev()
        {
            if (current == -1 || heap.Prev[current] == -1) return false;
            current = heap.Prev[current];
            return true;
        }
        public bool AddAfter(T item)
        {
            int index = heap.Pop();
            if (index == -1) return false;
            heap.Heap[index] = item;

            if (first == -1)
            {
                heap.Next[index] = -1;
                heap.Prev[index] = -1;
                first = index;
                last = index;
                current = index;
                count++;
                return true;
            }
            if (current == -1) current = first;

            int next = heap.Next[current];
            heap.Next[current] = index;
            heap.Next[index] = next;
            if (next > -1)
                heap.Prev[next] = index;
            else
                last = index;
            heap.Prev[index] = current;
            current = index;
            count++;
            return true;
        }
        public bool AddBefore(T item)
        {
            int index = heap.Pop();
            if (index == -1) return false;
            heap.Heap[index] = item;

            if (first == -1)
            {
                heap.Next[index] = -1;
                heap.Prev[index] = -1;
                first = index;
                last = index;
                current = index;
                count++;
                return true;
            }
            if (current == -1) current = first;

            int prev = heap.Prev[current];
            heap.Prev[current] = index;
            heap.Prev[index] = prev;
            if (prev > -1)
                heap.Next[prev] = index;
            else
                first = index;
            heap.Next[index] = current;
            current = index;
            count++;
            return true;
        }
        public void Clear()
        {
            current = first;
            while (current > -1)
            {
                int push = current;
                current = heap.Next[current];
                heap.Push(push);
            }
            first = -1;
            last = -1;
            count = 0;
        }
        public bool Remove() {
            if (current == -1) return false;
            int next = heap.Next[current];
            int prev = heap.Prev[current];
            heap.Push(current);

            if (next > -1)
                heap.Prev[next] = prev;
            else
                last = prev;

            if (prev > -1)
                heap.Next[prev] = next;
            else
                first = next;

            current = prev;
            if (current == -1) current = first;
            count--;
            return true;
        }
        public IEnumerable<T> GetItems()
        {
            First();
            if (Count > 0) yield return Current;
            while (Next()) yield return Current;
        }
    }
    // -------------------------------------------------------------------------
}
