using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Terramon.Pokemon.FirstGeneration.Normal.Bulbasaur
{
    public class BulbasaurNPC : ParentPokemonNPC_Bulbasaur
    {
        public override Type HomeClass() => typeof(Bulbasaur);

        public override void SetDefaults()
        {
            base.SetDefaults();
			npc.width = 20;
			npc.height = 20;
			npc.scale = 1f;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            npc.gfxOffY = 6;
            return true;
        }
    }
}