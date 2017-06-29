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
    // void First()
    // bool Next()
    // void Last()
    // T Current
    // bool Empty
    // bool Add(T item)
    // void Clear()
    // bool Remove()
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
        LinkedListHeap<T> heap = null;
        int first = -1;
        int curr = -1;
        int prev = -1;
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
            curr = first;
            prev = first;
        }
        public bool Next()
        {
            if (curr == -1 || heap[curr].Next == -1) return false;
            if (curr != first) prev = heap[prev].Next;
            curr = heap[curr].Next;

            return true;
        }
        public void Last()
        {
            while (Next());
        }
        public T Current { get { return heap[curr].Data; } }
        public bool Empty { get { return first == -1; } }
        public bool Add(T item)
        {
            int index = heap.Pop();
            if (index == -1) return false;
            heap.SetData(index, item);
            if (first == -1)
            {
                first = index;
                heap.SetNext(index, -1);
                current = index;
            }
            if (current == -1) current = first;
            heap.SetNext(index, heap[current].Next);
            heap.SetNext(current, index);
            current = index;
            return true;
        }
        public void Clear()
        {
            while (first != -1)
            {
                int next = heap[first].Next;
                heap.Push(first);
                first = next;
            }
            current = -1;
        }
        public bool Remove() {
            if (current == -1) return false;
            int next = heap[current].Next;
            heap.Push(current);
            current = next;
            return true;
        }
    }
    // -------------------------------------------------------------------------
}
