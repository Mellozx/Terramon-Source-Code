using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.ObjectData;

namespace Terramon.Tiles.Statues
{
    //Statue Tile

	public class ApricornStatue : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileShine[Type] = 1100;
			Main.tileSolid[Type] = false;
			Main.tileSolidTop[Type] = false;
			Main.tileFrameImportant[Type] = true;
            minPick = 0;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
            TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(Type);

			AddMapEntry(new Color(53, 79, 17), Language.GetText("Statue"));
		}

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 48, mod.ItemType("ApricornStatueItem"));
        }
    }
    
    //Statue Item

        public class ApricornStatueItem : ModItem
        {
        public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("Apricorn Statue");
            }

            public override void SetDefaults()
            {
                item.width = 12;
                item.height = 12;
                item.maxStack = 99;
                item.value = 1000;
                item.useTurn = true;
                item.autoReuse = true;
                item.useAnimation = 15;
                item.useTime = 10;
                item.useStyle = 1;
                item.consumable = true;
                item.createTile = mod.TileType("ApricornStatue");
            }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("BlackApricorn"));
            recipe.AddIngredient(ItemID.StoneBlock, 50);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}