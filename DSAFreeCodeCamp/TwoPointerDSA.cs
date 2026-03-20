using System;
using System.Collections.Generic;
using System.Text;

namespace DSAFreeCodeCamp;

public class TwoPointerDSA
{
    public bool PalindromeCheck(string str)
    {
        int left = 0;
        int right = str.Length - 1;
        while (left < right)
        {
            if (str[left] != str[right])
            {
                return false;
            }
            left++;
            right--;
        }
        return true;
    }

    public void ReverseString(char[] s)
    {
        int left = 0;
        int right = s.Length - 1;
        while (left < right)
        {
            char temp = s[left];
            s[left] = s[right];
            s[right] = temp;
            left++;
            right--;
        }
    }

    public void MiddleOfTheLinkedList(Node head)
    {
        Node slow = head;
        Node fast = head;
        while (fast != null && fast.Next != null)
        {
            slow = slow.Next;
            fast = fast.Next.Next;
        }
        Console.WriteLine($"Middle node value: {slow.Value}");
    }
}

public class Node
{
    public int Value { get; set; }
    public Node Next { get; set; }

    public Node(int value)
    {
        Value = value;
    }
}