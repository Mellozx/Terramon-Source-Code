using Terramon.Projectiles;

namespace Terramon.Items.Pokeballs.Thrown
{
    public abstract class BasePokeballProjectile : TerramonProjectile
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.damage = 1;
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 300;
			projectile.light = 1f;
        }
    }
}