using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Interactivity;
using Engine.Commands;
using LibCommands;

namespace Engine
{
    public class Engine
    {
        public static DiscordClient Bot;
        public static CommandManager CommandManager;

        public static void Main(string[] args)
        {
            string token = string.Empty;
            try
            {
                DotNetEnv.Env.Load();

                token = Environment.GetEnvironmentVariable("BotToken");

                if (string.IsNullOrEmpty(token)) throw new Exception("Bot Token not available");
            }
            catch (Exception)
            {
                Console.WriteLine("[Error] Bot Token not available");
                Environment.Exit(1);
            }

            Bot = new DiscordClient(new DiscordConfig
            {
                AutoReconnect = true,
                DiscordBranch = Branch.Stable,
                LargeThreshold = 250,
                LogLevel = LogLevel.Debug,
                Token = token,
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true
            });
            Bot.UseInteractivity();

            CommandManager = new CommandManager(Bot, "//");
            RegisterCommands();

            Run().GetAwaiter().GetResult();
        }

        public static async Task Run()
        {
            await Bot.ConnectAsync();

            Bot.MessageCreated += async (ev) =>
            {
                CommandManager.HandleMessage(ev);

                await Task.Delay(0);
            };

            await Task.Delay(-1);
        }

        public static void RegisterCommands()
        {
            List<Command> commands = new List<Command>
            {
                new HelpCommand(),
                new PingCommand(),
                new TestCommand()
            };

            commands.ForEach(CommandManager.RegisterCommand);
        }
    }
}