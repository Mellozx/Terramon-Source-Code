namespace Terramon.Pokemon.FirstGeneration.Normal.Eevee
{
    public class Eevee : ParentPokemon
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 34; //-6
            projectile.height = 24; //-4
            drawOriginOffsetY = -20;
        }
    }
}