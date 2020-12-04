using Microsoft.Xna.Framework;
using Razorwing.Framework.Localisation;
using Terramon.Players;
using Terraria;
using Terraria.Utilities;

namespace Terramon.Pokemon.Moves
{
    public abstract class BaseMove
    {
        public abstract string MoveName { get; }
        public abstract string MoveDescription { get; }
        public virtual PokemonType MoveType { get; } = PokemonType.Normal;
        public abstract Target Target { get; }
        public virtual int Cooldown { get; } = 5 * 60; //5 seconds by default
        public virtual int Speed { get; } = 80;
        public virtual int MaxPP { get; } = 20;
        public virtual int MaxBoostPP { get; } = 20;

        public static UnifiedRandom _mrand;
        public static int _seed;

        public bool moveDone = false;
        public bool TurnAnimation { get; set; } = false;

        /// <summary>
        /// Used for turn based battle animations
        /// </summary>
        public int AnimationFrame { get; set; }

        /// <summary>
        /// This text will be printed when turn animation ends
        /// </summary>
        public ILocalisedBindableString PostTextLoc { get; set; }
        /// <summary>
        /// Shortcut for text printing
        /// </summary>
        public string PostText => PostTextLoc?.Value;

        /// <summary>
        /// Weight of current move when <see cref="TerramonPlayer.AutoUse"/> enabled.
        /// The more weight the more chance to be used.
        /// </summary>
        public virtual int AutoUseWeight(ParentPokemon mon, Vector2 target, TerramonPlayer player) => 10;

        //If method bellow return false -> move action not cast if Perform failed, or ended if Update
        public virtual bool PerformInWorld(ParentPokemon mon, Vector2 target, TerramonPlayer player)
        {
            return false;
        }

        /// <summary>
        /// Called when casted in turn based battle
        /// </summary>
        /// <param name="mon">Attacker projectile</param>
        /// <param name="target">Defender projectile</param>
        /// <param name="player">Player class. CAN BE NULL for Wild pokemons, so or check for null or use ? operator like this: <code>player?.ActivePet</code></param>
        /// <param name="attacker">Attacker mon data like HP, Type, etc</param>
        /// <param name="deffender">Defender mon data</param>
        /// <returns>return true if move was performed</returns>
        public virtual bool PerformInBattle(ParentPokemon mon, ParentPokemon target,
            TerramonPlayer player, PokemonData attacker, PokemonData deffender)
        {
            return false;
        }

        /// <summary>
        /// Overrides pokemon projectile AI. Can be called both from battle and from world.
        /// Use <see cref="TurnAnimation"/> to see if this called from turn battle
        /// </summary>
        /// <param name="mon">Projectile</param>
        /// <param name="player">Caster class. Always check is player active (<code>player.player.active</code>)
        /// bc for wild mons it will be invalid</param>
        /// <returns>Returning true disables default projectile AI</returns>
        public virtual bool OverrideAI(ParentPokemon mon, TerramonPlayer player)
        {
            return false;
        }

        /// <summary>
        /// Called when turn based animation is requested. Called 120 times synced with updates calls.
        /// So rn in perfect condition turn animation takes const 3 seconds.
        /// </summary>
        /// <param name="mon">Attacker projectile</param>
        /// <param name="target">Defender projectile</param>
        /// <param name="player">Player class. CAN BE NULL for Wild pokemons, so or check for null or use ? operator like this: <code>player?.ActivePet</code></param>
        /// <param name="attacker">Attacker mon data like HP, Type, etc</param>
        /// <param name="deffender">Defender mon data</param>
        /// <returns>Return true if animation continues</returns>
        public virtual bool AnimateTurn(ParentPokemon mon, ParentPokemon target,
            TerramonPlayer player, PokemonData attacker, PokemonData deffender, BattleState state, bool opponent)
        {
            return false;
        }

        /// <summary>
        /// First called when move was casted. If returned true this continue calls each update
        /// until false was returned.
        /// </summary>
        /// <param name="mon"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public virtual bool Update(ParentPokemon mon, TerramonPlayer player)
        {
            return false;
        }

        /// <summary>
        /// Return nearest enemy NPC around point (Length of circle is 400 pixels)
        /// </summary>
        public static NPC GetNearestNPC(int x, int y)
        {
            return GetNearestNPC(new Vector2(x, y));
        }

        /// <summary>
        /// Return nearest enemy NPC around point (Length of circle is 400 pixels)
        /// </summary>
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

        /// <summary>
        /// Return nearest wild pokemon NPC around point
        /// </summary>
        public static NPC GetNearestPokemon(Vector2 pos)
        {
            int closest = -1;
            float lenght = float.MaxValue, buf;
            for (int i = 0; i < Main.maxNPCs; i++)
                if (Main.npc[i].active && Main.npc[i].modNPC is ParentPokemonNPC)
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

        /// <summary>
        /// Return nearest player around point, excluding caller
        /// </summary>
        public static Player GetNearestPlayer(Vector2 pos, Player caller)
        {
            int closest = -1;
            float lenght = float.MaxValue, buf;
            for (int i = 0; i < Main.maxNPCs; i++)
                if (Main.player[i].active && Main.player[i] != caller)
                {
                    buf = (pos - Main.player[i].position).LengthSquared();
                    if (buf < lenght)
                    {
                        closest = i;
                        lenght = buf;
                    }
                }

            if (closest == -1 || (pos - Main.player[closest].position).LengthSquared() > 60000)//400^2
                return null;

            return Main.player[closest];
        }

        /// <summary>
        /// Call this from AnimateTurn after the animation is finished
        /// </summary>
        public void EndMove()
        {
           // obsolete
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