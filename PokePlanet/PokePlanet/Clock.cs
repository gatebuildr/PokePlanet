namespace PokePlanet
{
    class Clock
    {
        // ReSharper disable once InconsistentNaming
        public const int TargetFPS = 60;
        public static int FramesToMillis(int frames)
        {
            return 1000*frames/TargetFPS;
        }
    }
}
