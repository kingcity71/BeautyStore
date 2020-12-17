namespace BeautyStore.DAL
{
    public static class StringExtansion
    {
        public static bool IsEqual(this string str1, string str2)
            => str1 != null && str1.Equals(str2, System.StringComparison.InvariantCultureIgnoreCase);

        public static bool IsContaint(this string str1, string str2)
            => str1!=null && str2!=null && str1.ToLower().Contains(str2.ToLower());
    }
}
