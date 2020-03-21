using System;
using System.Linq;
using Terramon.Items.Pokeballs.Inventory;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Ivysaur
{
    public class IvysaurNPC : ParentPokemonNPC
    {
        public override Type HomeClass()
        {
            return typeof(Ivysaur);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 20;
            npc.height = 20;
            npc.scale = 1.2f;
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
            if (spawnInfo.player.ZoneOverworldHeight && spawnInfo.player.ZoneJungle)
            {
                if (pokeballCaught.Any() || greatballCaught.Any() || ultraballCaught.Any() || duskballCaught.Any() ||
                    premierballCaught.Any())
                {
                    for (int i = 0; i < pokeballCaught.Length; i++)
                    {
                        PokeballCaught ball = pokeballCaught[i].modItem as PokeballCaught;
                        if (ball.PokemonName == "Venusaur")
                            return 0.035f;
                    }

                    for (int i = 0; i < greatballCaught.Length; i++)
                    {
                        GreatBallCaught greatball = greatballCaught[i].modItem as GreatBallCaught;
                        if (greatball.PokemonName == "Venusaur")
                            return 0.035f;
                    }

                    for (int i = 0; i < ultraballCaught.Length; i++)
                    {
                        UltraBallCaught ultraball = ultraballCaught[i].modItem as UltraBallCaught;
                        if (ultraball.PokemonName == "Venusaur")
                            return 0.035f;
                    }

                    for (int i = 0; i < duskballCaught.Length; i++)
                    {
                        DuskBallCaught duskball = duskballCaught[i].modItem as DuskBallCaught;
                        if (duskball.PokemonName == "Venusaur")
                            return 0.035f;
                    }

                    for (int i = 0; i < premierballCaught.Length; i++)
                    {
                        PremierBallCaught premierball = premierballCaught[i].modItem as PremierBallCaught;
                        if (premierball.PokemonName == "Venusaur")
                            return 0.035f;
                    }
                }

                return 0f;
            }

            return 0f;
        }
    }
}