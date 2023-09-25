using System;

namespace YeluoFunc
{
    internal class Program230925
    {
        public static void Main(string[] args)
        {
            //Console.WriteLine(TwoSum(new[] { 2, 7, 11, 15 }, 9).ToString());
            IsPalindrome(121);
        }
        
        /// <summary>
        /// 1. 两数之和
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int[] TwoSum(int[] nums, int target)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (nums[i] + nums[j] == target)
                    {
                        return new[] { i, j };
                    }
                }
            }

            return new[] { 0, 0 };
        }

        /// <summary>
        /// 9. 回文数
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static bool IsPalindrome(int x)
        {
            if (x < 0) return false; 
            string xstirng = x.ToString();
            int lenth = xstirng.Length;
            for (int i = 0; i < lenth; i++)
            {
                int j = lenth - i - 1;
                if (xstirng[i] != xstirng[j])
                    return false;
            }
            return true;
        }
    }
}