using Terramon.UI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terramon.MiscItems
{
    public class PokeGear : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Poké Gear");
            Tooltip.SetDefault("It's a suitcase from Prof. Oak."
                + "\nUse it to select your [c/98FB98:starter Pokémon.]" //98FB98 = Green
                    + "\n(This item is useless after it has been used.)");
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 20;
            item.scale = 1f;
            item.maxStack = 1;
            item.useTime = 34;
            item.useAnimation = 34;
            item.useStyle = 4;
            item.knockBack = 0;
            item.value = 0;
            item.rare = 9;
            item.UseSound = new Terraria.Audio.LegacySoundStyle(SoundID.MenuOpen, 0);
            item.autoReuse = false;
        }

       
        public override bool UseItem(Player player)
        {
            PokegearUI.Visible = true;
            return false;
        }
    }
}
