using System;

namespace Terramon.Pokemon.FirstGeneration.Normal.Paras
{
    public class Paras : ParentPokemon
    {
         

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 32;
            projectile.height = 32;
        }
    }
}