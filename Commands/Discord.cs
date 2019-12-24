using Terraria;
using Terraria.ModLoader;

namespace Terramon.Commands
{
    class Discord : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return "discord"; }
        }

        public override string Description
        {
            get { return "Join the Terramon Discord server."; }
        }

        public override string Usage
        {
            get { return "/discord"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            
                if (args.Length == 0)
                {
                    caller.Reply("Opened Discord link...");
                System.Diagnostics.Process.Start("https://discord.gg/MyeY4AM");
                return;
                }
        }
    }
}
