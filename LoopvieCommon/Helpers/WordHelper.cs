namespace LoopvieCommon.Helpers
{
    public static class WordHelper
    {
        public static void RandomizeList<T>(List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static List<string> GetWordAnswersVariants<T,TVariants>(T word,TVariants variants) where T : IEnumerable<char> where TVariants : List<string>
        {
            return new List<string>(variants) { word.ToString() };
        }
    }
}
