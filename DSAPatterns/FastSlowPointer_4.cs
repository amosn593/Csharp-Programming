using System;
using System.Collections.Generic;
using System.Text;

namespace DSAPatterns;

public static class FastSlowPointer_4
{
    /*
     * The Fast & Slow Pointers pattern (also called Tortoise and Hare) uses two pointers moving at different speeds. 
     * When there is a cycle, the fast pointer will eventually meet the slow pointer.
     * 
     * When to use
        Detecting cycles in linked lists or arrays

        Finding the middle of a linked list

        Finding the start of a cycle
     */

    public static void Main()
    {
       Console.WriteLine("Fast & Slow Pointers pattern!");
    }

    public static bool Template(ListNode head)
    {
        if (head == null) return false;

        // Fast & Slow Pointers template
        ListNode slow = head;
        ListNode fast = head;

        while (fast != null && fast.next != null)
        {
            slow = slow.next;
            fast = fast.next.next;
            if (slow == fast)
            {
                return true; // Cycle detected
            }
        }
        return false; // No cycle
    }

    public static bool HasCycle(ListNode head)
    {
        return Template(head);
    }

    public static ListNode FindCycleStart(ListNode head)
    {
        if (head == null) return null;
        ListNode slow = head;
        ListNode fast = head;
        // First step: Determine if there is a cycle
        while (fast != null && fast.next != null)
        {
            slow = slow.next;
            fast = fast.next.next;
            if (slow == fast)
            {
                // Cycle detected, now find the start of the cycle
                ListNode pointer1 = head;
                ListNode pointer2 = slow;
                while (pointer1 != pointer2)
                {
                    pointer1 = pointer1.next;
                    pointer2 = pointer2.next;
                }
                return pointer1; // Start of the cycle
            }
        }
        return null; // No cycle
    }

    public class ListNode
    {
        public int val;
        public ListNode next;

        public ListNode(int x)
        {
            val = x;
            next = null;
        }
    }

}
