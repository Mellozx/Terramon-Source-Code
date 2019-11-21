using Terraria;
using Terraria.ModLoader;

namespace Terramon.Items.MiscItems.GameBoys2
{
	public class GameBoyPink : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Game Boy (Pink)");
			Tooltip.SetDefault("It's an 8-bit handheld console."
				+ "\n[c/33ceff:Equip this to listen to music from Pokémon Fire Red!]"
					+ "\n[c/FFFF66:Soundtrack: Pokémon Center]");
		} 
		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 32;
			item.scale = 1f;
			item.maxStack = 1;
			item.useTime = 34;
			item.useAnimation = 34;
			item.useStyle = 4;
			item.knockBack = 0;
			item.value = 50000;
			item.rare = 0;
			item.accessory = true;
			item.autoReuse = false;
		}
		public override void UpdateAccessory(Player player, bool hideVisual) {
			Main.musicBox2 = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/GB_PokemonCenter");
		}
	}
}
