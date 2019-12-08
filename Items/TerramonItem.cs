using System.Collections.Generic;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using WebmilioCommons.Items.Standard;

namespace Terramon.Items
{
    public abstract class TerramonItem : StandardItem
    {
        protected TerramonItem(string displayName, string tooltip, int width, int height, int value = 0, int defense = 0, int rarity = ItemRarityID.White, int maxStack = 999) :
            this(
                new Dictionary<GameCulture, string>()
                {
                    { GameCulture.English, displayName }
                },
                new Dictionary<GameCulture, string>()
                {
                    { GameCulture.English, tooltip }
                }, 
                width, height, value, defense, rarity, maxStack
            )
        {
        }


        protected TerramonItem(Dictionary<GameCulture, string> displayNames, Dictionary<GameCulture, string> tooltips, int width, int height, int value = 0, int defense = 0, int rarity = ItemRarityID.White, int maxStack = 999) :
            base(displayNames, tooltips, width, height, value: value, defense: defense, rarity: rarity, maxStack: maxStack)
        {
        }
    }
}