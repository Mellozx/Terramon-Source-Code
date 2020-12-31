using Microsoft.Xna.Framework;
using Razorwing.Framework.Graphics;
using Terramon.Players;
using Terramon.UI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terramon.Pokemon.Moves
{
    public class QuickAttack : DamageMove
    {
		public override string MoveName => "Quick Attack";
		public override string MoveDescription => "An attack that always strikes first. If both Pokémon use this, the one with higher Speed attacks first.W";
		public override int Damage => 50;
		public override int Accuracy => 100;
		public override int MaxPP => 30;
		public override int MaxBoostPP => 48;
		public override bool MakesContact => true;
		public override bool Special => false;
		public override Target Target => Target.Opponent;
		public override int Cooldown => 60 * 1; 
		public override PokemonType MoveType => PokemonType.Normal;
		public override int Priority => 1;

		public override int AutoUseWeight(ParentPokemon mon, Vector2 pos, TerramonPlayer player)
		{
			NPC target = GetNearestNPC(pos);
			if (target == null)
				return 0;
			return 30;
		}

		

		public override bool AnimateTurn(ParentPokemon mon, ParentPokemon target, TerramonPlayer player, PokemonData attacker,
			PokemonData deffender, BattleState state, bool opponent)
		{
			if (AnimationFrame == 1) //At initial frame we pan camera to attacker
			{
				TerramonMod.ZoomAnimator.ScreenPosX(mon.projectile.position.X + 12, 500, Easing.OutExpo);
				TerramonMod.ZoomAnimator.ScreenPosY(mon.projectile.position.Y, 500, Easing.OutExpo);
			}
			else if (AnimationFrame == 140)
			{
				BattleMode.UI.splashText.SetText("");

				TerramonMod.ZoomAnimator.ScreenPosX(target.projectile.position.X + 12, 500, Easing.OutExpo);
				TerramonMod.ZoomAnimator.ScreenPosY(target.projectile.position.Y, 500, Easing.OutExpo);
			}
			else if (AnimationFrame == 165)
			{
				Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/UI/BattleSFX/Damage0" ).WithVolume(.75f));
			}
			else if (AnimationFrame == 200)
			{
				InflictDamage(mon, target, player, attacker, deffender, state, opponent);
				if (PostTextLoc.Args.Length >= 4) //If we can extract damage number
					CombatText.NewText(target.projectile.Hitbox, CombatText.DamagedHostile, (int)PostTextLoc.Args[3]); //Print combat text at attacked mon position
				BattleMode.queueEndMove = true;
			}

			// This should be at the very bottom of AnimateTurn() in every move.
			if (BattleMode.moveEnd)
			{
				AnimationFrame = 0;
				BattleMode.moveEnd = false;
				return false;
			}

			// IGNORE EVERYTHING BELOW WHEN MAKING YOUR OWN MOVES.
			if (AnimationFrame > 1810) return false;

			return true;
		}

	}
}

