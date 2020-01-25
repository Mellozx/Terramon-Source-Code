using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terramon.Pokemon.FirstGeneration.Normal._caughtForms;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Wartortle
{
    public class WartortleNPC : ParentPokemonNPC
    {
        public override Type HomeClass() => typeof(Wartortle);

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

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = Main.LocalPlayer;
            Item[] pokeballCaught = spawnInfo.player.inventory.Where(i => !i.IsAir && i.modItem is PokeballCaught).ToArray();
            Item[] greatballCaught = spawnInfo.player.inventory.Where(i => !i.IsAir && i.modItem is GreatBallCaught).ToArray();
            Item[] ultraballCaught = spawnInfo.player.inventory.Where(i => !i.IsAir && i.modItem is UltraBallCaught).ToArray();
            Item[] duskballCaught = spawnInfo.player.inventory.Where(i => !i.IsAir && i.modItem is DuskBallCaught).ToArray();
            Item[] premierballCaught = spawnInfo.player.inventory.Where(i => !i.IsAir && i.modItem is PremierBallCaught).ToArray();
            if (spawnInfo.player.ZoneRockLayerHeight && spawnInfo.player.ZoneSnow)
            {
                if (pokeballCaught.Any() || greatballCaught.Any() || ultraballCaught.Any() || duskballCaught.Any() || premierballCaught.Any())
                {
                    for (int i = 0; i < pokeballCaught.Length; i++)
                    {
                        PokeballCaught ball = (pokeballCaught[i].modItem as PokeballCaught);
                        if (ball.PokemonName == "Blastoise")
                            return 0.035f;
                    }
                    for (int i = 0; i < greatballCaught.Length; i++)
                    {
                        GreatBallCaught greatball = (greatballCaught[i].modItem as GreatBallCaught);
                        if (greatball.PokemonName == "Blastoise")
                            return 0.035f;
                    }
                    for (int i = 0; i < ultraballCaught.Length; i++)
                    {
                        UltraBallCaught ultraball = (pokeballCaught[i].modItem as UltraBallCaught);
                        if (ultraball.PokemonName == "Blastoise")
                            return 0.035f;
                    }
                    for (int i = 0; i < duskballCaught.Length; i++)
                    {
                        DuskBallCaught duskball = (pokeballCaught[i].modItem as DuskBallCaught);
                        if (duskball.PokemonName == "Blastoise")
                            return 0.035f;
                    }
                    for (int i = 0; i < premierballCaught.Length; i++)
                    {
                        PremierBallCaught premierball = (pokeballCaught[i].modItem as PremierBallCaught);
                        if (premierball.PokemonName == "Blastoise")
                            return 0.035f;
                    }
                }
                return 0f;
            }
            else
            {
                return 0f;
            }
        }
    }
}