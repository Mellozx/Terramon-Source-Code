using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terramon.Items.Pokeballs.Inventory;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Charmeleon
{
    public class CharmeleonNPC : ParentPokemonNPC
    {
        public override Type HomeClass()
        {
            return typeof(Charmeleon);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 20;
            npc.height = 20;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            npc.gfxOffY = 6;
            return true;
        }

        public override void AI()
        {
            if (Main.rand.Next(9) == 0)
                Dust.NewDust(npc.position, npc.width, npc.height, 55, 0f, 0f, 100, new Color(255, 91, 59));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = Main.LocalPlayer;
            Item[] pokeballCaught = spawnInfo.player.inventory.Where(i => !i.IsAir && i.modItem is PokeballCaught)
                .ToArray();
            Item[] greatballCaught = spawnInfo.player.inventory.Where(i => !i.IsAir && i.modItem is GreatBallCaught)
                .ToArray();
            Item[] ultraballCaught = spawnInfo.player.inventory.Where(i => !i.IsAir && i.modItem is UltraBallCaught)
                .ToArray();
            Item[] duskballCaught = spawnInfo.player.inventory.Where(i => !i.IsAir && i.modItem is DuskBallCaught)
                .ToArray();
            Item[] premierballCaught = spawnInfo.player.inventory.Where(i => !i.IsAir && i.modItem is PremierBallCaught)
                .ToArray();
            if (spawnInfo.player.ZoneRockLayerHeight)
            {
                if (pokeballCaught.Any() || greatballCaught.Any() || ultraballCaught.Any() || duskballCaught.Any() ||
                    premierballCaught.Any())
                {
                    for (int i = 0; i < pokeballCaught.Length; i++)
                    {
                        PokeballCaught ball = pokeballCaught[i].modItem as PokeballCaught;
                        if (ball.PokemonName == "Charizard")
                            return 0.035f;
                    }

                    for (int i = 0; i < greatballCaught.Length; i++)
                    {
                        GreatBallCaught greatball = greatballCaught[i].modItem as GreatBallCaught;
                        if (greatball.PokemonName == "Charizard")
                            return 0.035f;
                    }

                    for (int i = 0; i < ultraballCaught.Length; i++)
                    {
                        UltraBallCaught ultraball = pokeballCaught[i].modItem as UltraBallCaught;
                        if (ultraball.PokemonName == "Charizard")
                            return 0.035f;
                    }

                    for (int i = 0; i < duskballCaught.Length; i++)
                    {
                        DuskBallCaught duskball = pokeballCaught[i].modItem as DuskBallCaught;
                        if (duskball.PokemonName == "Charizard")
                            return 0.035f;
                    }

                    for (int i = 0; i < premierballCaught.Length; i++)
                    {
                        PremierBallCaught premierball = pokeballCaught[i].modItem as PremierBallCaught;
                        if (premierball.PokemonName == "Charizard")
                            return 0.035f;
                    }
                }

                return 0f;
            }

            return 0f;
        }
    }
}