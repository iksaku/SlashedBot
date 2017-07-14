using System;
using System.Threading.Tasks;
using DSharpPlus;

namespace SlashedBot
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			DotNetEnv.Env.Load();
			Run().GetAwaiter().GetResult();
		}

		public static async Task Run()
		{
			var bot = new DiscordClient(new DiscordConfig
			{
				AutoReconnect = true,
				DiscordBranch = Branch.Stable,
				LargeThreshold = 250,
				LogLevel = LogLevel.Debug,
				Token = Environment.GetEnvironmentVariable("BotToken"),
				TokenType = TokenType.Bot,
				UseInternalLogHandler = true
			});

			await bot.ConnectAsync();
			
			await Task.Delay(-1);
		}
	}
}