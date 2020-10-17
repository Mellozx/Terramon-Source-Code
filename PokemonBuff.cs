using Terramon.Players;
using Terraria;
using Terraria.ModLoader;

namespace Terramon
{
    public class PokemonBuff : ModBuff
    {
        public virtual string ProjectileName { get; set; }

        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
            DisplayName.SetDefault($"{ProjectileName}");
            Description.SetDefault($"{ProjectileName} is following you around!");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            TerramonPlayer modPlayer = player.GetModPlayer<TerramonPlayer>();
            if (string.IsNullOrEmpty(modPlayer.ActivePetName))
            {
                player.DelBuff(buffIndex);
                return;
            }

            ProjectileName = modPlayer.ActivePetName;

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
            }
        }


        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = $"{ProjectileName} is following you around!";
            rare = 0;
        }
    }
}