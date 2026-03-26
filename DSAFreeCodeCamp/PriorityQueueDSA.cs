using System;
using System.Collections.Generic;
using System.Text;

namespace DSAFreeCodeCamp;

public class PriorityQueueDSA
{
    public int KthLargestElement(int[] arr, int k)
    {
        PriorityQueue<int, int> minHeap = new PriorityQueue<int, int>();
        foreach (int num in arr)
        {
            minHeap.Enqueue(num, num);
            if (minHeap.Count > k)
            {
                minHeap.Dequeue();
            }
        }
        return minHeap.Peek();
    }

    public int KthSmallestElement(int[] arr, int k)
    {
        PriorityQueue<int, int> maxHeap = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b.CompareTo(a)));
        foreach (int num in arr)
        {
            maxHeap.Enqueue(num, num);
            if (maxHeap.Count > k)
            {
                maxHeap.Dequeue();
            }
        }
        return maxHeap.Peek();
    }
}
