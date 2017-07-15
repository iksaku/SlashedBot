using System;
using System.Collections.Generic;
using System.Linq;
using DSharpPlus;

namespace LibCommands
{
	public class CommandManager
	{
		public DiscordClient Bot { get; }
		public static string CommandPrefix { get; private set; }
		
		private static readonly Dictionary<string, Command> Commands = new Dictionary<string, Command>();
		public static List<Command> CommandList => Commands.Values.ToList();

		public CommandManager(DiscordClient bot, string commandPrefix)
		{
			Bot = bot;
			CommandPrefix = commandPrefix;
		}
		
		public void HandleMessage(MessageCreateEventArgs ev)
		{
			if (ev.Message.Content.Substring(0, 2) != CommandPrefix) return;
			
			var parts = ev.Message.Content.Split(' ');
			if (!Commands.TryGetValue(parts.First().Substring(2), out var command)) return;
			var args = parts.Where(s => !s.Equals(parts.First())).ToArray();
			
			command.Execute(ev, args);
		}

		public void RegisterCommand(Command command)
		{
			var cmd = command.Name.ToLowerInvariant();
			if (Commands.ContainsKey(cmd))
			{
				Bot.DebugLogger.LogMessage(LogLevel.Warning, "CommandHandler",
					"[Warning] Tried to register two commands with the same name", DateTime.Now);
				return;
			}
			
			Commands.Add(cmd, command);
		}

		public void UnregisterCommand(string command)
		{
			command = command.ToLowerInvariant();
			if (!Commands.ContainsKey(command)) return;
			Commands.Remove(command);
		}

		public void UnregisterCommand(Command command)
		{
			var cmd = command.Name.ToLowerInvariant();
			if (!Commands.ContainsKey(cmd)) return;
			Commands.Remove(cmd);
		}
	}
}