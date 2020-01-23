﻿using Microsoft.Xna.Framework;
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
    public abstract class NotCatchablePKMNBirdFlying : NotCatchablePKMN
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(PokeName());
            Main.npcFrameCount[npc.type] = 2;
        }

        private const int Flying1 = 0;
        private const int Flying2 = 1;
        public int AITimer = 0;

        public override void AI()
        {
            AITimer++;
            if (AITimer > 60)
                AITimer = 0;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            if (AITimer > 30)
                npc.frame.Y = Flying1 * frameHeight;
            else
                npc.frame.Y = Flying2 * frameHeight;
        }
    }
}
