using System;
using System.Collections.Generic;
using System.Text;

namespace DSAPatterns;

public static class SlidingWindow_3
{
    /*
     * The Sliding Window pattern maintains a window of elements and slides it across the array to find subarrays or substrings
     * that satisfy certain conditions. It avoids recalculating overlapping parts of consecutive windows.
     * When to use
            Contiguous subarray/substring problems

            Finding maximum/minimum in window of size k

            Longest/shortest substring with certain properties

            Problems involving consecutive elements
     */
    public static void Main()
    {
        //var result = Template();
        Console.WriteLine($"Sliding Window Pattern");

        //Template: Maximum sum of a subarray of size k
        Console.WriteLine($"Maximum sum of a subarray of size {3}: {Template()}");

        //Template: Longest substring with unique characters
        Console.WriteLine($"Longest substring with unique characters: {Template2()}");
    }

    public static int Template()
    {
        int[] arr = { 1, 2, 3, 4, 5 };
        int k = 3;
        int maxSum = int.MinValue;
        int windowSum = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            windowSum += arr[i];
            if (i >= k - 1)
            {
                maxSum = Math.Max(maxSum, windowSum);
                windowSum -= arr[i - (k - 1)];
            }
        }
        return maxSum;
    }

    public static int Template2()
    {
        string str = "abcabcbb";
        int k = 3;
        HashSet<char> windowChars = new HashSet<char>();
        int maxLength = 0;
        int left = 0;
        for (int right = 0; right < str.Length; right++)
        {
            while (windowChars.Contains(str[right]))
            {
                windowChars.Remove(str[left]);
                left++;
            }
            windowChars.Add(str[right]);
            maxLength = Math.Max(maxLength, right - left + 1);
        }
        return maxLength;
    }
}
