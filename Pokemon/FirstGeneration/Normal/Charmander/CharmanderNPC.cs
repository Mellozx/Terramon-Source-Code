using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Charmander
{
    public class CharmanderNPC : ParentPokemonNPC_Charmander
    {
        public override Type HomeClass() => typeof(Charmander);

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
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 55, 0f, 0f, 100, new Color(255, 148, 41), 1f);
            }
        }
    }
}