using DSharpPlus;
using LibCommands;

namespace Engine.Commands
{
    public class PingCommand : Command
    {
        public PingCommand() : base("ping", "Pong!")
        {
        }

        public override void Execute(MessageCreateEventArgs ev, string[] args)
        {
            ev.Message.RespondAsync("Pong!");
        }
    }
}