using System;
using System.Linq;
using DSharpPlus;
using LibCommands;

namespace Engine.Commands
{
    public class HelpCommand : Command
    {
        public HelpCommand() : base("help", "Get a list of available commands", "[command]")
        {
        }

        public override void Execute(MessageCreateEventArgs ev, string[] args)
        {
            switch (args.Length)
            {
                case 0:
                    ev.Message.RespondAsync("Available commands:" + string.Concat(CommandManager.CommandList.Select(
                                                cmd =>
                                                    $"\n - `{CommandManager.CommandPrefix}{cmd.Name}` {cmd.Description}")));
                    break;
                case 1:
                    Command command = CommandManager.CommandList.SingleOrDefault(cmd =>
                        cmd.Name.Equals(args[0], StringComparison.CurrentCultureIgnoreCase));

                    if (command == null)
                    {
                        ev.Message.RespondAsync($"Command `{args[0]}` not found");
                        return;
                    }

                    ev.Message.RespondAsync(
                        $"Showing help for `{command.Name}` command:\n - Description: {command.Description}\n - {command.GetUsage()}");
                    break;
                default:
                    ev.Message.RespondAsync(GetUsage());
                    break;
            }
        }
    }
}