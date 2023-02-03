namespace IoneVectronConverter.Common
{
    public struct GcRange
    {
        public int From { get; set; }
        public int To { get; set; }

        public override string ToString()
        {
            return $"{From} - {To}";
        }
    }
}
