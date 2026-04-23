using System;
using System.Collections.Generic;
using System.Text;

namespace DSAPatterns;

public static class TwoPointer_2
{
    /*
            The Two Pointer pattern involves using two pointers to traverse an array from both ends towards the center.
            This technique is often used to solve problems related to finding pairs, triplets, or subarrays that meet certain conditions.
            It reduces time complexity from O(n^2) to O(n) for many problems.

            When to use:
            Finding pairs in a sorted array
            Reversing an array
            Merging two sorted arrays
            Removing duplicates from a sorted array
           Comparing elements from both ends
           Partitioning arrays
           Palindrome checks
        */
    public static void Main()
    {
        Console.WriteLine("Two Pointer Pattern");
        var result = Template();
        Console.WriteLine($"Result: [{result[0]}, {result[1]}]");

        //Pending: 3Sum, IsPalindrome
        Console.WriteLine();
        Console.WriteLine("IsPalindrome: A man a plan a canal Panama ? " + IsPalindrome("A man a plan a canal Panama"));
    }
    public static int[] Template()
    {
        int[] arr = { 1, 2, 3, 4, 5, 6 };
        int target = 7;
        int left = 0;
        int right = arr.Length - 1;
        int[] response = {-1, -1};
        while (left < right)
        {
            int sum = arr[left] + arr[right];
            if (sum == target)
            {
                Console.WriteLine($"Pair found: ({left}, {right})");
                response[0] = left;
                response[1] = right;
                //Returning here will give us the first pair found.
                //If we want to find all pairs, we can continue searching by moving both pointers.
                return response;
            }
            else if (sum < target)
            {
                left++;
            }
            else
            {
                right--;
            }
        }

        // If no pair is found  
        Console.WriteLine("No pair found.");
        return response;
    }

    public static int[] _3Sum(int[] nums, int target)
    {
        Array.Sort(nums);
        for (int i = 0; i < nums.Length - 2; i++)
        {
            int left = i + 1;
            int right = nums.Length - 1;
            while (left < right)
            {
                int sum = nums[i] + nums[left] + nums[right];
                if (sum == target)
                {
                    return new int[] { nums[i], nums[left], nums[right] };
                }
                else if (sum < target)
                {
                    left++;
                }
                else
                {
                    right--;
                }
            }
        }
        return new int[] { -1, -1, -1 }; // No triplet found
    }


    public static bool IsPalindrome(string s)
    {
        int left = 0;
        int right = s.Length - 1;
        while (left < right)
        {
            // Skip non-alphanumeric characters
            while (left < right && !char.IsLetterOrDigit(s[left]))
            {
                left++;
            }
            while (left < right && !char.IsLetterOrDigit(s[right]))
            {
                right--;
            }
            if (char.ToLower(s[left]) != char.ToLower(s[right]))
            {
                return false;
            }
            left++;
            right--;
        }
        return true;
    }

}
