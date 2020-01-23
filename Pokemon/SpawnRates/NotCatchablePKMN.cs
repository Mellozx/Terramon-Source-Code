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
    public abstract class NotCatchablePKMN : ParentPokemonNPC
    {
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0f;
        }
    }
}
