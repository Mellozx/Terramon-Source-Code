using Microsoft.Xna.Framework;
using System;
using System.Text.RegularExpressions;
using Terramon.Items.Pokeballs.Inventory;
using Terramon.Items.Pokeballs.Thrown;
using Terramon.Network.Catching;
using Terramon.Pokemon.FirstGeneration.Normal._caughtForms;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terramon.Pokemon
{
    public abstract class ParentPokemonNPC : ModNPC
    {
        private readonly string[] ballProjectiles = TerramonMod.GetBallProjectiles();
        private readonly float[][] catchChances = TerramonMod.GetCatchChances();
        private readonly string nameMatcher = "([a-z](?=[A-Z]|[0-9])|[A-Z](?=[A-Z][a-z]|[0-9])|[0-9](?=[^0-9]))";

        public abstract Type HomeClass();

        private int ballUsage = 0;

        public string PokeName() => Regex.Replace(HomeClass().Name, nameMatcher, "$1 ");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(PokeName());
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.Bunny];
        }

        public override void SetDefaults()
        {
            npc.defense = 0;
            npc.lifeMax = 15;
            npc.lifeRegen = 15;
            npc.knockBackResist = 0.5f;

            npc.value = 0f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/capturepokemon");

            npc.aiStyle = 7;
            aiType = NPCID.Bunny;

            animationType = NPCID.Bunny;


        }

        private string GetSmallSpritePath(NPC npc)
        {
            return "Terramon/Minisprites/Regular/mini" + npc.TypeName;
        }

        public override bool? CanBeHitByItem(Player player, Item item) => false;

        private bool? CanBeHitByPlayer(Player player) => false; // what is this?

        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            foreach (string ballProjectile in ballProjectiles)
            {
                if (projectile.type == mod.ProjectileType(ballProjectile) && projectile.ai[1] == 1)
                {
                    return true;
                }
            }

            return false;
        }

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {

        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.player.ZoneOverworldHeight)
            {
                return 0.1f;
            }
            else
            {
                return 0f;
            }
        }

        public override void UpdateLifeRegen(ref int damage) { npc.life = npc.lifeMax; }

        // this method will be improved later
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            knockback = 0;
            crit = false;

            for (int i = 0; i < ballProjectiles.Length; i++)
            {
                if (projectile.type == mod.ProjectileType(ballProjectiles[i]) && projectile.ai[1] == 1)
                {
                    if (ballProjectiles[i] == "MasterBallProjectile") // Master Ball never fails
                    {
                        Catch(ref projectile, ref crit, ref damage, ModContent.ItemType<MasterBallCaught>());
                        return;
                    }
                    if (ballProjectiles[i] == "DuskBallProjectile") // Special Condition day/night
                    {
                        ballUsage++;
                        if ((!Main.dayTime && Main.rand.NextFloat() < catchChances[i][0]) ||
                            (Main.dayTime && Main.rand.NextFloat() < catchChances[i][1]))
                        {
                            Catch(ref projectile, ref crit, ref damage, ModContent.ItemType<DuskBallCaught>());
                            return;
                        }
                    }
                    else
                    if (ballProjectiles[i] == "QuickBallProjectile") // Special Condition
                    {
                            ballUsage++;
                            if (ballUsage == 1) // 2x catch chance
                            {
                                if (Main.rand.NextFloat() < .2380f)
                                {
                                    Catch(ref projectile, ref crit, ref damage, ModContent.ItemType<QuickBallCaught>());
                                    return;
                                }
                            }
                        if (ballUsage == 2) // 1.8x catch chance
                        {
                            if (Main.rand.NextFloat() < .2142f)
                            {
                                Catch(ref projectile, ref crit, ref damage, ModContent.ItemType<QuickBallCaught>());
                                return;
                            }
                        }
                        if (ballUsage == 3) // 1.6x catch chance
                        {
                            if (Main.rand.NextFloat() < .1904f)
                            {
                                Catch(ref projectile, ref crit, ref damage, ModContent.ItemType<QuickBallCaught>());
                                return;
                            }
                        }
                        if (ballUsage == 4) // 1.4x catch chance
                        {
                            if (Main.rand.NextFloat() < .1666f)
                            {
                                Catch(ref projectile, ref crit, ref damage, ModContent.ItemType<QuickBallCaught>());
                                return;
                            }
                        }
                        if (ballUsage == 5) // 1.2x catch chance
                        {
                            if (Main.rand.NextFloat() < .1428f)
                            {
                                Catch(ref projectile, ref crit, ref damage, ModContent.ItemType<QuickBallCaught>());
                                return;
                            }
                        }
                        if (ballUsage == 6) // 1x catch chance
                        {
                            if (Main.rand.NextFloat() < .1190f)
                            {
                                Catch(ref projectile, ref crit, ref damage, ModContent.ItemType<QuickBallCaught>());
                                return;
                            }
                        }
                        if (ballUsage == 7) // 0.75x catch chance
                        {
                            if (Main.rand.NextFloat() < .08925f)
                            {
                                Catch(ref projectile, ref crit, ref damage, ModContent.ItemType<QuickBallCaught>());
                                return;
                            }
                        }
                        if (ballUsage >= 8) // anything more.. 0.5x
                        {
                            if (Main.rand.NextFloat() < .0595f)
                            {
                                Catch(ref projectile, ref crit, ref damage, ModContent.ItemType<QuickBallCaught>());
                                return;
                            }
                        }

                    }
                    else if (ballProjectiles[i] == "TimerBallProjectile") // Special Condition
                    {
                        ballUsage++;
                        if (ballUsage == 1) // 0.5x catch chance
                        {
                            if (Main.rand.NextFloat() < .0595f)
                            {
                                Catch(ref projectile, ref crit, ref damage, ModContent.ItemType<TimerBallCaught>());
                                return;
                            }
                        }
                        if (ballUsage == 2) // 0.75x catch chance
                        {
                            if (Main.rand.NextFloat() < .08925f)
                            {
                                Catch(ref projectile, ref crit, ref damage, ModContent.ItemType<TimerBallCaught>());
                                return;
                            }
                        }
                        if (ballUsage == 3) // 1x catch chance
                        {
                            if (Main.rand.NextFloat() < .1190f)
                            {
                                Catch(ref projectile, ref crit, ref damage, ModContent.ItemType<TimerBallCaught>());
                                return;
                            }
                        }
                        if (ballUsage == 4) // 1.2x catch chance
                        {
                            if (Main.rand.NextFloat() < .1428f)
                            {
                                Catch(ref projectile, ref crit, ref damage, ModContent.ItemType<TimerBallCaught>());
                                return;
                            }
                        }
                        if (ballUsage == 5) // 1.4x catch chance
                        {
                            if (Main.rand.NextFloat() < .1666f)
                            {
                                Catch(ref projectile, ref crit, ref damage, ModContent.ItemType<TimerBallCaught>());
                                return;
                            }
                        }
                        if (ballUsage == 6) // 1.6x catch chance
                        {
                            if (Main.rand.NextFloat() < .1904f)
                            {
                                Catch(ref projectile, ref crit, ref damage, ModContent.ItemType<TimerBallCaught>());
                                return;
                            }
                        }
                        if (ballUsage == 7) // 1.8x catch chance
                        {
                            if (Main.rand.NextFloat() < .2142f)
                            {
                                Catch(ref projectile, ref crit, ref damage, ModContent.ItemType<TimerBallCaught>());
                                return;
                            }
                        }
                        if (ballUsage == 8) // 2x catch chance
                        {
                            if (Main.rand.NextFloat() < .2380f)
                            {
                                Catch(ref projectile, ref crit, ref damage, ModContent.ItemType<TimerBallCaught>());
                                return;
                            }
                        }
                        if (ballUsage == 9) // 2.2x catch chance
                        {
                            if (Main.rand.NextFloat() < .2618f)
                            {
                                Catch(ref projectile, ref crit, ref damage, ModContent.ItemType<TimerBallCaught>());
                                return;
                            }
                        }
                        if (ballUsage == 10) // 2.4x catch chance
                        {
                            if (Main.rand.NextFloat() < .2856f)
                            {
                                Catch(ref projectile, ref crit, ref damage, ModContent.ItemType<TimerBallCaught>());
                                return;
                            }
                        }
                        if (ballUsage == 11) // 2.6x catch chance
                        {
                            if (Main.rand.NextFloat() < .3094f)
                            {
                                Catch(ref projectile, ref crit, ref damage, ModContent.ItemType<TimerBallCaught>());
                                return;
                            }
                        }
                        if (ballUsage == 12) // 2.8x catch chance
                        {
                            if (Main.rand.NextFloat() < .3332f)
                            {
                                Catch(ref projectile, ref crit, ref damage, ModContent.ItemType<TimerBallCaught>());
                                return;
                            }
                        }
                        if (ballUsage >= 13) // anything more.. 3x
                        {
                            if (Main.rand.NextFloat() < .3570f)
                            {
                                Catch(ref projectile, ref crit, ref damage, ModContent.ItemType<TimerBallCaught>());
                                return;
                            }
                        }

                    }
                    {
                        ballUsage++;
                        for (int j = 0; j < catchChances[i].Length; j++) // Retain loop for improvement later
                        {
                            if (Main.rand.NextFloat() < catchChances[i][j])
                            {
                                if (projectile.type == ModContent.ProjectileType<PokeballProjectile>()) // Special Condition
                                {
                                    Catch(ref projectile, ref crit, ref damage, ModContent.ItemType<PokeballCaught>());
                                }
                                if (projectile.type == ModContent.ProjectileType<GreatBallProjectile>()) // Special Condition
                                {
                                    Catch(ref projectile, ref crit, ref damage, ModContent.ItemType<GreatBallCaught>());
                                }
                                if (projectile.type == ModContent.ProjectileType<UltraBallProjectile>()) // Special Condition
                                {
                                    Catch(ref projectile, ref crit, ref damage, ModContent.ItemType<UltraBallCaught>());
                                }
                                if (projectile.type == ModContent.ProjectileType<PremierBallProjectile>()) // Special Condition
                                {
                                    Catch(ref projectile, ref crit, ref damage, ModContent.ItemType<PremierBallCaught>());
                                }
                                return;
                            }
                        }
                    }
                    break;
                }
            }

            CombatText.NewText(npc.Hitbox, Color.White, "Miss...", true, false);

            if (projectile.type == ModContent.ProjectileType<PokeballProjectile>()) // Special Condition
            {
                if (Main.rand.Next(3) == 0)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<PokeballItem>());
                }
            }
            if (projectile.type == ModContent.ProjectileType<GreatBallProjectile>()) // Special Condition
            {
                if (Main.rand.Next(3) == 0)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<GreatBallItem>());
                }
            }
            if (projectile.type == ModContent.ProjectileType<UltraBallProjectile>()) // Special Condition
            {
                if (Main.rand.Next(3) == 0)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<UltraBallItem>());
                }
            }
            if (projectile.type == ModContent.ProjectileType<DuskBallProjectile>()) // Special Condition
            {
                if (Main.rand.Next(3) == 0)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<DuskBallItem>());
                }
            }
            if (projectile.type == ModContent.ProjectileType<PremierBallProjectile>()) // Special Condition
            {
                if (Main.rand.Next(3) == 0)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<PremierBallItem>());
                }
            }
            if (projectile.type == ModContent.ProjectileType<QuickBallProjectile>()) // Special Condition
            {
                if (Main.rand.Next(3) == 0)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<QuickBallItem>());
                }
            }
            if (projectile.type == ModContent.ProjectileType<TimerBallProjectile>()) // Special Condition
            {
                if (Main.rand.Next(3) == 0)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<TimerBallItem>());
                }
            }

            damage = 0;
            npc.life = npc.lifeMax + 1;
            projectile.ai[1] = 0;
        }

        private void Catch(ref Projectile proj, ref bool crit, ref int dmg, int type)
        {
            var PokeNam = Regex.Replace(HomeClass().Name, nameMatcher, "$1 ");
            proj.ai[1] = 2;
            crit = false;
            dmg = npc.lifeMax;
            CreateDust(4);
            CombatText.NewText(npc.Hitbox, Color.Orange, $"{PokeNam} was caught!", true, false);

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                var packet = new BaseCatchPacket();
                packet.Send((TerramonMod)mod, HomeClass().Name, npc.TypeName, npc.getRect(), type);
            }
            else
            {
                if (Main.netMode == NetmodeID.Server)
                    BaseCaughtClass.writeDetour(HomeClass().Name, npc.TypeName, GetSmallSpritePath(npc));

                int index = Item.NewItem(npc.getRect(), type);
                if (index >= 400)
                    return;

                if (Main.netMode == NetmodeID.Server)
                    return;

                if (!(Main.item[index].modItem is BaseCaughtClass item)) return;
                item.PokemonName = npc.TypeName;
                item.CapturedPokemon = HomeClass().Name;
            }
        }

        private void CreateDust(int counter)
        {
            for (int j = 0; j < counter; j++)
            {
                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, 220, npc.velocity.X * -0.5f, npc.velocity.Y * -0.5f);
            }
        }
    }
}
