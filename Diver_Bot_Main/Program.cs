namespace Diver_Bot;

public static class Program
{
    public static void Main(string[] args)
    {
        try
        {
            App.Startup();
            Task.Run(async () =>
            {
                await App.Client.ConnectAsync();
                await Task.Delay(-1);
            }).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}

