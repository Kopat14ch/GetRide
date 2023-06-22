namespace Sources.Level
{
    public static class LevelConfig
    {
        private const int RoadsValue = 0;
        private const int SeedValue = 1;

        private static int[,] _levels = { {1, 742}, {2, 436}, {3, 576}, {4, 483}, {5, 612}, {6, 53}};

        public static int GetRoads(int number) => _levels[--number, RoadsValue];

        public static int GetSeed(int number) => _levels[--number, SeedValue];
    }
}