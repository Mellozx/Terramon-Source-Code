using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.Moves
{
    public class HealMove : BaseMove
    {
        public override string MoveName => "Heal";

        public override Target Target => Target.Trainer;

        public override int Cooldown => 20 * 60; //20 sec cooldown

        public override int AutoUseWeight(Projectile proj, ParentPokemon mon, Vector2 target, TerramonPlayer player)
        {
            Player pl = Main.player[player.whoAmI];
            if (!(pl.statLife < pl.statLifeMax - (100 * (pl.statLifeMax / 500f))))
                return 0;
            return (int)Math.Round(100 * ((float)pl.statLife / pl.statLifeMax)); // The less hp left, the more chance to cast
        }

        public override bool PerformInWorld(Projectile proj, ParentPokemon mon, Vector2 target, TerramonPlayer player)
        {
            Player pl = Main.player[player.whoAmI];
            if (pl.statLife < pl.statLifeMax - (100 * ((float)pl.statLifeMax / 500f))) //The more hp player have the more hp threshold
            {
                pl.HealEffect(200 * (pl.statLifeMax / 500));
                pl.statLife += 200;
                return true;
            }
            return false;
        }
    }
}
