using System.Collections.Generic;
using System.Linq;
using DSharpPlus;

namespace LibCommands
{
    public abstract class Command
    {
        public string Name { get; }
        public string Description { get; }
        public List<string> Usages { get; } = new List<string>();

        private DiscordEmbed _embedUsage;
        public DiscordEmbed EmbedUsage
        {
            get
            {
                if (_embedUsage == null)
                {
                    List<DiscordEmbedField> fields = new List<DiscordEmbedField>
                    {
                        new DiscordEmbedField
                        {
                            Inline = false,
                            Name = "Description",
                            Value = Description
                        }
                    };

                    if (Usages.Count > 0)
                        fields.Add(new DiscordEmbedField
                        {
                            Inline = false,
                            Name = "Usage",
                            Value = string.Concat(Usages.Select(usage =>
                                $"\n{CommandManager.CommandPrefix}{Name} {usage}"))
                        });

                    _embedUsage = new DiscordEmbed
                    {
                        Description = $"_All you need to know about: `{CommandManager.CommandPrefix}{Name}`_",
                        Fields = fields
                    };
                }
                return _embedUsage;
            }
        }

        protected Command(string name, string description)
        {
            Name = name.ToLowerInvariant();
            Description = description;
        }

        protected Command(string name, string description, string usage) : this(name, description)
        {
            Usages.Add(usage);
        }

        protected Command(string name, string description, string[] usages) : this(name, description)
        {
            Usages.AddRange(usages);
        }

        public abstract void Execute(MessageCreateEventArgs ev, string[] args);
    }
}