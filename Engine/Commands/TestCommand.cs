using System;
using System.Collections.Generic;
using System.Linq;
using DSharpPlus;
using DSharpPlus.Interactivity;
using LibCommands;

namespace Engine.Commands
{
    public class TestCommand : Command, ITestCommand
    {
        public TestCommand() : base("test", "Testy tests!", "[embed|pagination]") {}

        public override async void Execute(MessageCreateEventArgs ev, string[] args)
        {
            if (args.Length != 1)
            {
                await ev.Message.RespondAsync(string.Empty, embed: EmbedUsage);
                return;
            }
            List<DiscordEmbedField> fields = new List<DiscordEmbedField>();
            switch (args[0])
            {
                case "embed":
                    for (int i = 1; i <= 6; i++)
                    {
                        fields.Add(new DiscordEmbedField()
                        {
                            Inline = true,
                            Name = $"Field {i}",
                            Value = $"Value {i}"
                        });
                    }
            
                    DiscordEmbed embed = new DiscordEmbed()
                    {
                        Title = string.Empty,
                        Description = string.Empty,
                        Fields = fields,
                        Color = 0xff0000,
                        Thumbnail = new DiscordEmbedThumbnail
                        {
                            Url = "https://s-media-cache-ak0.pinimg.com/736x/3e/9b/fe/3e9bfe4b53875be9cb327e22ffb5d7e2--pokemon-eevee-evolutions-mega-eevee.jpg"
                        }
                    };
                    await Engine.Bot.SendMessageAsync(ev.Channel, string.Empty, false, embed);
                    break;
                // TODO: Pagination test
                default:
                    await ev.Message.RespondAsync(string.Empty, embed: EmbedUsage);
                    break;
            }
        }
    }
}