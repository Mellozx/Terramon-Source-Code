using Microsoft.Xna.Framework;
using Terramon.Players;
using Terraria;
using Terraria.ID;

namespace Terramon.Pokemon.Moves
{
    public class ShootMove : BaseMove
    {
        public override string MoveName => "Shoot";
        public override Target Target => Target.Opponent;
        public override int Cooldown => 60 * 1; //Once per second

        public override int AutoUseWeight(Projectile proj, ParentPokemon mon, Vector2 pos, TerramonPlayer player)
        {
            NPC target = GetNearestNPC(pos);
            if (target == null)
                return 0;
            return 30;
        }

        public override bool PerformInWorld(Projectile proj, ParentPokemon mon, Vector2 pos, TerramonPlayer player)
        {
            NPC target = GetNearestNPC(pos);
            if (target == null)
                return false;
            player.Attacking = true;
            Vector2 vel = (target.position + (target.Size/2)) - (proj.position + (proj.Size/2));
            var l = vel.Length();
            vel += target.velocity * (l / 100);//Make predict shoot
            vel.Normalize(); //Direction
            vel *= 15; //Speed
            Projectile.NewProjectile((proj.position + (proj.Size / 2)), vel, ProjectileID.DD2PhoenixBowShot, 20, 1f, player.whoAmI);
            return true;
        }

    }
}