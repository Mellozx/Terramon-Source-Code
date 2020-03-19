namespace Terramon.Pokemon.FirstGeneration.Normal.Raichu

{
    public class Raichu : ParentPokemon
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 35;
            projectile.height = 35;
            drawOriginOffsetY = -11;
        }
    }
}