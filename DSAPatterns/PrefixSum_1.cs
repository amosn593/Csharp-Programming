using System;
using System.Collections.Generic;
using System.Text;

namespace DSAPatterns;

public static class PrefixSum_1
{
    /*
     The Prefix Sum pattern involves preprocessing an array to create a new array where each element at index i represents
     the sum of all elements from the start up to i. This allows for O(1) sum queries on any subarray.

            When to use
            Multiple sum queries on subarrays

            Finding subarrays with a target sum

            Calculating cumulative totals
     */

    public static void Template()
    {
        int[] arr = { 1, 2, 3, 4, 5, 6};
        int[] prefixSum = new int[arr.Length + 1];

        //prefixSum[0] = arr[0];

        for (int i = 0; i < arr.Length; i++)
        {
            prefixSum[i + 1] = prefixSum[i] + arr[i];
        }

        Console.WriteLine(string.Join(", ", arr));

        Console.WriteLine(string.Join(", ", prefixSum));

        // Example: Get the sum of the subarray from index 1 to 3
        int left = 1;
        int right = 3;
        int subarraySum = prefixSum[right + 1 ] - prefixSum[left ];
        Console.WriteLine($"Sum of subarray from index {left} to {right} is: {subarraySum}");
    }
}
