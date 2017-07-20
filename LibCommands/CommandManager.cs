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
        
        public static IReadOnlyDictionary<string, ulong> ChannelIds = new Dictionary<string, ulong>
        {
            {"iksaku", 335861098005921793}
        };

        private static readonly Dictionary<string, Command> Commands = new Dictionary<string, Command>();
        public static List<Command> CommandList => Commands.Values.ToList();

        public CommandManager(DiscordClient bot, string commandPrefix)
        {
            Bot = bot;
            CommandPrefix = commandPrefix;
        }

        public void HandleMessage(MessageCreateEventArgs ev)
        {
            if (ev.Author.IsBot) return;
            if (ev.Message.Content.Substring(0, 2) != CommandPrefix) return;
            if (ev.Channel.Id != ChannelIds["iksaku"])
            {
                ev.Message.RespondAsync(
                    "I'm sorry but I'm currently under development... Nothing to see here (yet) :sweat_smile:");
                return;
            }

            string[] parts = ev.Message.Content.Split(' ');
            if (!Commands.TryGetValue(parts.First().Substring(2), out var command)) return;
            string[] args = parts.Skip(1).ToArray();

            Bot.DebugLogger.LogMessage(LogLevel.Debug, "CommandManager",
                $"Executing command '{command.Name}' with arguments: [{string.Join(", ", args)}]\nAuthor: '{ev.Author.Username}', Id: {ev.Author.Id} | Channel: '{ev.Channel.Name}', Id: {ev.Channel.Id}", DateTime.Now);

            command.Execute(ev, args);
        }

        public void RegisterCommand(Command command)
        {
            if (Commands.ContainsKey(command.Name))
            {
                Bot.DebugLogger.LogMessage(LogLevel.Warning, "CommandManager",
                    $"Tried to register two commands with the same name: {command.Name}", DateTime.Now);
                return;
            }

            Commands.Add(command.Name, command);
        }

        public void UnregisterCommand(string command)
        {
            command = command.ToLowerInvariant();
            if (!Commands.ContainsKey(command)) return;
            Commands.Remove(command);
        }

        public void UnregisterCommand(Command command)
        {
            string cmd = command.Name.ToLowerInvariant();
            if (!Commands.ContainsKey(cmd)) return;
            Commands.Remove(cmd);
        }
    }
}