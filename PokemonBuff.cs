using Razorwing.Framework.Localisation;
using Terramon.Network.Sync;
using Terramon.Players;
using Terraria;
using Terraria.ModLoader;

namespace Terramon
{
    public class PokemonBuff : ModBuff
    {
        protected ILocalisedBindableString PokeName = TerramonMod.Localisation.GetLocalisedString(new LocalisedString("*"));
        protected ILocalisedBindableString Following = TerramonMod.Localisation.GetLocalisedString(new LocalisedString(("buff.following", "{0} is following you around!")));
        public virtual string ProjectileName { get; set; }

        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
            DisplayName.SetDefault(PokeName.Value);
            Description.SetDefault(Following.Value);
        }

        public override void Update(Player player, ref int buffIndex)
        {
            TerramonPlayer modPlayer = player.GetModPlayer<TerramonPlayer>();
            if (string.IsNullOrEmpty(modPlayer.ActivePetName))
            {
                player.DelBuff(buffIndex);
                return;
            }

            if (ProjectileName != modPlayer.ActivePetName)
            {
                ProjectileName = modPlayer.ActivePetName;
                PokeName = TerramonMod.Localisation.GetLocalisedString(new LocalisedString(ProjectileName));
                Following.Args = new object[] {PokeName.Value};
            }



            player.buffTime[buffIndex] = 40000;

            modPlayer.ActivatePet(ProjectileName);

            var petProjectileNotSpawned = !(player.ownedProjectileCounts[mod.ProjectileType(ProjectileName)] > 0);

            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                if (player.direction == 1) // direction right
                {
                    modPlayer.ActivePetId = Projectile.NewProjectile(player.position.X + 50,
                    player.position.Y - 8, 0f, 0f, mod.ProjectileType(ProjectileName), 0, 0f,
                    player.whoAmI, 0f, 0f);
                }
                else // direction left
                {
                    modPlayer.ActivePetId = Projectile.NewProjectile(player.position.X - 50,
                    player.position.Y - 8, 0f, 0f, mod.ProjectileType(ProjectileName), 0, 0f,
                    player.whoAmI, 0f, 0f);
                }
                new PetIDSyncPacket().Send((TerramonMod)mod, modPlayer.ActivePetId);
            }
        }


        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = Following.Value;
            rare = 0;
        }
    }
}