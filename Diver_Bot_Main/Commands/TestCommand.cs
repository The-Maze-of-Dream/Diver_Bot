using System.Security.Cryptography;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;

namespace Diver_Bot.Commands;

public class TestCommand : BaseCommandModule
{
    [Command("test")]
    [Cooldown(5, 30, CooldownBucketType.User)]
    public async Task TestCommandAsync(CommandContext context)
    {
        await context.Channel.SendMessageAsync("Hello");
    }

    [Command("add")]
    public async Task AddCommandAsync(CommandContext context, int number1, int number2)
    {
        int result = number1 + number2;
        await context.Channel.SendMessageAsync(result.ToString());
    }

    [Command("subtract")]
    public async Task SubtractCommandAsync(CommandContext context, int number1, int number2)
    {
        int result = number1 - number2;
        await context.Channel.SendMessageAsync(result.ToString());
    }

    [Command("embed")]
    public async Task EmbedCommandAsync(CommandContext context)
    {
        var message = new DiscordMessageBuilder().AddEmbed(new DiscordEmbedBuilder()
            .WithTitle("This is my first discord embed")
            .WithDescription($"This command was executed by {context.User.Username}")
            .WithColor(DiscordColor.Blue));
        await context.Channel.SendMessageAsync(message);
    }

    [Command("interact")]
    public async Task InteractCommandAsync(CommandContext context)
    {
        var interact = App.Client.GetInteractivity();
        var messageretrieve = await interact.WaitForMessageAsync(message => message.Content == "Hello");
        if (messageretrieve.Result.Content == "Hello")
        {
            await context.Channel.SendMessageAsync(
                $"{context.User.Username} was interacted with {messageretrieve.Result.Content}");
        }
    }

    [Command("reaction")]
    public async Task ReactionCommandAsync(CommandContext context)
    {
        var interact = App.Client.GetInteractivity();
        var react = await interact.WaitForReactionAsync(message => message.Message.Id == 1309793629963030570);
        if (react.Result.Message.Id == 1309793629963030570)
        {
            await context.Channel.SendMessageAsync($"{context.User.Username} was reacted with {react.Result.Emoji}");
        }
    }


    [Command("poll")]
    public async Task PollCommandAsync(CommandContext context, string option1, string option2, string option3,
        string option4, [RemainingText] string pollTitle)
    {
        var interact = App.Client.GetInteractivity();
        var pollTime = TimeSpan.FromSeconds(10);
        DiscordEmoji[] emojiOptions =
        {
            DiscordEmoji.FromName(App.Client, ":one:"),
            DiscordEmoji.FromName(App.Client, ":two:"),
            DiscordEmoji.FromName(App.Client, ":three:"),
            DiscordEmoji.FromName(App.Client, ":four:"),
        };
        string optionList = $"{emojiOptions[0]} | {option1} \n" +
                            $"{emojiOptions[1]} | {option2} \n" +
                            $"{emojiOptions[2]} | {option3} \n" +
                            $"{emojiOptions[3]} | {option4}";
        var pollMessage = new DiscordEmbedBuilder
        {
            Color = DiscordColor.Red,
            Title = pollTitle,
            Description = optionList
        };
        var sentPoll = await context.Channel.SendMessageAsync(embed: pollMessage);
        foreach (var emoji in emojiOptions)
        {
            await sentPoll.CreateReactionAsync(emoji);
        }

        var totalReaction = await interact.CollectReactionsAsync(sentPoll, pollTime);
        int count1 = 0;
        int count2 = 0;
        int count3 = 0;
        int count4 = 0;
        foreach (var emoji in totalReaction)
        {
            if (emoji.Emoji == emojiOptions[0])
            {
                count1++;
            }
            if (emoji.Emoji == emojiOptions[1])
            {
                count2++;
            }if (emoji.Emoji == emojiOptions[2])
            {
                count3++;
            }if (emoji.Emoji == emojiOptions[3])
            {
                count4++;
            }
        }
        int totalVotes = count1 + count2 + count3 + count4;
        string resultDescription = $"{emojiOptions[0]} : {count1} \n" +
                                   $"{emojiOptions[1]} : {count2} \n" +
                                   $"{emojiOptions[2]} : {count3} \n" +
                                   $"{emojiOptions[3]} : {count4} \n" + 
                                   $"Total Votes: {totalVotes}";
        var resultEmbed = new DiscordEmbedBuilder
        {
            Color = DiscordColor.Green,
            Title = "Result of the poll",
            Description = resultDescription
        };
        await context.Channel.SendMessageAsync(embed: resultEmbed);

    }
}