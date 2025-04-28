public class Random
{
    int seed;

    public Random(int initialSeed)
    {
        seed = initialSeed;
    }

    public int Next()
    {
        seed = seed * 1664525 + 1013904223;
        return seed;
    }

    public int NextInt(int max)
    {
        Next();
        int r = seed;
        r = r / 1000;
        r = Math.abs(r);
        return r - (max * (r / max));
    }
}
