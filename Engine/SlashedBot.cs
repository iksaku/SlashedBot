using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus;
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
			DotNetEnv.Env.Load();

			var token = Environment.GetEnvironmentVariable("BotToken");

			if (string.IsNullOrEmpty(token))
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
			
			Run().GetAwaiter().GetResult();
		}

		public static async Task Run()
		{
			await Bot.ConnectAsync();
			
			CommandManager = new CommandManager(Bot, "//");
			RegisterCommands();
			
			Bot.MessageCreated += async (ev) =>
			{
				// Do not reply if Bot message
				if (ev.Author.IsBot) return;
				
				CommandManager.HandleMessage(ev);
				
				await Task.Delay(0);
			};
			
			await Task.Delay(-1);
		}

		public static void RegisterCommands()
		{
			var commands = new List<Command>()
			{
				new HelpCommand(),
				new PingCommand()
			};
			
			commands.ForEach(CommandManager.RegisterCommand);
		}
	}
}