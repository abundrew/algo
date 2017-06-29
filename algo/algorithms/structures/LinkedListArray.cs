using System;
using System.Collections.Generic;

namespace algorithms.structures
{
    // ----- Linked List Array -------------------------------------------------
    //
    // LinkedListHeap(int capacity)
    // LinkedListItem<T> this[int index]
    // void SetNext(int index, int next)
    // void SetData(int index, T data)
    // int Pop()
    // int Pop(int prev)
    // void Push(int index)
    // void Push(int prev, int index)
    //
    // LinkedListArraySet<T>(int capacity)
    // LinkedListArray<T> CreateLinkedListArray()
    // LinkedListArray<T> CreateLinkedListArray(IEnumerable<T> items)
    //
    // LinkedListArray(LinkedListHeap<T> heap)
    // LinkedListArray(LinkedListHeap<T> heap, IEnumerable<T> items)
    // IEnumerable<T> Items
    // bool Add(T item)
    // void Clear()
    // bool Find(T item)
    // bool Remove(T item)
    // -------------------------------------------------------------------------
    public struct LinkedListItem<T> 
    {
        public int Next { get; set; }
        public T Data { get; set; }
    }

    public class LinkedListHeap<T>
    {
        LinkedListItem<T>[] heap = null;
        int first = 0;
        public LinkedListHeap(int capacity)
        {
            heap = new LinkedListItem<T>[capacity];
            for (int i = 0; i < capacity - 1; i++)
                heap[i].Next = i + 1;
            heap[capacity - 1].Next = -1;
            first = 0;
        }
        public LinkedListItem<T> this[int index] { get { return heap[index]; } }
        public void SetNext(int index, int next)
        {
            heap[index].Next = next;
        }
        public void SetData(int index, T data)
        {
            heap[index].Data = data;
        }
        public int Pop()
        {
            if (first == -1) return -1;
            int pop = first;
            first = heap[pop].Next;
            return pop;
        }
        public int Pop(int prev)
        {
            if (heap[prev].Next == -1) return -1;
            int pop = heap[prev].Next;
            heap[prev].Next = heap[pop].Next;
            return pop;
        }
        public void Push(int index)
        {
            heap[index].Next = first;
            first = index;
        }
        public void Push(int prev, int index)
        {
            heap[index].Next = heap[prev].Next;
            heap[prev].Next = index;
        }
    }

    public class LinkedListArraySet<T> where T : IComparable
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

    public class LinkedListArray<T> where T: IComparable
    {
        LinkedListHeap<T> heap = null;
        int first = -1;
        int current = -1;
        public LinkedListArray(LinkedListHeap<T> heap)
        {
            this.heap = heap;
        }
        public LinkedListArray(LinkedListHeap<T> heap, IEnumerable<T> items)
        {
            this.heap = heap;
            foreach (T item in items) Add(item);
        }
        public void First()
        {
            current = first;
        }
        public bool Next()
        {
            if (current == -1 || heap[current].Next == -1) return false;
            current = heap[current].Next;
            return true;
        }
        public void Last()
        {
            while (current > -1 && heap[current].Next > -1) current = heap[current].Next;
        }
        public T Current { get { return heap[current].Data; } }
        public bool Add(T item)
        {
            int index = heap.Pop(first, item);
            if (index == -1) return false;
            first = index;
            return true;
        }
        public void Clear()
        {
            while (first != -1)
            {
                heap.Push(first);
                first = heap[first].Next;
            }
        }
        public bool Find(T item)
        {
            int index = first;
            while (index != -1)
            {
                if (heap[index].Data.Equals(item)) return true;
                index = heap[index].Next;
            }
            return false;
        }
        public bool Remove(T item) {
            if (heap[first].Data.Equals(item))
            {
                int push = first;
                first = heap[first].Next;
                heap.Push(push);
                return true;
            }
            int index = heap[first].Next;
            int prev = first;
            while (index != -1)
            {
                if (heap[index].Data.Equals(item))
                {
                    heap.Push(prev, index);
                    return true;
                }
                index = heap[index].Next;
            }
            return false;
        }
    }
    // -------------------------------------------------------------------------
}
