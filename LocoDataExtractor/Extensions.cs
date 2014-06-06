namespace LocoDataExtractor
{
    public static class Extensions
    {
        public static string GetClassName(this object obj)
        {
            var split = obj.GetType().ToString().Split('.');
            return split[split.Length - 1];
        }
    }
}
