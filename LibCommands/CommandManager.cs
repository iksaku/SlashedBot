using System;
using System.Collections.Generic;
using System.Linq;
using DSharpPlus;

namespace LibCommands
{
	public class CommandManager
	{
		public DiscordClient Bot;
		private readonly Dictionary<string, Command> _commands = new Dictionary<string, Command>();

		public CommandManager(DiscordClient bot)
		{
			Bot = bot;
			
			// TODO: Create Commands
		}
		
		public void HandleMessage(MessageCreateEventArgs e)
		{
			if (e.Message.Content.Substring(0, 2) != "//") return;
			
			var parts = e.Message.Content.Split(' ');
			var args = parts.Where(s => !s.Equals(parts.First())).ToArray();

			if (!_commands.TryGetValue(parts.First().Substring(2), out var command)) return;
			command.Execute(e.Author, args);
		}

		private void RegisterCommand(Command command)
		{
			var cmd = command.Name.ToLowerInvariant();
			if (_commands.ContainsKey(cmd))
			{
				Bot.DebugLogger.LogMessage(LogLevel.Warning, "CommandHandler",
					"[Warning] Tried to register two commands with the same name", DateTime.Now);
				return;
			}
			
			_commands.Add(cmd, command);
		}

		private void UnregisterCommand(string command)
		{
			command = command.ToLowerInvariant();
			if (!_commands.ContainsKey(command)) return;
			_commands.Remove(command);
		}

		private void UnregisterCommand(Command command)
		{
			var cmd = command.Name.ToLowerInvariant();
			if (!_commands.ContainsKey(cmd)) return;
			_commands.Remove(cmd);
		}
	}
}