using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terramon.Achievements;
using Terraria;

namespace Terramon.Pokemon.Moves
{
    public abstract class BaseMove
    {
        public abstract string MoveName { get; }
        public virtual PokemonType MoveType { get; } = PokemonType.Normal;
		public abstract Target Target { get; }
        public virtual int Cooldown { get; } = 5*60;//5 seconds by default

        public virtual bool PerformInWorld(ParentPokemon mon, NPC target) => false;
        public virtual bool PerformInBattle(ParentPokemon mon, ParentPokemon target) => false;
        public virtual bool OverrideAI(ParentPokemon mon) => false;
    }

    public enum Target
    {
        Self,
        Opponent,
        Party,
        OpponentParty,
        Trainer,
    }
}
