using System.Collections.Generic;
using DSharpPlus;
using LibCommands;

namespace Engine.Commands
{
    public class TestCommand : Command, ITestCommand
    {
        public TestCommand() : base("embed", "Testy embeds!", "[embed|pagination]") {}

        public override void Execute(MessageCreateEventArgs ev, string[] args)
        {
            if (args.Length != 1) ev.Message.RespondAsync(string.Empty, embed: EmbedUsage);
            switch (args[0])
            {
                case "embed":
                    List<DiscordEmbedField> fields = new List<DiscordEmbedField>();
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
                    Engine.Bot.SendMessageAsync(ev.Channel, string.Empty, false, embed);
                    break;
                // TODO: Pagination
                default:
                    ev.Message.RespondAsync(string.Empty, embed: EmbedUsage);
                    break;
            }
        }
    }
}