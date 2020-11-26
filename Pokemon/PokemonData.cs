using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Razorwing.Framework.Extensions;
using Razorwing.Framework.Localisation;
using Steamworks;
using Terramon.Items.Pokeballs.Inventory;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ModLoader.IO;

namespace Terramon.Pokemon
{
    //TODO: Make BaseCaughtClass use this class instead fields
    //TODO: bc we migrate to this class to work with data

    /// <summary>
    /// Class what simplify access to pokemon <see cref="PokeballCaught"/>'s <see cref="TagCompound"/> data
    /// </summary>
    public class PokemonData : ITagLoadable
    {
        
        internal bool needUpdate = false;
        internal byte pokeballType;
        private int level;
        private int exp;
        private int maxHp;
        private int hp;
        public string pokemon;

        private ILocalisedBindableString localised;
        public string PokemonName => localised?.Value ?? Pokemon;

        public string Pokemon
        {
            get => pokemon;
            set
            {
                if(pokemon == value)
                    return;

                pokemon = value;
                localised = TerramonMod.Localisation.GetLocalisedString(new LocalisedString(pokemon));
                Types = TerramonMod.GetPokemon(Pokemon).PokemonTypes;
            }
        }

        public bool IsShiny { get; set; }
        public BaseMove[] Moves { get; set; }
        public PokemonType[] Types { get; private set; }// Auto filled
        public bool Fainted { get; set; }//TODO: Add saving HP, MaxHP and Fainted to pokeball TagCompound.

        /// <summary>
        /// Used to store some local data what don't be saved
        /// </summary>
        public Dictionary<string, string> CustomData { get; set; } = new Dictionary<string, string>(); 

        public int Level
        {
            get => level;
            set
            {
                if(value == level)
                    return;
                var d = value - level;
                if (d > 0 && !string.IsNullOrEmpty(pokemon))
                {
                    var parent = TerramonMod.GetPokemon(pokemon);
                    //Get sum of base stats of pokemon
                    var sum = parent.MaxHP + parent.PhysicalDamage + parent.PhysicalDefence +
                              parent.Speed + parent.SpecialDamage + parent.SpecialDefence;
                    // Divide it by 50
                    // https://bulbapedia.bulbagarden.net/wiki/Statistic#Level
                    sum /= 50;

                    do
                    {
                        d--;

                        // And randomly add stats by one point
                        while (sum > 0)
                        {
                            sum--;
                            Random rand = new Random();//We don't want desync Main.random by this;
                            switch (rand.Next(6))
                            {
                                case 0:
                                    MaxHP++;
                                    break;
                                case 1:
                                    Speed++;
                                    break;
                                case 2:
                                    PhysDmg++;
                                    break;
                                case 3:
                                    PhysDef++;
                                    break;
                                case 4:
                                    SpDmg++;
                                    break;
                                case 5:
                                    SpDef++;
                                    break;
                            }
                        }
                    } while (d > 0);
                }
                level = value;
                needUpdate = true;
            }
        }

        public int Exp
        {
            get => exp;
            set
            {
                if(exp == value)
                    return;
                exp = value;
                if (exp > 100)
                {
                    exp -= 100;
                    Level += 1;
                }
                needUpdate = true;
            }
        }

        public int MaxHP
        {
            get => maxHp;
            set
            {
                if(maxHp == value)
                    return;
                
                maxHp = value;
                needUpdate = true;
            }
        }

        public int HP
        {
            get => hp;
            set
            {
                if (hp == value)
                    return;

                hp = value > MaxHP ? MaxHP : value;
                if (hp <= 0)
                    Fainted = true;
                needUpdate = true;

            }
        }

        /// <summary>
        /// Physical damage 
        /// </summary>
        public int PhysDmg { get; set; } = 50 + Main.rand?.Next(20) ?? 0;
        /// <summary>
        /// Physical defense
        /// </summary>
        public int PhysDef { get; set; } = 60 + Main.rand?.Next(20) ?? 0;
        /// <summary>
        /// Special damage
        /// </summary>
        public int SpDmg { get; set; } = 65 + Main.rand?.Next(20) ?? 0;
        /// <summary>
        /// Special defense
        /// </summary>
        public int SpDef { get; set; } = 50+Main.rand?.Next(20)??0;
        public int Speed { get; set; } = 45 + Main.rand?.Next(20) ?? 0;

        /// <summary>
        /// Increase mon HP by <see cref="amout"/> and return actually healed value;
        /// </summary>
        /// <param name="delta">How many HP need to refill</param>
        /// <returns>Applied healing (same as <see cref="delta"/> if <see cref="MaxHP"/> not reached)</returns>
        public int Heal(int delta)
        {
            if (hp + delta > maxHp)
            {
                var d = MaxHP - HP;
                HP = MaxHP;
                return d;
            }

            HP += delta;
            return delta;
        }

