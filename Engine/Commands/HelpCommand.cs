using System;
using System.Collections.Generic;
using System.Linq;
using DSharpPlus;
using DSharpPlus.Interactivity;
using LibCommands;

namespace Engine.Commands
{
    public class HelpCommand : Command
    {
        private List<Page> _helpPages;
        
        public HelpCommand() : base("help", "Get a list of available commands", "[command]") {}

        public override async void Execute(MessageCreateEventArgs ev, string[] args)
        {
            List<Command> commands = CommandManager.CommandList.Where(cmd => !(cmd is ITestCommand)).ToList();
            switch (args.Length)
            {
                case 0:
                    if (_helpPages == null)
                    {
                        _helpPages = new List<Page>();
                        int cmdCount = 0;
                        commands.ForEach(cmd =>
                        {
                            if (cmdCount >= 6 || _helpPages.Count < 1)
                            {
                                cmdCount = 0;
                                _helpPages.Add(new Page
                                {
                                    Embed = new DiscordEmbed
                                    {
                                        Description = "Here are some of the available commands:",
                                    }
                                });
                            }
                            _helpPages.Last().Embed.Fields.Add(new DiscordEmbedField
                            {
                                Inline = false,
                                Name = $"{CommandManager.CommandPrefix}{cmd.Name}",
                                Value = cmd.Description
                            });
                            ++cmdCount;
                        });
                        if (_helpPages.Count > 1)
                        {
                            _helpPages.ForEach(p =>
                            {
                                p.Embed.Footer = new DiscordEmbedFooter
                                {
                                    Text = $"Page {_helpPages.FindIndex(i => i.Equals(p))+1} of {_helpPages.Count}"
                                };
                            });
                        }
                    }
                    
                    if (_helpPages.Count > 1)
                    {
                        await Engine.Bot.GetInteractivityModule().SendPaginatedMessage(ev.Channel, ev.Author, _helpPages,
                            TimeSpan.FromMinutes(5), TimeoutBehaviour.Ignore);
                    }
                    else
                    {
                        await ev.Message.RespondAsync(string.Empty, embed: _helpPages.Last().Embed);
                    }
                    break;
                case 1:
                    Command command = commands.SingleOrDefault(cmd =>
                        cmd.Name.Equals(args[0], StringComparison.CurrentCultureIgnoreCase));

                    if (command == null)
                    {
                        await ev.Message.RespondAsync($"Command _`{args[0]}`_ not found");
                        return;
                    }

                    await ev.Message.RespondAsync(string.Empty, embed: command.EmbedUsage);
                    break;
                default:
                    await ev.Message.RespondAsync(string.Empty, embed: EmbedUsage);
                    break;
            }
        }
    }
}