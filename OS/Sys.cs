public static class Sys
{
    public static void init()
    {
        Math.init();
        Memory.init();
        Screen.init();
        Output.init();
        Main.main();
    }

    public static void wait(int duration)
    {
        Thread.Sleep(duration);
    }

    public static void halt()
    {
        while (true)
        {
            Sys.wait(1000);
        }
    }
}
