using System;
using Terramon.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terramon.Pokemon
{
    public abstract class ParentPokemon : ModProjectile
    {
        /// <summary>
        ///     Next stage pokemon to evolve.
        ///     If value == null => mon can't evolve
        /// </summary>
        public virtual Type EvolveTo { get; } = null;

        /// <summary>
        ///     How much candies need to evolve.
        /// </summary>
        public virtual int EvolveCost { get; } = 0;

        /// <summary>
        ///     Item what used for evolution
        /// </summary>
        public virtual EvolveItem EvolveItem { get; } = EvolveItem.RareCandy;

        /// <summary>
        ///     Just for checking if this mon can evolve or not
        /// </summary>
        public bool CanEvolve => EvolveTo != null && EvolveCost != 0;

        public virtual PokemonType[] PokemonTypes => new[] {PokemonType.Normal};

        public virtual string[] DefaultMove => new[] {"", "", "", ""};

        private string iconName;
        public virtual string IconName => iconName ?? (iconName = $"Terramon/Minisprites/Regular/mini{GetType().Name}");


        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 11;
            Main.projPet[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Puppy);
            aiType = ProjectileID.Puppy;
        }

        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            player.zephyrfish = false; // Relic from aiType
            if (aiType != 0)
                mainAi = aiType;
            return true;
        }

        private int mainAi = ProjectileID.Puppy;

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            TerramonPlayer modPlayer = player.GetModPlayer<TerramonPlayer>();
            if (player.dead)
            {
                modPlayer.ResetEffects();
                modPlayer.ActivePetId = -1;
            }

            if (modPlayer.IsPetActive(GetType().Name)) projectile.timeLeft = 2;

            if (modPlayer.ActiveMove != null)
            {
                if (modPlayer.ActiveMove.OverrideAI(projectile, this, modPlayer))
                    aiType = 0;
            }
            else if (aiType == 0)
            {
                aiType = mainAi;
            }
        }
    }

    public enum PokemonType
    {
        Bug,
        Dark,
        Dragon,
        Electric,
        Fight,
        Fire,
        Flying,
        Ghost,
        Grass,
        Ground,
        Ice,
        Normal,
        Poison,
        Psyhcic,
        Rock,
        Steel,
        Water,
        Nuclear,
        Light,
        Machine,
        Sound
    }

    public enum EvolveItem
    {
        RareCandy,
        LinkCable
    }
}