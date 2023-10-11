// using System;
// using System.Collections.Generic;
//
// namespace YeluoFunc
// {
//     internal class Program230926
//     {
//         public static void Main(string[] args)
//         {
//             IsValid("()[]{}");
//         }
//         
//         /// <summary>
//         /// 20. 有效的括号
//         /// </summary>
//         /// <param name="s"></param>
//         /// <returns></returns>
//         public static bool IsValid(string s)
//         {
//             if (s.Length % 2 != 0) return false;
//             Stack<char> stack = new Stack<char>();
//             for (int i = 0; i < s.Length;i++)
//             {
//                 if (s[i] == '(')
//                 {
//                     stack.Push(')');
//                 }
//                 else if (s[i] == '{')
//                 {
//                     stack.Push('}');
//                 }
//                 else if (s[i] == '[')
//                 {
//                     stack.Push(']');
//                 }
//                 else if (stack.Count == 0 || stack.Pop() != s[i])
//                 {
//                     return false;
//                 }
//             }
//             if (stack.Count>0)
//             {
//                 return false;
//             }
//             return true;
//         }
//     }
// }