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
}
