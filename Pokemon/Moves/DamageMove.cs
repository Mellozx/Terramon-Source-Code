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
        public virtual bool Special => false;

        public DamageMove()
        {
            PostTextLoc =
                TerramonMod.Localisation.GetLocalisedString(new LocalisedString(("moves.baseDamageText", "{0} attacked {1} with {2} for {3} damage")));
        }

        public override bool PerformInBattle(ParentPokemon mon, ParentPokemon target, TerramonPlayer player, PokemonData attacker,
            PokemonData deffender)
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
            PokemonData deffender)
        {
            var p = (float)Damage / 100;
            float d = -1;
            if (!Special)
            {
                d = (((((float)attacker.Level * 2) / 5 + 2) * p * ((float)attacker.PhysDmg / deffender.PhysDef))
                     / 50) + 2;
            }
            else
            {
                d = (((((float)attacker.Level * 2) / 5 + 2) * p * ((float)attacker.SpDmg / deffender.SpDef))
                     / 50) + 2;
            }
            //float d = !Special ? (((((float)attacker.Level * 2) / 5 + 2) * p * ((float)attacker.PhysDmg / deffender.PhysDef)) / 50) + 2:
            //    (((((float)attacker.Level * 2) / 5 + 2) * p * ((float)attacker.SpDmg / deffender.SpDef)) / 50) + 2;
            foreach (var it in deffender.Types)
            {
                var r = it.GetResist(MoveType);
                if (r != 1f)
                {
                    d *= r;
                    break;
                }
            }

            Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/UI/BattleSFX/Damage1").WithVolume(.8f));
            d = deffender.Damage((int)Math.Abs(d));
            target.damageReceived = true;
            PostTextLoc.Args = new object[] { attacker.PokemonName, deffender.PokemonName, MoveName, (int)d };
            return d;
        }
    }
}
