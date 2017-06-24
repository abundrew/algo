using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithms.structures
{
    // ----- Linked List Array -------------------------------------------------
    //
    // LinkedListHeap(int capacity)
    // LinkedListItem<T> this[int index]
    // int Pop(int index, T data)
    // void Push(int index)
    // void Push(int prev, int index)
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
        public int Pop(int index, T data) {
            if (first == -1) return -1;
            int pop = first;
            first = heap[first].Next;
            heap[pop].Next = index;
            heap[pop].Data = data;
            return pop;
        }
        public void Push(int index)
        {
            heap[index].Next = first;
            first = index;
        }
        public void Push(int prev, int index)
        {
            heap[prev].Next = heap[index].Next;
            heap[index].Next = first;
            first = index;
        }
    }

    public class LinkedListArray<T> where T: IComparable
    {
        LinkedListHeap<T> heap = null;
        int first = -1;
        public LinkedListArray(LinkedListHeap<T> heap)
        {
            this.heap = heap;
            first = -1;
        }
        public LinkedListArray(LinkedListHeap<T> heap, IEnumerable<T> items)
        {
            this.heap = heap;
            first = -1;
            foreach (T item in items) Add(item);
        }
        public IEnumerable<T> Items {
            get {
                int index = first;
                while (index != -1)
                {
                    yield return heap[index].Data;
                    index = heap[index].Next;
                }
            }
        }
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
