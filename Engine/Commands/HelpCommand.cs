using System;
using System.Collections.Generic;
using System.Linq;
using DSharpPlus;
using LibCommands;

namespace Engine.Commands
{
    public class HelpCommand : Command
    {
        public HelpCommand() : base("help", "Get a list of available commands", "[command]") {}

        public override void Execute(MessageCreateEventArgs ev, string[] args)
        {
            List<Command> commands = CommandManager.CommandList.Where(cmd => !(cmd is ITestCommand)).ToList();
            switch (args.Length)
            {
                case 0: // TODO: Paginatio
                    /*ev.Message.RespondAsync("Available commands:" + string.Concat(commands.Select(
                                                cmd =>
                                                    $"\n - `{CommandManager.CommandPrefix}{cmd.Name}` {cmd.Description}")));*/
                    
                    List<DiscordEmbedField> fields = new List<DiscordEmbedField>();
                    commands.ForEach(cmd =>
                    {
                        fields.Add(new DiscordEmbedField
                        {
                            Inline = false,
                            Name = $"{CommandManager.CommandPrefix}{cmd.Name}",
                            Value = cmd.Description
                        });
                    });
                    ev.Message.RespondAsync(string.Empty, embed: new DiscordEmbed
                    {
                        Description = "_Here are some available commands:_",
                        Fields = fields
                    });
                    break;
                case 1:
                    Command command = commands.SingleOrDefault(cmd =>
                        cmd.Name.Equals(args[0], StringComparison.CurrentCultureIgnoreCase));

                    if (command == null)
                    {
                        ev.Message.RespondAsync($"Command _`{args[0]}`_ not found");
                        return;
                    }

                    /*ev.Message.RespondAsync(
                        $"Showing help for `{command.Name}` command:\n - Description: {command.Description}\n - {command.GetUsage()}");*/

                    ev.Message.RespondAsync(string.Empty, embed: command.EmbedUsage);
                    break;
                default:
                    ev.Message.RespondAsync(string.Empty, embed: EmbedUsage);
                    break;
            }
        }
    }
}