        /// <summary>
        /// Decrease mon HP by <see cref="amout"/> and return actually damage value;
        /// </summary>
        /// <param name="delta">How many HP need to decrease</param>
        /// <returns>Applied damage</returns>
        [Obsolete]
        public int Damage(int delta, PokemonType damageType)
        {
            foreach (PokemonType it in Types)
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator // Disable this warning bc we have precise values
                if(it.GetResist(damageType) == 1f)
                    continue;
                delta = (int)(delta * it.GetResist(damageType));
            }
            if (hp - delta < 0)
            {
                var d = HP;
                HP = 0;//This makes mon automatically fainted
                return d;
            }

            HP -= delta;
            return delta;
        }

        public int Damage(int delta)
        {
            if (hp - delta < 0)
            {
                var d = HP;
                HP = 0;//This makes mon automatically fainted
                return d;
            }

            HP -= delta;
            return delta;
        }


        public PokemonData()
        {
            Moves = new BaseMove[] {null, null, null,null};
            Types = new [] {PokemonType.Normal};
            MaxHP = 45 + Main.rand?.Next(20) ?? 0;
            HP = 45 + Main.rand?.Next(20) ?? 0;
            Level = 1 + Main.rand?.Next(8) ?? 0;
            Fainted = false;
        }

        public PokemonData(TagCompound tag)
        {
            Load(tag);
        }

