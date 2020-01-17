using Microsoft.Xna.Framework;
using System;
using System.Text.RegularExpressions;
using Terramon.Items.Pokeballs.Inventory;
using Terramon.Items.Pokeballs.Thrown;
using Terramon.Pokemon.FirstGeneration.Normal._caughtForms;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terramon.Pokemon
{
    public abstract class ParentPokemonNPC_Eevee : ModNPC //4.50% spawn in forest, also used for Pikachu
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
            if (spawnInfo.player.ZoneOverworldHeight)
            {
                return 0.045f;
            }
            else
            {
                return 0f;
            }
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
                            CatchPokemonDuskBall(ref projectile, ref crit, ref damage);
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
                                    CatchPokemonPokeball(ref projectile, ref crit, ref damage);
                                }
                                if (projectile.type == ModContent.ProjectileType<GreatBallProjectile>()) // Special Condition
                                {
                                    CatchPokemonGreatBall(ref projectile, ref crit, ref damage);
                                }
                                if (projectile.type == ModContent.ProjectileType<UltraBallProjectile>()) // Special Condition
                                {
                                    CatchPokemonUltraBall(ref projectile, ref crit, ref damage);
                                }
                                if (projectile.type == ModContent.ProjectileType<PremierBallProjectile>()) // Special Condition
                                {
                                    CatchPokemonPremierBall(ref projectile, ref crit, ref damage);
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

        private void CatchPokemonPokeball(ref Projectile proj, ref bool crit, ref int dmg)
        {	
			var PokeNam = Regex.Replace(HomeClass().Name, nameMatcher, "$1 ");
            proj.ai[1] = 2;
            crit = false;
            dmg = npc.lifeMax;
            CreateDust(4);
			CombatText.NewText(npc.Hitbox, Color.Orange, $"{PokeNam} was caught!", true, false);
                int index = Item.NewItem(npc.getRect(), ModContent.ItemType<PokeballCaught>());
                if (index >= 400)
                    return;
                (Main.item[index].modItem as PokeballCaught).PokemonNPC = npc.type;
                (Main.item[index].modItem as PokeballCaught).PokemonName = npc.TypeName;
                (Main.item[index].modItem as PokeballCaught).SmallSpritePath = GetSmallSpritePath(npc);
            
        }
        private void CatchPokemonGreatBall(ref Projectile proj, ref bool crit, ref int dmg)
        {
            var PokeNam = Regex.Replace(HomeClass().Name, nameMatcher, "$1 ");
            proj.ai[1] = 2;
            crit = false;
            dmg = npc.lifeMax;
            CreateDust(4);
            CombatText.NewText(npc.Hitbox, Color.Orange, $"{PokeNam} was caught!", true, false);
            int index = Item.NewItem(npc.getRect(), ModContent.ItemType<GreatBallCaught>());
            if (index >= 400)
                return;
            (Main.item[index].modItem as GreatBallCaught).PokemonNPC = npc.type;
            (Main.item[index].modItem as GreatBallCaught).PokemonName = npc.TypeName;
            (Main.item[index].modItem as GreatBallCaught).SmallSpritePath = GetSmallSpritePath(npc);

        }

        private void CatchPokemonUltraBall(ref Projectile proj, ref bool crit, ref int dmg)
        {
            var PokeNam = Regex.Replace(HomeClass().Name, nameMatcher, "$1 ");
            proj.ai[1] = 2;
            crit = false;
            dmg = npc.lifeMax;
            CreateDust(4);
            CombatText.NewText(npc.Hitbox, Color.Orange, $"{PokeNam} was caught!", true, false);
            int index = Item.NewItem(npc.getRect(), ModContent.ItemType<UltraBallCaught>());
            if (index >= 400)
                return;
            (Main.item[index].modItem as UltraBallCaught).PokemonNPC = npc.type;
            (Main.item[index].modItem as UltraBallCaught).PokemonName = npc.TypeName;
            (Main.item[index].modItem as UltraBallCaught).SmallSpritePath = GetSmallSpritePath(npc);

        }
        private void CatchPokemonDuskBall(ref Projectile proj, ref bool crit, ref int dmg)
        {
            var PokeNam = Regex.Replace(HomeClass().Name, nameMatcher, "$1 ");
            proj.ai[1] = 2;
            crit = false;
            dmg = npc.lifeMax;
            CreateDust(4);
            CombatText.NewText(npc.Hitbox, Color.Orange, $"{PokeNam} was caught!", true, false);
            int index = Item.NewItem(npc.getRect(), ModContent.ItemType<DuskBallCaught>());
            if (index >= 400)
                return;
            (Main.item[index].modItem as DuskBallCaught).PokemonNPC = npc.type;
            (Main.item[index].modItem as DuskBallCaught).PokemonName = npc.TypeName;
            (Main.item[index].modItem as DuskBallCaught).SmallSpritePath = GetSmallSpritePath(npc);

        }
        private void CatchPokemonPremierBall(ref Projectile proj, ref bool crit, ref int dmg)
        {
            var PokeNam = Regex.Replace(HomeClass().Name, nameMatcher, "$1 ");
            proj.ai[1] = 2;
            crit = false;
            dmg = npc.lifeMax;
            CreateDust(4);
            CombatText.NewText(npc.Hitbox, Color.Orange, $"{PokeNam} was caught!", true, false);
            int index = Item.NewItem(npc.getRect(), ModContent.ItemType<PremierBallCaught>());
            if (index >= 400)
                return;
            (Main.item[index].modItem as PremierBallCaught).PokemonNPC = npc.type;
            (Main.item[index].modItem as PremierBallCaught).PokemonName = npc.TypeName;
            (Main.item[index].modItem as PremierBallCaught).SmallSpritePath = GetSmallSpritePath(npc);

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
