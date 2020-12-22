using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Razorwing.Framework.Localisation;
using Terramon.Players;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.Moves
{
    public abstract class DamageMove : BaseMove
    {
        public abstract int Damage { get; }// Perc 200-100

        public virtual int Accuracy => 100;
        public bool Miss => _mrand.Next(100) > Accuracy;
        public virtual bool Special => false;
        public virtual bool MakesContact => false;

        public DamageMove()
        {
            PostTextLoc =
                TerramonMod.Localisation.GetLocalisedString(new LocalisedString(("moves.baseDamageText", "{0} attacked {1} with {2} for {3} damage")));
            
        }

        public override bool PerformInBattle(ParentPokemon mon, ParentPokemon target, TerramonPlayer player, PokemonData attacker,
            PokemonData deffender, BaseMove move)
        {
            if (player == null)
            {
                BattleMode.UI.splashText.SetText($"The wild {attacker.PokemonName} used {MoveName}!");
            } else
            {
                BattleMode.UI.splashText.SetText($"{attacker.PokemonName} used {MoveName}!");
            }
            return true;
        }

        public float InflictDamage(ParentPokemon mon, ParentPokemon target, TerramonPlayer player, PokemonData attacker,
            PokemonData deffender, BattleState state, bool opponent, bool checkZeroResist = true)
        {
            // Check if move can even inflict damage. For example, normal type moves cannot damage ghost type Pokemon.
            // Call InflictDamage() with checkZeroResist = false to skip this check
            if (checkZeroResist && 
            target.PokemonTypes.Contains(PokemonType.Ghost) && MoveType == PokemonType.Normal || 
            target.PokemonTypes.Contains(PokemonType.Ghost) && MoveType == PokemonType.Fighting || 
            target.PokemonTypes.Contains(PokemonType.Steel) && MoveType == PokemonType.Poison || 
            target.PokemonTypes.Contains(PokemonType.Flying) && MoveType == PokemonType.Ground || 
            target.PokemonTypes.Contains(PokemonType.Normal) && MoveType == PokemonType.Ghost || 
            target.PokemonTypes.Contains(PokemonType.Ground) && MoveType == PokemonType.Electric || 
            target.PokemonTypes.Contains(PokemonType.Dark) && MoveType == PokemonType.Psychic || 
            target.PokemonTypes.Contains(PokemonType.Fairy) && MoveType == PokemonType.Dragon)
            {
                if (opponent) BattleMode.UI.splashText.SetText($"It doesn't affect {deffender.PokemonName}!");
                else if (state == BattleState.BattleWithWild)
                {
                    BattleMode.UI.splashText.SetText($"It doesn't affect the wild {deffender.PokemonName}...");
                } else if (state == BattleState.BattleWithPlayer)
                {
                    BattleMode.UI.splashText.SetText($"It doesn't affect the foe's {deffender.PokemonName}...");
                }
                BattleMode.endMoveTimer = -120;
                return 0f;
            }

            float dmg;

            int attackerphysDefModifier = 0;
            int attackerspDefModifier = 0;
            int attackercritRatioModifier = 0;

            int deffenderphysDefModifier = 0;
            int deffenderspDefModifier = 0;

            bool critical = false;

            if (attacker.CustomData.ContainsKey("PhysDefModifier")) attackerphysDefModifier = int.Parse(attacker.CustomData["PhysDefModifier"]);
            if (attacker.CustomData.ContainsKey("SpDefModifier")) attackerspDefModifier = int.Parse(attacker.CustomData["SpDefModifier"]);
            if (attacker.CustomData.ContainsKey("CritRatioModifier")) attackercritRatioModifier = int.Parse(attacker.CustomData["CritRatioModifier"]);
            
            if (deffender.CustomData.ContainsKey("PhysDefModifier")) deffenderphysDefModifier = int.Parse(deffender.CustomData["PhysDefModifier"]);
            if (deffender.CustomData.ContainsKey("SpDefModifier")) deffenderspDefModifier = int.Parse(deffender.CustomData["SpDefModifier"]);

            // Same type attack bonus (STAB)
            if (mon.PokemonTypes.Length > 1) { if (mon.PokemonTypes[0] == MoveType || mon.PokemonTypes[1] == MoveType) dmg = Damage * 1.5f; else dmg = Damage; } else { if (mon.PokemonTypes[0] == MoveType) dmg = Damage * 1.5f; else dmg = Damage; }

            var p = dmg / 100;
            float d = -1;

            // critical hit chance
            if (_mrand.Next(1, GetCritChance(attackercritRatioModifier)) == 1)
            {
                critical = true;
                // ignore attackers negative stat stages
                if (attackerphysDefModifier < 0) attackerphysDefModifier = 0;
                if (attackerspDefModifier < 0) attackerspDefModifier = 0;
                // ignore defenders positive stat stages
                if (deffenderphysDefModifier > 0) deffenderphysDefModifier = 0;
                if (deffenderspDefModifier > 0) deffenderspDefModifier = 0;
            }

            if (!Special)
            {
                d = (((((float)attacker.Level * 2) / 5 + 2) * p * ((float)attacker.PhysDmg / deffender.PhysDef + deffenderphysDefModifier))
                     / 50) + 2;
            }
            else
            {
                d = (((((float)attacker.Level * 2) / 5 + 2) * p * ((float)attacker.SpDmg / deffender.SpDef + deffenderspDefModifier))
                     / 50) + 2;
            }

            // Move resist
            float r1 = 1f, r2 = 1f;
            r1 = deffender.Types[0].GetResist(MoveType);
            if (deffender.Types.Length > 1) r2 = deffender.Types[1].GetResist(MoveType);
            d *= r1 * r2;

            if (!critical)
            {
                if (r1 * r2 < 0.6f) Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/UI/BattleSFX/Damage0").WithVolume(.8f));
                else Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/UI/BattleSFX/Damage1").WithVolume(.8f));
            }
            else
            {
                d *= 2;
                Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/UI/BattleSFX/Damage2").WithVolume(.8f));
            }
            d = deffender.Damage((int)Math.Abs(d));
            target.damageReceived = true;
            PostTextLoc.Args = new object[] { attacker.PokemonName, deffender.PokemonName, MoveName, (int)d };
            return d;
        }

        /// <summary>
        /// Restores HP to a given Pokemon
        /// </summary>
        /// <param name="pokemon">The Pokemon data of the target</param>
        /// <param name="target">The Pokemon projectile of the target</param>
        /// <param name="amount">The integer amount to heal</param>
        public void SelfHeal(PokemonData pokemon, ParentPokemon target, int amount)
        {
            Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/UI/BattleSFX/Heal0").WithVolume(.8f));
            pokemon.HP += amount;
            target.healedHealth = true;
        }

        /// <summary>
        /// Adjusts modifier of the given stat for a target
        /// Returns string like "Bulbasaur's defense rose sharply!".
        /// </summary>
        /// <param name="pokemon">The Pokemon data of the target</param>
        /// <param name="target">The Pokemon projectile of the target</param>
        /// <param name="stat">String value of the stat to be adjusted</param>
        /// <param name="modifier">How many points to modify</param>
        /// <param name="state">The current BattleState</param>
        /// <param name="opponent">Whether or not this being called from wild Pokemon or trainer</param>
        public ILocalisedBindableString ModifyStat(PokemonData pokemon, ParentPokemon target, GetStat stat, int modifier, BattleState state, bool opponent)
        {
            ILocalisedBindableString text;

            string statname = "";
            string adjustment = "";

            if (opponent)
            {
               text = TerramonMod.Localisation.GetLocalisedString(new LocalisedString(("moves.modifyStatText", "{0}'s {1} {2}")));
            } else
            {
               text = TerramonMod.Localisation.GetLocalisedString(new LocalisedString(("moves.modifyStatText", "Wild {0}'s {1} {2}")));
            }

            if (modifier == -3) adjustment = "severely fell!";
            if (modifier == -2) adjustment = "harshly fell!";
            if (modifier == -1) adjustment = "fell!";
            if (modifier == 1) adjustment = "rose!";
            if (modifier == 2) adjustment = "sharply rose!";
            if (modifier == 3) adjustment = "drastically rose!";

            if (stat == GetStat.Defense)
            {
                statname = "Defense";
                if (pokemon.CustomData.ContainsKey("PhysDefModifier"))
                {
                    if (int.Parse(pokemon.CustomData["PhysDefModifier"]) == 6 && modifier > 0)
                    {
                        pokemon.CustomData["PhysDefModifier"] = "6";
                        adjustment = "won't go higher!";
                        text.Args = new object[]
                        {
                            pokemon.PokemonName,
                            statname,
                            adjustment
                        };
                        return text; // Cant go any higher!
                    }
                    else if (int.Parse(pokemon.CustomData["PhysDefModifier"]) == -6 && modifier < 0)
                    {
                        pokemon.CustomData["PhysDefModifier"] = "-6";
                        adjustment = "won't go lower!";
                        text.Args = new object[]
                        {
                            pokemon.PokemonName,
                            statname,
                            adjustment
                        };
                        return text; // Cant go any lower!
                    }
                }

                if (pokemon.CustomData.ContainsKey("PhysDefModifier"))
                {
                    int a = int.Parse(pokemon.CustomData["PhysDefModifier"]);
                    int b = modifier;
                    pokemon.CustomData["PhysDefModifier"] = (a + b).ToString();
                    if (modifier > 0)
                    {
                        Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/UI/BattleSFX/StatRise").WithVolume(.8f));
                        target.statModifiedUp = true;
                    }
                    else
                    {
                        Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/UI/BattleSFX/StatFall").WithVolume(.8f));
                        target.statModifiedDown = true;
                    }
                }
                else
                {
                    pokemon.CustomData.Add("PhysDefModifier", modifier.ToString());
                    if (modifier > 0)
                    {
                        Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/UI/BattleSFX/StatRise").WithVolume(.8f));
                        target.statModifiedUp = true;
                    }
                    else
                    {
                        Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/UI/BattleSFX/StatFall").WithVolume(.8f));
                        target.statModifiedDown = true;
                    }
                }

                if (int.Parse(pokemon.CustomData["PhysDefModifier"]) > 6)
                {
                    pokemon.CustomData["PhysDefModifier"] = "6";
                }

                if (int.Parse(pokemon.CustomData["PhysDefModifier"]) < -6)
                {
                    pokemon.CustomData["PhysDefModifier"] = "-6";
                }

                text.Args = new object[]
                {
                    pokemon.PokemonName,
                    statname,
                    adjustment
                };
                return text;
            }

            if (stat == GetStat.SpDef)
            {
                statname = "Special Defense";
                if (pokemon.CustomData.ContainsKey("SpDefModifier"))
                {
                    if (int.Parse(pokemon.CustomData["SpDefModifier"]) == 6 && modifier > 0)
                    {
                        pokemon.CustomData["SpDefModifier"] = "6";
                        adjustment = "won't go higher!";
                        text.Args = new object[]
                        {
                            pokemon.PokemonName,
                            statname,
                            adjustment
                        };
                        return text; // Cant go any higher!
                    }
                    else if (int.Parse(pokemon.CustomData["SpDefModifier"]) == -6 && modifier < 0)
                    {
                        pokemon.CustomData["SpDefModifier"] = "-6";
                        adjustment = "won't go lower!";
                        text.Args = new object[]
                        {
                            pokemon.PokemonName,
                            statname,
                            adjustment
                        };
                        return text; // Cant go any lower!
                    }
                }

                if (pokemon.CustomData.ContainsKey("SpDefModifier"))
                {
                    int a = int.Parse(pokemon.CustomData["SpDefModifier"]);
                    int b = modifier;
                    pokemon.CustomData["SpDefModifier"] = (a + b).ToString();
                    if (modifier > 0)
                    {
                        Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/UI/BattleSFX/StatRise").WithVolume(.8f));
                        target.statModifiedUp = true;
                    }
                    else
                    {
                        Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/UI/BattleSFX/StatFall").WithVolume(.8f));
                        target.statModifiedDown = true;
                    }
                }
                else
                {
                    pokemon.CustomData.Add("SpDefModifier", modifier.ToString());
                    if (modifier > 0)
                    {
                        Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/UI/BattleSFX/StatRise").WithVolume(.8f));
                        target.statModifiedUp = true;
                    }
                    else
                    {
                        Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/UI/BattleSFX/StatFall").WithVolume(.8f));
                        target.statModifiedDown = true;
                    }
                }

                if (int.Parse(pokemon.CustomData["SpDefModifier"]) > 6)
                {
                    pokemon.CustomData["SpDefModifier"] = "6";
                }

                if (int.Parse(pokemon.CustomData["SpDefModifier"]) < -6)
                {
                    pokemon.CustomData["SpDefModifier"] = "-6";
                }

                text.Args = new object[]
                {
                    pokemon.PokemonName,
                    statname,
                    adjustment
                };
                return text;
            }

            // Pseudo-statistic
            if (stat == GetStat.CritRatio)
            {
                if (opponent) text = TerramonMod.Localisation.GetLocalisedString(new LocalisedString(("moves.modifyStatText", "{0} is getting pumped!")));
                else text = TerramonMod.Localisation.GetLocalisedString(new LocalisedString(("moves.modifyStatText", "The wild {0} is getting pumped!")));

                Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/UI/BattleSFX/StatRise").WithVolume(.8f));
                
                if (pokemon.CustomData.ContainsKey("CritRatioModifier"))
                {
                    if (int.Parse(pokemon.CustomData["CritRatioModifier"]) + modifier > 5) // Going past maximum
                    {
                        if (opponent) text = TerramonMod.Localisation.GetLocalisedString(new LocalisedString(("moves.modifyStatText", "{0} is overflowing with energy!")));
                        else text = TerramonMod.Localisation.GetLocalisedString(new LocalisedString(("moves.modifyStatText", "The wild {0} is overflowing with energy!")));
                        text.Args = new object[]
                        {
                            pokemon.PokemonName
                        };
                        target.gettingPumped = true;
                        return text;
                    }
                    pokemon.CustomData["CritRatioModifier"] = (int.Parse(pokemon.CustomData["CritRatioModifier"]) + modifier).ToString();
                    text.Args = new object[]
                    {
                        pokemon.PokemonName
                    };
                    target.gettingPumped = true;
                    return text;
                } else
                {
                    pokemon.CustomData.Add("CritRatioModifier", modifier.ToString());
                    text.Args = new object[]
                    {
                        pokemon.PokemonName
                    };
                    target.gettingPumped = true;
                    return text;
                }
            }

            return text;
        }

        // https://bulbapedia.bulbagarden.net/wiki/Critical_hit#Probability
        public int GetCritChance(int stage)
        {
            if (stage == 0) return 16; // 6.25%
            if (stage == 1) return 8; // 12.5%
            if (stage == 2) return 2; // 50%
            if (stage >= 3) return 1; // 100%
            return 16;
        }
        public enum GetStat
        {
            HP,
            Attack,
            Defense,
            SpAtk,
            SpDef,
            Speed,
            CritRatio // pseudo-statistic
        }
    }
}
