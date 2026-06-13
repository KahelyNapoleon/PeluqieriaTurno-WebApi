using System;
using System.Collections.Generic;

namespace Temporal
{
    public static class Program
    {
        static void Main()
        {
            var nums = new int[] { 0, 0, 1, 1, 1, 2, 2, 3, 3, 4 };

            Console.WriteLine(RemoveDuplicates(nums));
            Console.WriteLine(RemoveElement(nums, 2));
        }

        public static int RemoveDuplicates(int[] nums)
        {
            int derechaP = 1;
            int izquierdaP = 0;

            for (; derechaP < nums.Length; derechaP++)
            {
                if (nums[derechaP] != nums[izquierdaP])
                {
                    izquierdaP++;
                    nums[izquierdaP] = nums[derechaP];
                }
            }

            return izquierdaP + 1;

        }

        public static int RemoveElement(int[] nums, int val)
        {
            

            int izquierda = 0;
            int derecha = nums.Length - 1;

            for (; izquierda < derecha; derecha--)
            {
                if (nums[izquierda] == val)
                {
                    if (nums[derecha] == val)
                    {
                        continue;
                    }
                    nums[izquierda] = nums[derecha];
                    izquierda++;
                }
                else
                {
                    izquierda++;
                }
            }

            return izquierda +1;
        }
    }
}