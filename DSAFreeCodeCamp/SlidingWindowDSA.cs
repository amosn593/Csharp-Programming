using System;
using System.Collections.Generic;
using System.Text;

namespace DSAFreeCodeCamp;

public class SlidingWindowDSA
{
    public int MaxSumSubarray(int[] arr, int k)
    {
        if (arr.Length < k)
        {
            throw new ArgumentException("Array length must be greater than or equal to k.");
        }
        int maxSum = 0;
        int currentSum = 0;
        // Calculate the sum of the first window
        for (int i = 0; i < k; i++)
        {
            currentSum += arr[i];
        }
        maxSum = currentSum;
        // Slide the window through the array
        for (int i = k; i < arr.Length; i++)
        {
            currentSum += arr[i] - arr[i - k]; // Add next element and remove the first element of the previous window
            maxSum = Math.Max(maxSum, currentSum); // Update max sum if current sum is greater
        }
        return maxSum;
    }

    public int LongestSubstringWithoutRepeatingCharacters(string s)
    {
        HashSet<char> charSet = new HashSet<char>();
        int left = 0;
        int maxLength = 0;
        for (int right = 0; right < s.Length; right++)
        {
            while (charSet.Contains(s[right]))
            {
                charSet.Remove(s[left]);
                left++;
            }
            charSet.Add(s[right]);
            maxLength = Math.Max(maxLength, right - left + 1);
        }
        return maxLength;
    }
}
