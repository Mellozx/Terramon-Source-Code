using Microsoft.Xna.Framework;
using Terramon.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WebmilioCommons.Managers;

namespace Terramon.Items.Pokeballs.Inventory
{
    public abstract class BasePokeballItem : TerramonItem, IHasUnlocalizedName
    {
        protected BasePokeballItem(string unlocalizedName, string displayName, string tooltip, int value, int rarity, float catchRate) : base(displayName, tooltip, 24, 24, value, 0 , rarity)
        {
            UnlocalizedName = unlocalizedName;

            CatchRate = catchRate;
        }


        public override void SetDefaults()
        {
            base.SetDefaults();

            item.useTime = 16;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = item.useTime;
            item.UseSound = SoundID.Item1;

            item.scale = 0.6f;
            item.maxStack = 99;
        }


        public string UnlocalizedName { get; }

        public float CatchRate { get; }
    }
}