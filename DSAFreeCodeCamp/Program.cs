using DSAFreeCodeCamp;

Console.WriteLine("Free Code Camp, DSA!");

// Two Values Sum To Target
var hashMapDS = new HashMapDS();
var arr = new int[] { 2, 7, 11, 15 };
var target = 9;
hashMapDS.TwoValuesSumToTarget(arr, target);

//Tow Pointer Palindrome Check
var twoPointerDSA = new TwoPointerDSA();
var str = "racecar";

Console.WriteLine($"Is '{str}' a palindrome? {twoPointerDSA.PalindromeCheck(str)}");

// Sliding Window Max Sum Subarray
var slidingWindowDSA = new SlidingWindowDSA();
var arr2 = new int[] { 1, 2, 3, 4, 5, 6 };
var k = 3;
Console.WriteLine($"Maximum sum of a subarray of size {k} is: {slidingWindowDSA.MaxSumSubarray(arr2, k)}");

// Sliding Window Longest Substring Without Repeating Characters
var str2 = "abcabcbb";
Console.WriteLine($"Length of the longest substring without repeating characters in '{str2}' is: {slidingWindowDSA.LongestSubstringWithoutRepeatingCharacters(str2)}");

//Binary Search First Occurrence
var binarySearchDSA = new BinarySearchDSA();
var arr3 = new int[] { 1, 2, 2, 3, 4, 5 };
Console.WriteLine($"First occurrence of 2 in the array is at index: {binarySearchDSA.FirstOccurrence(arr3, 2)}");

//Binary Search
var arr4 = new int[] { 1, 2, 3, 4, 5, 6 };
Console.WriteLine($"Index of 4 in the array is: {binarySearchDSA.BinarySearch(arr4, 4)}");

//Priority Queue
var priorityQueueDSA = new PriorityQueueDSA();
var arr5 = new int[] { 5, 2, 8, 1, 3 };
Console.WriteLine($"3 largest item: {priorityQueueDSA.KthLargestElement(arr5, 3)}");