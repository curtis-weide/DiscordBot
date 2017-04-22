using Discord;
using Discord.Commands;
using System;

namespace AwwBot
{
    public class DiscordBot
    {
        DiscordClient client;
        CommandService commands;

        public DiscordBot()
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

            client.ExecuteAndWait(async () =>
            {
                await client.Connect("MzA1MTMxOTkzMDc3OTA3NDY3.C9wv1A.PM_jAGFN7DwM2r8Ln3mmw05q4Ak", TokenType.Bot);
            });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
