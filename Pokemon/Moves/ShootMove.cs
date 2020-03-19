using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.Moves
{
    public class ShootMove : BaseMove
    {
        public override string MoveName => "Shoot";
        public override Target Target => Target.Opponent;

        public override bool PerformInWorld(Projectile proj, ParentPokemon mon, Vector2 pos, TerramonPlayer player)
        {
            NPC target = GetNearestNPC(pos);
            if (target == null)
                return false;
            Vector2 vel = (target.position - proj.position);
            vel.Normalize();//Direction
            vel *= 15;//Speed
            Projectile.NewProjectile(proj.position, vel, ProjectileID.ChlorophyteArrow, 20, 1f, player.whoAmI);
            return true;
        }
    }
}
