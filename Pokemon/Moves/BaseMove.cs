using Microsoft.Xna.Framework;
using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.Moves
{
    public abstract class BaseMove
    {
        public abstract string MoveName { get; }
        public virtual PokemonType MoveType { get; } = PokemonType.Normal;
        public abstract Target Target { get; }
        public virtual int Cooldown { get; } = 5 * 60; //5 seconds by default

        /// <summary>
        /// Weight of current move when <see cref="TerramonPlayer.AutoUse"/> enabled.
        /// The more weight the more chance to be used.
        /// </summary>
        public virtual int AutoUseWeight(Projectile proj, ParentPokemon mon, Vector2 target, TerramonPlayer player) => 10;

        //If method bellow return false -> move action not cast if Perform failed, or ended if Update
        public virtual bool PerformInWorld(Projectile proj, ParentPokemon mon, Vector2 target, TerramonPlayer player)
        {
            return false;
        }

        public virtual bool PerformInBattle(Projectile proj, ParentPokemon mon, ParentPokemon target,
            TerramonPlayer player)
        {
            return false;
        }

        public virtual bool OverrideAI(Projectile proj, ParentPokemon mon, TerramonPlayer player)
        {
            return false;
        }

        public virtual bool Update(Projectile proj, ParentPokemon mon, TerramonPlayer player)
        {
            return false;
        }

        public static NPC GetNearestNPC(int x, int y)
        {
            return GetNearestNPC(new Vector2(x, y));
        }

        public static NPC GetNearestNPC(Vector2 pos)
        {
            int closest = -1;
            float lenght = float.MaxValue, buf;
            for (int i = 0; i < Main.maxNPCs; i++)
                if (Main.npc[i].active && !Main.npc[i].friendly && !(Main.npc[i].modNPC is ParentPokemonNPC))
                {
                    buf = (pos - Main.npc[i].position).LengthSquared();
                    if (buf < lenght)
                    {
                        closest = i;
                        lenght = buf;
                    }
                }

            if (closest == -1 || (pos - Main.npc[closest].position).LengthSquared() > 60000)//400^2
                return null;

            return Main.npc[closest];
        }
    }

    public enum Target
    {
        Self,
        Opponent,
        Party,
        OpponentParty,
        Trainer
    }
}