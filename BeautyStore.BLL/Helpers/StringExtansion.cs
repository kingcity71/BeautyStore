using System;
using System.Collections.Generic;
using System.Text;

namespace BeautyStore.BLL.Helpers
{
    public static class StringExtansion
    {
        public static bool IsEqual(this string baseStr, string str) => baseStr.ToLower() == str.ToLower();
    }
}
