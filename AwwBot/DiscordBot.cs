using Discord;
using Discord.Commands;
using System;
using System.IO;
using RedditSharp;
using System.Linq;

namespace AwwBot
{
    public class AwwBot
    {
        DiscordClient client;
        CommandService commands;

        public AwwBot()
        {

            client = new DiscordClient(input =>
            {
                input.LogLevel = LogSeverity.Info;
                input.LogHandler = Log;
            });

            client.UsingCommands(input =>
            {
                input.PrefixChar = Convert.ToChar("!");
                input.AllowMentionPrefix = true;
            });

            commands = client.GetService<CommandService>();

            commands.CreateCommand("help").Do(async (e) =>
            {
                await e.Channel.SendMessage("For a r/aww picture, type !aww. For a r/rarepuppers picture, type !pupper. For a picture of Luna, type !Luna");
            });

            commands.CreateCommand("aww").Do(async (e) =>
            {
                await e.Channel.SendMessage(RedditAww());
            });


            commands.CreateCommand("Luna").Do(async (e) =>
            {
                await e.Channel.SendFile(LunaPics());
            });

            client.ExecuteAndWait(async () =>
            {
                await client.Connect("MzA1MTMxOTkzMDc3OTA3NDY3.C9wv1A.PM_jAGFN7DwM2r8Ln3mmw05q4Ak", TokenType.Bot);
            });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private string RedditAww()
        {
            var reddit = new Reddit();
            var subreddit = reddit.GetSubreddit("r/aww");
            var post = subreddit.Posts.Where(x => !x.IsSelfPost && x.CreatedUTC >= DateTime.UtcNow.AddDays(-1)).Take(100);
            return post.ElementAt(new Random().Next(0, 99)).Url.OriginalString;
        }

        private string LunaPics()
        {
            var images = Directory.GetFiles("images");
            var randomNumber = new Random().Next(0, images.Length);
            return images[randomNumber];
        }

    }
}
