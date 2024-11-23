using Diver_Bot.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.Configuration;

namespace Diver_Bot;

public class App
{
    public static DiscordClient Client { get; private set; }
    public static CommandsNextExtension Commands { get; set; }
    public static IConfigurationRoot Configuration { get; set; } = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json", false, true).Build();
    
    public static void Startup()
    {
        ConfigBot();
    }

    private static void ConfigBot()
    {
        if (string.IsNullOrEmpty(Configuration["BOT_TOKEN"]))
            throw new Exception("Bot token is missing from configuration.");

        var discordConfig = new DiscordConfiguration()
        {
            Intents = DiscordIntents.All,
            Token = Configuration["BOT_TOKEN"] ?? "",
            TokenType = TokenType.Bot,
            AutoReconnect = true,
        };
        Client = new DiscordClient(discordConfig);
        Client.UseInteractivity(new InteractivityConfiguration()
        {
            Timeout = TimeSpan.FromMinutes(2)
        });
        if (string.IsNullOrEmpty(Configuration["Prefix"]))
            throw new Exception("Bot token is missing from configuration.");

        var commandConfig = new CommandsNextConfiguration()
        {
            StringPrefixes = new string[] { Configuration["PREFIX"] ?? "" },
            EnableMentionPrefix = true,
            EnableDms = true,
            EnableDefaultHelp = false
        };
        
        Commands = Client.UseCommandsNext(commandConfig);
        Commands.RegisterCommands<TestCommand>();
        Client.ChannelCreated += ClientOnChannelCreated;
        Client.VoiceStateUpdated += ClientOnVoiceStateUpdated;
        Client.Ready += ClientOnReady;
    }

    private static async Task ClientOnVoiceStateUpdated(DiscordClient sender, VoiceStateUpdateEventArgs args)
    {
        if (args.Before == null && args.Channel.Name == "Phòng Chờ")
        {
            await args.Channel.SendMessageAsync($"{args.User.Mention} has joined the server.");
        }
    }

    private static async Task ClientOnChannelCreated(DiscordClient sender, ChannelCreateEventArgs args)
    {
        await args.Channel.SendMessageAsync($"Chào mừng bạn đã đến với kênh {args.Channel.Name}");
    }

    private static Task ClientOnReady(DiscordClient sender, ReadyEventArgs args)
    {
        Console.WriteLine("Connected!");
        return Task.CompletedTask;
    }
}