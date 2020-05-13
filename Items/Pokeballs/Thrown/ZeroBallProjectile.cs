using System;
using Microsoft.Xna.Framework;
using Terramon.Items.Pokeballs.Inventory;
using Terramon.Items.Pokeballs.Parts;
using Terramon.Pokemon.FirstGeneration.Normal.Beedrill;
using Terramon.Pokemon.FirstGeneration.Normal.Blastoise;
using Terramon.Pokemon.FirstGeneration.Normal.Bulbasaur;
using Terramon.Pokemon.FirstGeneration.Normal.Butterfree;
using Terramon.Pokemon.FirstGeneration.Normal.Caterpie;
using Terramon.Pokemon.FirstGeneration.Normal.Charizard;
using Terramon.Pokemon.FirstGeneration.Normal.Charmander;
using Terramon.Pokemon.FirstGeneration.Normal.Charmeleon;
using Terramon.Pokemon.FirstGeneration.Normal.Dragonair;
using Terramon.Pokemon.FirstGeneration.Normal.Dragonite;
using Terramon.Pokemon.FirstGeneration.Normal.Dratini;
using Terramon.Pokemon.FirstGeneration.Normal.Eevee;
using Terramon.Pokemon.FirstGeneration.Normal.Gastly;
using Terramon.Pokemon.FirstGeneration.Normal.Gengar;
using Terramon.Pokemon.FirstGeneration.Normal.Goldeen;
using Terramon.Pokemon.FirstGeneration.Normal.Haunter;
using Terramon.Pokemon.FirstGeneration.Normal.Horsea;
using Terramon.Pokemon.FirstGeneration.Normal.Ivysaur;
using Terramon.Pokemon.FirstGeneration.Normal.Kakuna;
using Terramon.Pokemon.FirstGeneration.Normal.Magikarp;
using Terramon.Pokemon.FirstGeneration.Normal.Metapod;
using Terramon.Pokemon.FirstGeneration.Normal.Oddish;
using Terramon.Pokemon.FirstGeneration.Normal.Pidgeot;
using Terramon.Pokemon.FirstGeneration.Normal.Pidgeotto;
using Terramon.Pokemon.FirstGeneration.Normal.Pidgey;
using Terramon.Pokemon.FirstGeneration.Normal.Pikachu;
using Terramon.Pokemon.FirstGeneration.Normal.Rattata;
using Terramon.Pokemon.FirstGeneration.Normal.Squirtle;
using Terramon.Pokemon.FirstGeneration.Normal.Venusaur;
using Terramon.Pokemon.FirstGeneration.Normal.Wartortle;
using Terramon.Pokemon.FirstGeneration.Normal.Weedle;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terramon.Items.Pokeballs.Thrown
{
    public class ZeroBallProjectile : BasePokeballProjectile
    {
        public Color zeroballparticles = new Color(22, 100, 148);
        public override void AI()
        {
            for (int i = 0; i < 200; i++)
            {
                NPC target = Main.npc[i];
                if 
                    (
                        target.type == ModContent.NPCType<BulbasaurNPC>() ||
                        target.type == ModContent.NPCType<IvysaurNPC>() ||
                        target.type == ModContent.NPCType<VenusaurNPC>() ||
                        target.type == ModContent.NPCType<CharmanderNPC>() ||
                        target.type == ModContent.NPCType<CharmeleonNPC>() ||
                        target.type == ModContent.NPCType<CharizardNPC>() ||
                        target.type == ModContent.NPCType<SquirtleNPC>() ||
                        target.type == ModContent.NPCType<WartortleNPC>() ||
                        target.type == ModContent.NPCType<BlastoiseNPC>() ||
                        target.type == ModContent.NPCType<CaterpieNPC>() ||
                        target.type == ModContent.NPCType<MetapodNPC>() ||
                        target.type == ModContent.NPCType<ButterfreeNPC>() ||
                        target.type == ModContent.NPCType<WeedleNPC>() ||
                        target.type == ModContent.NPCType<KakunaNPC>() ||
                        target.type == ModContent.NPCType<BeedrillNPC>() ||
                        target.type == ModContent.NPCType<PidgeyNPC>() ||
                        target.type == ModContent.NPCType<PidgeottoNPC>() ||
                        target.type == ModContent.NPCType<PidgeotNPC>() ||
                        target.type == ModContent.NPCType<RattataNPC>() ||
                        // target.type == ModContent.NPCType<RaticateNPC>() ||
                        // target.type == ModContent.NPCType<SpearowNPC>() ||
                        // target.type == ModContent.NPCType<FearowNPC>() ||
                        target.type == ModContent.NPCType<PikachuNPC>() ||
                        target.type == ModContent.NPCType<OddishNPC>() ||
                        target.type == ModContent.NPCType<GastlyNPC>() ||
                        target.type == ModContent.NPCType<HaunterNPC>() ||
                        target.type == ModContent.NPCType<GengarNPC>() ||
                        target.type == ModContent.NPCType<HorseaNPC>() ||
                        target.type == ModContent.NPCType<GoldeenNPC>() ||
                        target.type == ModContent.NPCType<MagikarpNPC>() ||
                        target.type == ModContent.NPCType<EeveeNPC>() ||
                        target.type == ModContent.NPCType<DratiniNPC>() ||
                        target.type == ModContent.NPCType<DragonairNPC>() ||
                        target.type == ModContent.NPCType<DragoniteNPC>() 
                    )
                {
                    float shootToX = target.position.X + (float)target.width * 0.5f - projectile.Center.X;
                    float shootToY = target.position.Y - projectile.Center.Y;
                    float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

                    //If the distance between the live targeted npc and the projectile is less than 480 pixels
                    if (distance < 480f && !target.friendly && target.active)
                    {
                        //Divide the factor, 3f, which is the desired velocity
                        distance = 3f / distance;

                        //Multiply the distance by a multiplier if you wish the projectile to have go faster
                        shootToX *= distance * 5;
                        shootToY *= distance * 5;

                        //Set the velocities to the shoot values
                        projectile.velocity.X = shootToX;
                        projectile.velocity.Y = shootToY;
                    }
                }
            }

            if (projectile.ai[0] == 0)
            {
                if (Main.netMode != NetmodeID.Server)
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/throwball").WithVolume(.7f));
                projectile.ai[0] = 1;
                projectile.ai[1] = 1;

                projectile.netUpdate = true;
            }

            if (projectile.owner == Main.myPlayer && projectile.timeLeft <= 3)
            {
                
                projectile.tileCollide = false;

                projectile.alpha = 255;

                projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
                projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
                projectile.width = 250;
                projectile.height = 250;
                projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
                projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
            }
            else
            {

            }
            projectile.ai[0] += 1f;
            if (projectile.ai[0] > 5f)
            {
                projectile.ai[0] = 10f;

                if (projectile.velocity.Y == 0f && projectile.velocity.X != 0f)
                {
                    projectile.velocity.X = projectile.velocity.X * 0.97f;

                    {
                        projectile.velocity.X = projectile.velocity.X * 0.99f;
                    }
                    if ((double)projectile.velocity.X > -0.01 && (double)projectile.velocity.X < 0.01)
                    {
                        projectile.velocity.X = 0f;
                        projectile.netUpdate = true;
                    }
                }
                projectile.velocity.Y = projectile.velocity.Y + 0.2f;
            }

            projectile.rotation += projectile.velocity.X * 0.025f;
            return;
        }

        public override void Kill(int timeLeft)
        {
            if (Main.netMode != NetmodeID.Server)
                Main.PlaySound(SoundID.Item10, projectile.position);
            Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 60, 0, 0, 0, zeroballparticles);
            Vector2 usePos = projectile.position;

            Vector2 rotVector =
                (projectile.rotation - MathHelper.ToRadians(90f)).ToRotationVector2();
            usePos += rotVector * 16f;

            for (int i = 0; i < 20; i++)
            {
                Dust dust = Dust.NewDustDirect(usePos, projectile.width, projectile.height, 60, 0, 0, 0, zeroballparticles);
                dust.position = (dust.position + projectile.Center) / 2f;
                dust.velocity += rotVector * 2f;
                dust.velocity *= 0.5f;
                dust.noGravity = true;
                usePos -= rotVector * 8f;
            }
        }
    }
}