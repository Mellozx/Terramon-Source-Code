using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Terramon.Pokemon.FirstGeneration.Normal.Caterpie
{
    public class CaterpieNPC : ParentPokemonNPC_Caterpie
    {
        public override Type HomeClass() => typeof(Caterpie);

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
    }
}