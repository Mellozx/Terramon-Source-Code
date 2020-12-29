using System.Diagnostics;
using Terramon.Players;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Commands
{
    internal class Wiki : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "wiki";

        public override string Description => "Launch the Terramon wiki.";

        public override string Usage => "/wiki";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (args.Length == 0)
            {
                TerramonPlayer modPlayer = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
                modPlayer.PartySlot1.HP = 2;
                caller.Reply("Opened Wiki...");
                //Process.Start("https://terrariamods.gamepedia.com/Terramon");
            }
        }
    }
}