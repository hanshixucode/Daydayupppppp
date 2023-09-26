using System;
using System.Collections.Generic;

namespace YeluoFunc
{
    internal class Program230926
    {
        public static void Main(string[] args)
        {
            
        }
        
        /// <summary>
        /// 20. 有效的括号
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool IsValid(string s)
        {
            if (s.Length % 2 != 0) return false;
            Dictionary<string, string> Dic = new Dictionary<string, string>()
             {
                 {"(",")"},
                 {"{",")"},
                 {"[",")"},
             };
            for (int i = 0; i < s.Length;)
            {
                if (Dic.ContainsKey(s[i].ToString()) && s[i + 1].ToString() == Dic[s[i].ToString()])
                {
                    i = i + 2;
                    continue;
                }

                return false;
            }
            return true;
        }
    }
}