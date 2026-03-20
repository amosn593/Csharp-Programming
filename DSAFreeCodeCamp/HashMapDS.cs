using System;
using System.Collections.Generic;
using System.Text;

namespace DSAFreeCodeCamp;

public class HashMapDS
{
    public void TwoValuesSumToTarget(int[] arr, int target)
    {
        Dictionary<int, int> hashMap = new Dictionary<int, int>();
        for (int i = 0; i < arr.Length; i++)
        {
            int complement = target - arr[i];
            if (hashMap.ContainsKey(complement))
            {
                
                Console.WriteLine($"Pair found: [{hashMap[complement]} , {i} ] ");
                return;
            }
            hashMap[arr[i]] = i;
        }
        Console.WriteLine("No pair found that sums to the target.");
    }
}
