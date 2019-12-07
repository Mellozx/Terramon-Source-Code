using Terraria.ID;
using WebmilioCommons.Managers;
using Terramon.Players;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terramon.Items.Pokeballs.Inventory
{
    public abstract class BasePokeballItem : TerramonItem, IHasUnlocalizedName
    {
        protected BasePokeballItem(string unlocalizedName, Dictionary<GameCulture, string> displayNames, Dictionary<GameCulture, string> tooltips, int value, int rarity, float catchRate, Color? nameColorOverride = null) :
            base(displayNames, tooltips, 24, 24, value, 0, rarity)
        {
            UnlocalizedName = unlocalizedName;

            CatchRate = catchRate;

            NameColorOverride = nameColorOverride;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.useTime = 16;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = item.useTime;
            item.UseSound = SoundID.Item1;
            item.damage = 1;

            item.scale = 0.6f;
            item.maxStack = 99;
        }


        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(tooltips);

            tooltips.RemoveAll(l => l.Name == "Damage");
            tooltips.RemoveAll(l => l.Name == "CritChance");
            tooltips.RemoveAll(l => l.Name == "Speed");
            tooltips.RemoveAll(l => l.Name == "Knockback");

            if (NameColorOverride != null)
                tooltips.Find(t => t.Name == TooltipLines.ITEM_NAME).overrideColor = NameColorOverride;
        }


        public string UnlocalizedName { get; }

        public float CatchRate { get; }

        public Color? NameColorOverride { get; }
    }
}