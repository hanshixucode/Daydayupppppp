// using System;
// using System.Collections.Generic;
// using System.Linq;
//
// namespace YeluoFunc
// {
//     internal class Program230925
//     {
//         public static void Main(string[] args)
//         {
//             //Console.WriteLine(TwoSum(new[] { 2, 7, 11, 15 }, 9).ToString());
//             IsPalindrome(121);
//             Console.WriteLine(RomanToInt("IV").ToString());
//             LongestCommonPrefix(new[] { "ab", "a" });
//         }
//         
//         /// <summary>
//         /// 1. 两数之和
//         /// </summary>
//         /// <param name="nums"></param>
//         /// <param name="target"></param>
//         /// <returns></returns>
//         public static int[] TwoSum(int[] nums, int target)
//         {
//             for (int i = 0; i < nums.Length; i++)
//             {
//                 for (int j = i + 1; j < nums.Length; j++)
//                 {
//                     if (nums[i] + nums[j] == target)
//                     {
//                         return new[] { i, j };
//                     }
//                 }
//             }
//
//             return new[] { 0, 0 };
//         }
//
//         /// <summary>
//         /// 9. 回文数
//         /// </summary>
//         /// <param name="x"></param>
//         /// <returns></returns>
//         public static bool IsPalindrome(int x)
//         {
//             if (x < 0) return false; 
//             string xstirng = x.ToString();
//             int lenth = xstirng.Length;
//             for (int i = 0; i < lenth; i++)
//             {
//                 int j = lenth - i - 1;
//                 if (xstirng[i] != xstirng[j])
//                     return false;
//             }
//             return true;
//         }
//         /// <summary>
//         /// 13. 罗马数字转整数
//         /// </summary>
//         /// <param name="s"></param>
//         /// <returns></returns>
//         public static int RomanToInt(string s)
//         {
//             Dictionary<string, int> StringToNum = new Dictionary<string, int>()
//             {
//                 {"I",1},
//                 {"V",5},
//                 {"X",10},
//                 {"L",50},
//                 {"C",100},
//                 {"D",500},
//                 {"M",1000},
//             };
//             int length = s.Length;
//             int sum = 0;
//             for (int i = 0; i < length; i++)
//             {
//                 int value = StringToNum[s[i].ToString()];
//                 if (i + 1 < length)
//                 {
//                     if (value < StringToNum[s[i + 1].ToString()])
//                     {
//                         value *= -1;
//                     }
//                 }
//                 sum += value;
//             }
//             return sum;
//         }
//         
//         /// <summary>
//         /// 14. 最长公共前缀
//         /// </summary>
//         /// <param name="strs"></param>
//         /// <returns></returns>
//         public static string LongestCommonPrefix(string[] strs)
//         {
//             if (strs.Contains("")) return "";
//             if (strs.Length == 1) return strs[0];
//             string sum = "";
//             for (int i = 0; i < strs[0].Length; i++)
//             {
//                 var common = strs[0][i].ToString();
//                 for (int j = 1; j < strs.Length; j++)
//                 {
//                     if ( i > (strs[j].Length -1) || strs[j][i].ToString() != common)
//                         return sum;
//                 }
//                 sum += common;
//             }
//             
//             return sum;
//         }
//     }
// }