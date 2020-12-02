using Microsoft.Xna.Framework;
using Razorwing.Framework.Graphics;
using Razorwing.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terramon.Players;
using Terramon.UI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terramon.Pokemon.Moves
{
    public class Absorb : DamageMove
    {
        public override string MoveName => "Absorb";
        public override int Damage => 20;
        public override Target Target => Target.Opponent;
        public override int Cooldown => 60 * 1; //Once per second
        public override PokemonType MoveType => PokemonType.Grass;

        public override int AutoUseWeight(ParentPokemon mon, Vector2 pos, TerramonPlayer player)
        {
            NPC target = GetNearestNPC(pos);
            if (target == null)
                return 0;
            return 30;
        }

        public override bool PerformInWorld(ParentPokemon mon, Vector2 pos, TerramonPlayer player)
        {
            NPC target = GetNearestNPC(pos);
            if (target == null)
                return false;

            player.Attacking = true;
            Vector2 vel = (target.position + (target.Size / 2)) - (mon.projectile.position + (mon.projectile.Size / 2));
            var l = vel.Length();
            vel += target.velocity * (l / 100);//Make predict shoot
            vel.Normalize(); //Direction
            vel *= 15; //Speed
            Projectile.NewProjectile((mon.projectile.position + (mon.projectile.Size / 2)), vel, ProjectileID.DD2PhoenixBowShot, 20, 1f, player.whoAmI);
            return true;
        }

        public int endMoveTimer;
        public const string PROJID_KEY = "move.projID";

        private int spore1;
        private int spore2;
        private int spore3;
        private int spore4;
        private int spore5;
        private int spore6;

        private float damageDealt;
        public override bool AnimateTurn(ParentPokemon mon, ParentPokemon target, TerramonPlayer player, PokemonData attacker,
            PokemonData deffender)
        {
            if (AnimationFrame == 1) //At initial frame we pan camera to attacker
            {
                TerramonMod.ZoomAnimator.ScreenPosX(mon.projectile.position.X + 12, 500, Easing.OutExpo);
                TerramonMod.ZoomAnimator.ScreenPosY(mon.projectile.position.Y, 500, Easing.OutExpo);
            }
            else if (AnimationFrame == 140) //Move animation begin after 140 frames
            {
                BattleMode.UI.splashText.SetText("");

                Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/UI/BattleSFX/" + MoveName).WithVolume(.75f));

                TerramonMod.ZoomAnimator.ScreenPosX(target.projectile.position.X + 12, 500, Easing.OutExpo);
                TerramonMod.ZoomAnimator.ScreenPosY(target.projectile.position.Y, 500, Easing.OutExpo);

                spore1 = Projectile.NewProjectile(target.projectile.Hitbox.Center(), new Vector2(0, 0), ModContent.ProjectileType<AbsorbSpore>(), 0, 0);
                Main.projectile[spore1].maxPenetrate = 99;
                Main.projectile[spore1].penetrate = 99;
            }
            else if (AnimationFrame == 155)
            {
                spore2 = Projectile.NewProjectile(target.projectile.Hitbox.Center(), new Vector2(0, 0), ModContent.ProjectileType<AbsorbSpore>(), 0, 0);
                Main.projectile[spore2].maxPenetrate = 99;
                Main.projectile[spore2].penetrate = 99;
            }
            else if (AnimationFrame == 170)
            {
                spore3 = Projectile.NewProjectile(target.projectile.Hitbox.Center(), new Vector2(0, 0), ModContent.ProjectileType<AbsorbSpore>(), 0, 0);
                Main.projectile[spore3].maxPenetrate = 99;
                Main.projectile[spore3].penetrate = 99;
            }
            else if (AnimationFrame == 185)
            {
                TerramonMod.ZoomAnimator.ScreenPosX(mon.projectile.position.X + 12, 500, Easing.OutExpo);
                TerramonMod.ZoomAnimator.ScreenPosY(mon.projectile.position.Y, 500, Easing.OutExpo);
                spore4 = Projectile.NewProjectile(target.projectile.Hitbox.Center(), new Vector2(0, 0), ModContent.ProjectileType<AbsorbSpore>(), 0, 0);
                Main.projectile[spore4].maxPenetrate = 99;
                Main.projectile[spore4].penetrate = 99;
            }
            else if (AnimationFrame == 200)
            {
                spore5 = Projectile.NewProjectile(target.projectile.Hitbox.Center(), new Vector2(0, 0), ModContent.ProjectileType<AbsorbSpore>(), 0, 0);
                Main.projectile[spore5].maxPenetrate = 99;
                Main.projectile[spore5].penetrate = 99;
            }
            else if (AnimationFrame == 215)
            {
                spore6 = Projectile.NewProjectile(target.projectile.Hitbox.Center(), new Vector2(0, 0), ModContent.ProjectileType<AbsorbSpore>(), 0, 0);
                Main.projectile[spore6].maxPenetrate = 99;
                Main.projectile[spore6].penetrate = 99;
            }
            else if (AnimationFrame == 265)//At Last frame we destroy new proj
            {
                TerramonMod.ZoomAnimator.ScreenPosX(target.projectile.position.X + 12, 500, Easing.OutExpo);
                TerramonMod.ZoomAnimator.ScreenPosY(target.projectile.position.Y, 500, Easing.OutExpo);
                damageDealt = InflictDamage(mon, target, player, attacker, deffender);
                if (PostTextLoc.Args.Length >= 4)//If we can extract damage number
                    CombatText.NewText(target.projectile.Hitbox, CombatText.DamagedHostile, (int)PostTextLoc.Args[3]);//Print combat text at attacked mon position
                Main.projectile[spore1].timeLeft = 0;
                Main.projectile[spore1].active = false;
                Main.projectile[spore2].timeLeft = 0;
                Main.projectile[spore2].active = false;
                Main.projectile[spore3].timeLeft = 0;
                Main.projectile[spore3].active = false;
                Main.projectile[spore4].timeLeft = 0;
                Main.projectile[spore4].active = false;
                Main.projectile[spore5].timeLeft = 0;
                Main.projectile[spore5].active = false;
                Main.projectile[spore6].timeLeft = 0;
                Main.projectile[spore6].active = false;
            }

            else if (AnimationFrame > 140 && AnimationFrame < 265)
            {
                //Vector2 vel = (target.projectile.position + (target.projectile.Size / 2)) - (mon.projectile.position + (mon.projectile.Size / 2));
                //var l = vel.Length();
                //vel.Normalize();
                //Main.projectile[id].position = mon.projectile.position + (vel * (l * (AnimationFrame / 120)));
                if (AnimationFrame < 190) Main.projectile[spore1].position = Interpolation.ValueAt(AnimationFrame, target.projectile.Hitbox.Center(), mon.projectile.Hitbox.Center(), 140, 190,
                    Easing.Out);
                if (AnimationFrame > 155 && AnimationFrame < 205)
                {
                    Main.projectile[spore2].position = Interpolation.ValueAt(AnimationFrame, target.projectile.Hitbox.Center(), mon.projectile.Hitbox.Center(), 155, 205,
                    Easing.Out);
                }
                if (AnimationFrame > 170 && AnimationFrame < 220)
                {
                    Main.projectile[spore3].position = Interpolation.ValueAt(AnimationFrame, target.projectile.Hitbox.Center(), mon.projectile.Hitbox.Center(), 170, 220,
                    Easing.Out);
                }
                if (AnimationFrame > 185 && AnimationFrame < 235)
                {
                    Main.projectile[spore4].position = Interpolation.ValueAt(AnimationFrame, target.projectile.Hitbox.Center(), mon.projectile.Hitbox.Center(), 185, 235,
                    Easing.Out);
                }
                if (AnimationFrame > 200 && AnimationFrame < 250)
                {
                    Main.projectile[spore5].position = Interpolation.ValueAt(AnimationFrame, target.projectile.Hitbox.Center(), mon.projectile.Hitbox.Center(), 200, 250,
                    Easing.Out);
                }
                if (AnimationFrame > 215 && AnimationFrame < 265)
                {
                    Main.projectile[spore6].position = Interpolation.ValueAt(AnimationFrame, target.projectile.Hitbox.Center(), mon.projectile.Hitbox.Center(), 215, 265,
                    Easing.Out);
                }
            }

            if (AnimationFrame > 295)
            {
                if (!BattleMode.UI.HP1.AdjustingHP() && !BattleMode.UI.HP1.AdjustingHP())
                {
                    endMoveTimer++;
                    if (endMoveTimer >= 100 && endMoveTimer < 200)
                    {
                        if (player?.Battle.State == BattleState.BattleWithWild) BattleMode.UI.splashText.SetText($"Sucked life from the wild {deffender.PokemonName}!");
                        //TerramonMod.ZoomAnimator.ScreenPos(mon.projectile.position + new Vector2(12, 0), 500, Easing.OutExpo);
                        TerramonMod.ZoomAnimator.ScreenPosX(mon.projectile.position.X + 12, 500, Easing.OutExpo);
                        TerramonMod.ZoomAnimator.ScreenPosY(mon.projectile.position.Y, 500, Easing.OutExpo);
                        //BattleMode.animWindow = 0;
                    }
                    if (endMoveTimer == 200)
                    {
                        BattleMode.UI.splashText.SetText("");
                        attacker.HP += (int)damageDealt / 2;
                        CombatText.NewText(mon.projectile.Hitbox, CombatText.HealLife, (int)damageDealt / 2);
                    }
                    if (endMoveTimer >= 300)
                    {
                        endMoveTimer = 0;
                        return false;
                    }
                }
            }

            return true;
        }
    }

    public class AbsorbSpore : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.alpha = 0;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 10000;
        }

        private int timer;

        public override void AI()
        {
            timer++;
            if (timer >= 8)
            {
                Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 75, 0f, 0f, 0);
                dust.velocity *= 0.4f;
                dust.velocity += projectile.velocity;
                dust.noGravity = true;
                timer = 0;
            }
        }
    }
}
