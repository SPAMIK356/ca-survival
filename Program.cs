namespace CASurvive
{
    internal class Program
    {
        public static Action? Loaded;
        static void Main(string[] args)
        {
            SimConfig.Initialize();
        }
    }
}
