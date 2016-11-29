namespace Task
{
    internal class Pair
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public Pair(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public override string ToString()
        {
            return string.Format("{0}|{1}", Min, Max);
        }
    }
}