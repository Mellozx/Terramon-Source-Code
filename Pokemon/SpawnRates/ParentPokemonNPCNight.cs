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
    public abstract class ParentPokemonNPCNight : ModNPC
    {
        private readonly string[] ballProjectiles = TerramonMod.GetBallProjectiles();
        private readonly float[][] catchChances = TerramonMod.GetCatchChances();
        private readonly string nameMatcher = "([a-z](?=[A-Z]|[0-9])|[A-Z](?=[A-Z][a-z]|[0-9])|[0-9](?=[^0-9]))";

        public abstract Type HomeClass();

        public string PokeName() => Regex.Replace(HomeClass().Name, nameMatcher, "$1 ");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(PokeName());
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.Bunny];
        }

        public override void SetDefaults()
        {
            npc.defense = 0;
            npc.lifeMax = 1;
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

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit) { }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!Main.dayTime)
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
            return 0f;
        }

        // this method will be improved later
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            knockback = 0;
            crit = false;

            for (int i = 0; i < ballProjectiles.Length; i++)
            {
                if (projectile.type == mod.ProjectileType(ballProjectiles[i]) && projectile.ai[1] == 1)
                {
                    if (ballProjectiles[i] == "DuskBallProjectile") // Special Condition
                    {
                        if ((!Main.dayTime && Main.rand.NextFloat() < catchChances[i][0]) ||
                            (Main.dayTime && Main.rand.NextFloat() < catchChances[i][1]))
                        {
                            Catch(ref projectile, ref crit, ref damage, ModContent.ItemType<DuskBallCaught>());
                            return;
                        }
                    }
                    else
                    {
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
                packet.Send((TerramonMod)mod, npc.type, npc.TypeName, GetSmallSpritePath(npc), npc.getRect(), type);
            }
            else
            {
                if (Main.netMode == NetmodeID.Server)
                    BaseCaughtClass.writeDetour(npc.type, npc.TypeName, GetSmallSpritePath(npc));

                int index = Item.NewItem(npc.getRect(), type);
                if (index >= 400)
                    return;

                if (Main.netMode == NetmodeID.Server)
                    return;

                (Main.item[index].modItem as BaseCaughtClass).PokemonNPC = npc.type;
                (Main.item[index].modItem as BaseCaughtClass).PokemonName = npc.TypeName;
                (Main.item[index].modItem as BaseCaughtClass).SmallSpritePath = GetSmallSpritePath(npc);
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
