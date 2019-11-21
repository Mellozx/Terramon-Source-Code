using Terramon.Players;
using Terramon.UI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terramon.Items.MiscItems
{
    public class Suitcase : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pokécase");
            Tooltip.SetDefault("It's a suitcase from Prof. Oak."
                + "\nUse it to select your [c/98FB98:starter Pokémon.]" //98FB98 = Green
                    + "\n(This item is useless after it has been used.)");
        }
        public override void SetDefaults()
        {
            item.width = 46;
            item.height = 44;
            item.scale = 1f;
            item.maxStack = 1;
            item.useTime = 34;
            item.useAnimation = 34;
            item.useStyle = 4;
            item.knockBack = 0;
            item.value = 0;
            item.rare = -11;
            item.UseSound = new Terraria.Audio.LegacySoundStyle(SoundID.MenuOpen, 0);
            item.autoReuse = false;
        }

        public override bool CanUseItem(Player player)
        {
            TerramonPlayer TerramonPlayer = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
            return !TerramonPlayer.StarterChosen;
        }

        public override bool UseItem(Player player)
        {
            TerramonPlayer TerramonPlayer = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
            if (TerramonPlayer.StarterChosen == false)
            {
                ChooseStarter.Visible = true;
            }
            return false;
        }
    }
}
