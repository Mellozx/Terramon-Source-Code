using Terraria;
using Terraria.ModLoader;

namespace Terramon.Items.MiscItems
{
    public class Pokedex : ModItem
    {
        public int Timer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pokédex");
            Tooltip.SetDefault("It's a digital encyclopedia created by [c/33FF33:Professor Oak] as an invaluable tool to Trainers."
                + "\n[c/FFFF66:Use it to communicate with the Professor!]"
                    + "\nWhile this item is in your inventory, use [c/FF3333:/pokedex] to open the dex menu."
                        + "\n[c/33FF33:The Professor may have new things to say once in a while.]"
                            + "\nOh, and you probably shouldn't get rid of it.");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 32;
            item.scale = 0.8f;
            item.maxStack = 1;
            item.useTime = 34;
            item.useAnimation = 34;
            item.useStyle = 4;
            item.knockBack = 0;
            item.value = 0;
            item.rare = -12;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/pokedexon");
            item.autoReuse = false;
        }

        public override bool UseItem(Player player)
        {
            Timer++;
            if (Timer == 1)
            {
                Main.NewText("[c/FFFF66:Calling the Professor...] [c/C0C0C0:(Click to proceed)]");
            }
            if (Timer == 2)
            {
                Main.NewText("[c/33FF33:Professor Oak:] Hey there, Trainer! I'm assuming you've safely arrived in the land of Terraria. [c/C0C0C0:(Click to proceed)]");
            }
            if (Timer == 3)
            {
                Main.NewText("[c/33FF33:Professor Oak:] Many subregions are waiting to be discovered, and I need you to catch every last Pokémon so I can finally complete my research! [c/C0C0C0:(Click to proceed)]");
            }
            if (Timer == 4)
            {
                Main.NewText("[c/33FF33:Professor Oak:] I packed you 8 [c/FF3333:Poké Balls] that I hope you will or have used wisely. [c/C0C0C0:(Click to proceed)]");
            }
            if (Timer == 5)
            {
                Main.NewText("[c/33FF33:Professor Oak:] Lastly, use [c/FF3333:/choose] to pick your starting Pokémon. Good luck on your journey! [c/C0C0C0:(Click to proceed)]");
            }
            if (Timer == 6)
            {
                Main.NewText("[c/FFFF66:Call ended.]");
                Timer = 0;
            }
            return true;
        }
    }
}
