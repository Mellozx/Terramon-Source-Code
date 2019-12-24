using Terraria;
using Terraria.ModLoader;

namespace Terramon.Commands
{
    class Wiki : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return "wiki"; }
        }

        public override string Description
        {
            get { return "Launch the Terramon wiki."; }
        }

        public override string Usage
        {
            get { return "/wiki"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            
                if (args.Length == 0)
                {
                    caller.Reply("Opened Wiki...");
                System.Diagnostics.Process.Start("https://terrariamods.gamepedia.com/Terramon");
                return;
                }
        }
    }
}
