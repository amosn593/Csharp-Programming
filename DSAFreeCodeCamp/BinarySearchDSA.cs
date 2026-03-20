using System;
using System.Collections.Generic;
using System.Text;

namespace DSAFreeCodeCamp;

public class BinarySearchDSA
{
    public int FirstOccurrence(int[] arr, int target)
    {
        int left = 0;
        int right = arr.Length - 1;
        int result = -1;
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (arr[mid] == target)
            {
                result = mid; // Store the index of the found target
                right = mid - 1; // Continue searching in the left half
            }
            else if (arr[mid] < target)
            {
                left = mid + 1; // Search in the right half
            }
            else
            {
                right = mid - 1; // Search in the left half
            }
        }
        return result; // Return the index of the first occurrence or -1 if not found
    }

    public int BinarySearch(int[] arr, int target)
    {
        int left = 0;
        int right = arr.Length - 1;
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (arr[mid] == target)
            {
                return mid; // Target found at index mid
            }
            else if (arr[mid] < target)
            {
                left = mid + 1; // Search in the right half
            }
            else
            {
                right = mid - 1; // Search in the left half
            }
        }
        return -1; // Target not found in the array
    }

    public int FindMinimumInRotatedSortedArray(int[] arr)
    {
        int left = 0;
        int right = arr.Length - 1;
        while (left < right)
        {
            int mid = left + (right - left) / 2;
            if (arr[mid] > arr[right])
            {
                left = mid + 1; // Minimum is in the right half
            }
            else
            {
                right = mid; // Minimum is in the left half or at mid
            }
        }
        return arr[left]; // The minimum element is at index left
    }
}
