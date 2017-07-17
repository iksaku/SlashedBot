using DSharpPlus;

namespace LibCommands
{
    public abstract class Command
    {
        public string Name { get; }
        public string Description { get; }
        public string Usage { get; } = string.Empty;

        protected Command(string name, string description)
        {
            Name = name.ToLowerInvariant();
            Description = description;
        }

        protected Command(string name, string description, string usage) : this(name, description)
        {
            Usage = usage;
        }

        public virtual string GetUsage()
        {
            return $"Usage: `{CommandManager.CommandPrefix}{Name} {Usage}`";
        }

        public abstract void Execute(MessageCreateEventArgs ev, string[] args);
    }
}