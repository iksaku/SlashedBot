using DSharpPlus;

namespace LibCommands
{
	public abstract class Command
	{
		public string Name { get; }
		public string Description { get; }
		public string Usage { get; }
		
		protected Command(string name, string description)
		{
			Name = name;
			Description = description;
		}

		protected Command(string name, string description, string usage) : this(name, description)
		{
			Usage = usage;
		}

		public virtual string GetUsage()
		{
			return $"Usage: //{Name} {Usage}";
		} 
		
		public abstract void Execute(DiscordUser sender, string[] args);
	}
}