        public PokemonData(BaseCaughtClass tag)
        {
            IsShiny = tag.isShiny;
            //v2
            pokemon = tag.CapturedPokemon;
            Types = TerramonMod.GetPokemon(Pokemon).PokemonTypes;

            level = tag.Level;//Assign to field here to avoid leveling up
            exp = tag.Exp;

            if (Moves == null)
                Moves = new BaseMove[4];
            Moves[0] = tag.Moves[0];
            Moves[1] = tag.Moves[1];
            Moves[2] = tag.Moves[2];
            Moves[3] = tag.Moves[3];


            Fainted = tag.PokeData.Fainted;
            MaxHP = tag.PokeData.MaxHP;
            HP = tag.PokeData.HP;
            PhysDmg = tag.PokeData.PhysDmg;
            PhysDef = tag.PokeData.PhysDef;
            SpDmg = tag.PokeData.SpDmg;
            SpDef = tag.PokeData.SpDef;


            //Update all old pokebals
            bool retrofit = true;
            foreach (var it in Moves)
                if (it != null)
                    retrofit = false;
            if (retrofit)
            {
                var def = TerramonMod.GetPokemon(Pokemon).DefaultMove;
                try //In case someone forgot leave nulls at empty moves 
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Moves[i] = TerramonMod.GetMove(def[i]);
                    }
                }
                catch (Exception e)
                {
                    TerramonMod.Instance.Logger.Error($"Looks like someone from dev team don't fill moves field properly for {Pokemon} pokemon.\n" +
                                                      $"Please, report this to Terramon Team!");
                    TerramonMod.Instance.Logger.Error(e);
                }
            }

            pokeballType = (byte) TerramonMod.PokeballFactory.GetEnum(tag);
        }


        public static implicit operator TagCompound(PokemonData tag)
        {
            return new TagCompound()
            {
                [nameof(BaseCaughtClass.PokemonName)] = tag.Pokemon,
                //[nameof(SmallSpritePath)] = SmallSpritePath,
                //
                //[nameof(SmallSpritePath)] = SmallSpritePath, // what do i do here
                //v2

                [nameof(BaseCaughtClass.isShiny)] = tag.IsShiny,
                [nameof(BaseCaughtClass.CapturedPokemon)] = tag.Pokemon,
                [nameof(BaseCaughtClass.Level)] = tag.Level,
                [nameof(BaseCaughtClass.Exp)] = tag.Exp,

                [nameof(MaxHP)] = tag.MaxHP,
                [nameof(HP)] = tag.HP,
                [nameof(PhysDmg)] = tag.PhysDmg,
                [nameof(PhysDef)] = tag.PhysDef,
                [nameof(SpDmg)] = tag.SpDmg,
                [nameof(SpDef)] = tag.SpDef,

                //Store move name
                [BaseCaughtClass.MOVE1] = tag.Moves?[0]?.GetType().Name ?? "",
                [BaseCaughtClass.MOVE2] = tag.Moves?[1]?.GetType().Name ?? "",
                [BaseCaughtClass.MOVE3] = tag.Moves?[2]?.GetType().Name ?? "",
                [BaseCaughtClass.MOVE4] = tag.Moves?[3]?.GetType().Name ?? "",
                //[nameof(Moves)] = from it in Moves select it.MoveName,
                //Used to restore items in sidebarUI
                [BaseCaughtClass.POKEBAL_PROPERTY] = tag.pokeballType
            };
        }

        public static explicit operator PokemonData(TagCompound tag)
        {
            return new PokemonData(tag);
        }

        public void Load(TagCompound tag)
        {
            IsShiny = tag.GetBool(nameof(BaseCaughtClass.isShiny));
            //v2
            pokemon = tag.ContainsKey(nameof(BaseCaughtClass.CapturedPokemon))
                ? tag.GetString(nameof(BaseCaughtClass.CapturedPokemon))
                : tag.GetString(nameof(BaseCaughtClass.PokemonName));

            if (!string.IsNullOrEmpty(pokemon))
            {
                Types = TerramonMod.GetPokemon(Pokemon).PokemonTypes;

                level = tag.ContainsKey(nameof(Level)) ? tag.GetInt(nameof(Level)) : 1;
                exp = tag.ContainsKey(nameof(Exp)) ? tag.GetInt(nameof(Exp)) : 0;

                //Average values from bulbasaur
                MaxHP = tag.ContainsKey(nameof(MaxHP)) ? tag.GetInt(nameof(MaxHP)) : 45;
                HP = tag.ContainsKey(nameof(HP)) ? tag.GetInt(nameof(HP)) : 45;
                Fainted = tag.ContainsKey(nameof(Fainted)) && tag.GetBool(nameof(Fainted));//ReSharper changes
                PhysDef = tag.ContainsKey(nameof(PhysDef)) ? tag.GetInt(nameof(PhysDef)) : 50;
                PhysDmg = tag.ContainsKey(nameof(PhysDmg)) ? tag.GetInt(nameof(PhysDmg)) : 50;
                SpDef = tag.ContainsKey(nameof(SpDef)) ? tag.GetInt(nameof(SpDef)) : 50;
                SpDmg = tag.ContainsKey(nameof(SpDmg)) ? tag.GetInt(nameof(SpDmg)) : 65;

                if (Moves == null)
                    Moves = new BaseMove[4];
                Moves[0] = tag.ContainsKey(BaseCaughtClass.MOVE1) ? TerramonMod.GetMove(tag.GetString(BaseCaughtClass.MOVE1)) : null;
                Moves[1] = tag.ContainsKey(BaseCaughtClass.MOVE2) ? TerramonMod.GetMove(tag.GetString(BaseCaughtClass.MOVE2)) : null;
                Moves[2] = tag.ContainsKey(BaseCaughtClass.MOVE3) ? TerramonMod.GetMove(tag.GetString(BaseCaughtClass.MOVE3)) : null;
                Moves[3] = tag.ContainsKey(BaseCaughtClass.MOVE4) ? TerramonMod.GetMove(tag.GetString(BaseCaughtClass.MOVE4)) : null;

                //Update all old pokebals
                bool retrofit = true;
                foreach (var it in Moves)
                    if (it != null)
                        retrofit = false;
                if (retrofit)
                {
                    var def = TerramonMod.GetPokemon(Pokemon).DefaultMove;
                    try //In case someone forgot leave nulls at empty moves 
                    {
                        for (int i = 0; i < 4; i++)
                        {

                            Moves[i] = TerramonMod.GetMove(def[i]);

                        }
                    }
                    catch (Exception e)
                    {
                        TerramonMod.Instance.Logger.Error($"Looks like someone from dev team don't fill moves field properly for {Pokemon} pokemon.\n" +
                                                          $"Please, report this to Terramon Team!");
                        TerramonMod.Instance.Logger.Error(e);
                    }
                }

                pokeballType = tag.GetByte(BaseCaughtClass.POKEBAL_PROPERTY);
            }
        }

        public TagCompound GetCompound() => new TagCompound()
        {
            [nameof(BaseCaughtClass.PokemonName)] = this.Pokemon,
            //[nameof(SmallSpritePath)] = SmallSpritePath,
            //
            //[nameof(SmallSpritePath)] = SmallSpritePath, // what do i do here
            //v2

            [nameof(BaseCaughtClass.isShiny)] = this.IsShiny,
            [nameof(BaseCaughtClass.CapturedPokemon)] = this.Pokemon,
            [nameof(BaseCaughtClass.Level)] = this.Level,
            [nameof(BaseCaughtClass.Exp)] = this.Exp,

            [nameof(MaxHP)] = this.MaxHP,
            [nameof(HP)] = this.HP,
            [nameof(PhysDmg)] = this.PhysDmg,
            [nameof(PhysDef)] = this.PhysDef,
            [nameof(SpDmg)] = this.SpDmg,
            [nameof(SpDef)] = this.SpDef,

            //Store move name
            [BaseCaughtClass.MOVE1] = this.Moves?[0]?.GetType().Name ?? "",
            [BaseCaughtClass.MOVE2] = this.Moves?[1]?.GetType().Name ?? "",
            [BaseCaughtClass.MOVE3] = this.Moves?[2]?.GetType().Name ?? "",
            [BaseCaughtClass.MOVE4] = this.Moves?[3]?.GetType().Name ?? "",
            //[nameof(Moves)] = from it in Moves select it.MoveName,
            //Used to restore items in sidebarUI
            [BaseCaughtClass.POKEBAL_PROPERTY] = this.pokeballType
        };


    }
}
