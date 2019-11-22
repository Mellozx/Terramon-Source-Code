using Microsoft.Xna.Framework;
using Terramon.Items.Pokeballs.Thrown;
using Terramon.ModCompatibilities;
using Terramon.Players;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Items.Pokeballs.Inventory
{
    public abstract class BaseThrowablePokeballItem<T> : BasePokeballItem where T : BasePokeballProjectile
    {
        protected BaseThrowablePokeballItem(string unlocalizedName, string displayName, string tooltip, int value, int rarity, float catchRate) : 
            base(unlocalizedName, displayName, tooltip, value, rarity, catchRate)
        {
        }


        public override void SetDefaults()
        {
            base.SetDefaults();

            item.shoot = ModContent.ProjectileType<T>();
            item.shootSpeed = 10f;

            item.ranged = true;
            item.autoReuse = false;
            item.consumable = true;
            item.noUseGraphic = true;
        }


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            TerramonPlayer terramonPlayer = TerramonPlayer.Get(player);

            OnPokeballThrown(terramonPlayer);

            if (TerramonMod.Instance.AchievementLibLoaded)
                OnCheckShootAchievements(terramonPlayer, TerramonMod.Instance.AchievementLibCompatibility, terramonPlayer.GetThrownPokeballsCount(this));

            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        protected virtual void OnPokeballThrown(TerramonPlayer terramonPlayer) => terramonPlayer.IncrementThrownPokeballs(this);

        protected virtual void OnCheckShootAchievements(TerramonPlayer terramonPlayer, AchievementLibCompatibility compatibility, int thrownPokeballsCount) { }
    }
}