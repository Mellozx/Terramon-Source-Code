using System;
using Microsoft.Xna.Framework;
using Terramon.Projectiles;
using Terraria;
using Terraria.ModLoader;

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
			projectile.scale = 0.90f;
        }


        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.ai[0] == 2)
                return true;

            if (projectile.soundDelay == 0)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/ballbounce").WithVolume(.7f));
            }
            projectile.soundDelay = 10;


            if (projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 1f)
            {
                projectile.velocity.X = oldVelocity.X * -0.4f;
            }
            if (projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 1f)
            {
                projectile.velocity.Y = oldVelocity.Y * -0.4f;
            }

            return false;
        }
    }
}