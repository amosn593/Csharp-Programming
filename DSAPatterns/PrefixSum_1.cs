using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        //Range sum query using prefix sum array
        // Example: Get the sum of the subarray from index 1 to 3
        int left = 1;
        int right = 3;
        int subarraySum = prefixSum[right + 1 ] - prefixSum[left ];
        Console.WriteLine($"Sum of subarray from index {left} to {right} is: {subarraySum}");
    }

    public static int SubarraySum(int[] nums, int k)
    {
        var count = 0;
        var currentSum = 0;
        var prefixMap = new Dictionary<int, int>();

        prefixMap[0] = 1;   // important

        foreach (var num in nums)
        {
            currentSum += num;

            if (prefixMap.ContainsKey(currentSum - k))
            {
                count += prefixMap[currentSum - k];
            }

            if (!prefixMap.ContainsKey(currentSum))
                prefixMap[currentSum] = 0;

            prefixMap[currentSum]++;
        }

        Console.WriteLine("\nSubarray Sum Equals K:\n");

        Console.WriteLine($"Input array: {string.Join(", ", nums)}");

        Console.WriteLine($"Number of subarrays that sum to {k} is: {count}");

        return count;

    }

    public static int LongestSubarrayWithSumK(int[] nums, int k)
    {
        var maxLength = 0;
        var currentSum = 0;
        var prefixMap = new Dictionary<int, int>();
        prefixMap[0] = -1;   // important
        for (int i = 0; i < nums.Length; i++)
        {
            currentSum += nums[i];
            if (prefixMap.ContainsKey(currentSum - k))
            {
                var length = i - prefixMap[currentSum - k];
                maxLength = Math.Max(maxLength, length);
            }
            if (!prefixMap.ContainsKey(currentSum))
            {
                prefixMap[currentSum] = i;
            }
        }
        Console.WriteLine("\nLongest Subarray with Sum K:\n");
        Console.WriteLine($"Input array: {string.Join(", ", nums)}");
        Console.WriteLine($"Length of the longest contiguous subarray that sums to {k} is: {maxLength}");
        return maxLength;
    }

    public static int LongestSubarrayWithEqual0sAnd1s(int[] nums)
    {
        var maxLength = 0;
        var currentSum = 0;
        var prefixMap = new Dictionary<int, int>();
        prefixMap[0] = -1;   // important
        // We treat 0 as -1 and 1 as +1, so that when we encounter a sum of 0, it means we have an equal number of 0s and 1s.
        // This way, we can use the same logic as finding the longest subarray with sum k (where k is 0 in this case).
        for (int i = 0; i < nums.Length; i++)
        {
            currentSum += nums[i] == 0 ? -1 : 1;
            if (prefixMap.ContainsKey(currentSum))
            {
                var length = i - prefixMap[currentSum];
                maxLength = Math.Max(maxLength, length);
            }
            else
            {
                prefixMap[currentSum] = i;
            }
        }
        Console.WriteLine("\nLongest Subarray with Equal Number of 0s and 1s:\n");
        Console.WriteLine($"Input array: {string.Join(", ", nums)}");
        Console.WriteLine($"Length of the longest contiguous subarray with equal number of 0 and 1 is: {maxLength}");
        return maxLength;
    }

    public static int ProductOfSubarrayExceptSelf(int[] nums)
    {
        int n = nums.Length;
        int[] prefixProduct = new int[n];
        int[] suffixProduct = new int[n];
        int[] result = new int[n];
        prefixProduct[0] = 1;
        for (int i = 1; i < n; i++)
        {
            prefixProduct[i] = prefixProduct[i - 1] * nums[i - 1];
        }
        suffixProduct[n - 1] = 1;
        for (int i = n - 2; i >= 0; i--)
        {
            suffixProduct[i] = suffixProduct[i + 1] * nums[i + 1];
        }
        for (int i = 0; i < n; i++)
        {
            result[i] = prefixProduct[i] * suffixProduct[i];
        }
        Console.WriteLine("\nProduct of Array Except Self:\n");
        Console.WriteLine($"Input array: {string.Join(", ", nums)}");
        Console.WriteLine($"Output array where each element is the product of all other elements: {string.Join(", ", result)}");
        return 0; // Just to satisfy the return type, the actual result is printed above.
    }

    //public static PrefixSumMatrix()
    //{
    /*
     The Prefix Sum Matrix is a 2D extension of the prefix sum array. It allows for efficient sum queries on submatrices.
        When to use
        Multiple sum queries on submatrices
        Finding submatrices with a target sum
        Calculating cumulative totals in 2D
     */


    //} 
}
