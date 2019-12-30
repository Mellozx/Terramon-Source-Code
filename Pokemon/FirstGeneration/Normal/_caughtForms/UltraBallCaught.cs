using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terramon.Achievements;
using Terramon.Items.Pokeballs.Thrown;
using Terramon.Players;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;

namespace Terramon.Pokemon.FirstGeneration.Normal._caughtForms
{
    public class UltraBallCaught : ModItem
    {
        public int PokemonNPCUltra;
        public string PokemonNameUltra;
        public string SmallSpritePath;
        public override bool CloneNewInstances => true;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Ultra Ball");
            Tooltip.SetDefault("Contains %PokemonName"
                + "\nLeft click to send out this Pokémon."
                + "\nRight click to add to your party.");
        }
        public override void SetDefaults()
        {

            item.damage = 0;

            item.width = 24;
            item.height = 24;

            item.useTime = 20;
            item.useStyle = 1;
            item.useAnimation = 20;

            item.UseSound = SoundID.Item2;
            item.accessory = false;
            item.shoot = 10;

            item.noMelee = true;

            item.rare = 0;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.HasBuff(mod.BuffType(PokemonNameUltra + "Buff")))
            {
                player.ClearBuff(mod.BuffType(PokemonNameUltra + "Buff"));
                switch (Main.rand.Next(3))
                {
                    case 0:
                        CombatText.NewText(player.Hitbox, Color.White, PokemonNameUltra + ", switch out!\nCome back!", true, false);
                        break;
                    case 1:
                        CombatText.NewText(player.Hitbox, Color.White, PokemonNameUltra + ", return!", true, false);
                        break;
                    default:
                        CombatText.NewText(player.Hitbox, Color.White, "That's enough for now, " + PokemonNameUltra + "!", true, false);
                        break;
                }
                return true;
            }
            else
                player.AddBuff(mod.BuffType(PokemonNameUltra + "Buff"), 2);
            CombatText.NewText(player.Hitbox, Color.White, "Go! " + PokemonNameUltra + "!", true, false);
            return true;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (SmallSpritePath == null)
                return true;
            Texture2D pokemonTexture = GetTexture(SmallSpritePath);
            Texture2D itemTexture = Main.itemTexture[item.type];
            spriteBatch.Draw(itemTexture, position, frame, drawColor, 0f, origin, scale, SpriteEffects.None, 0);
            spriteBatch.Draw(pokemonTexture, position + itemTexture.Size() * Main.inventoryScale - new Vector2(5, 5), pokemonTexture.Frame(), drawColor, 0f, pokemonTexture.Size() / 2f, Main.inventoryScale, SpriteEffects.None, 0);
            return false;
        }
        public override bool CanRightClick()
        {
            if (!ModContent.GetInstance<TerramonMod>().PartySlots.partyslot1.Item.IsAir && !ModContent.GetInstance<TerramonMod>().PartySlots.partyslot2.Item.IsAir && !ModContent.GetInstance<TerramonMod>().PartySlots.partyslot3.Item.IsAir && !ModContent.GetInstance<TerramonMod>().PartySlots.partyslot4.Item.IsAir && !ModContent.GetInstance<TerramonMod>().PartySlots.partyslot5.Item.IsAir && !ModContent.GetInstance<TerramonMod>().PartySlots.partyslot6.Item.IsAir)
            {
                return false;
            }
            return true;
        }
        public override void RightClick(Player player)
        {
            if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot1.Item.IsAir)
            {
                ModContent.GetInstance<TerramonMod>().PartySlots.partyslot1.Item = item.Clone();
                item.TurnToAir();
            }
            else

            if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot2.Item.IsAir)
            {
                ModContent.GetInstance<TerramonMod>().PartySlots.partyslot2.Item = item.Clone();
                item.TurnToAir();
            }
            else

            if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot3.Item.IsAir)
            {
                ModContent.GetInstance<TerramonMod>().PartySlots.partyslot3.Item = item.Clone();
                item.TurnToAir();
            }
            else

            if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot4.Item.IsAir)
            {
                ModContent.GetInstance<TerramonMod>().PartySlots.partyslot4.Item = item.Clone();
                item.TurnToAir();
            }
            else

            if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot5.Item.IsAir)
            {
                ModContent.GetInstance<TerramonMod>().PartySlots.partyslot5.Item = item.Clone();
                item.TurnToAir();
            }
            else

            if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot6.Item.IsAir)
            {
                ModContent.GetInstance<TerramonMod>().PartySlots.partyslot6.Item = item.Clone();
                item.TurnToAir();
            }
            else
            {
                Main.NewText("All Party Slots are full", 255, 240, 20, false);
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine nameLine = tooltips.FirstOrDefault(t => t.Name == "ItemName" && t.mod == "Terraria");
            if (nameLine != null)
            {
                nameLine.text = "Ultra Ball (" + PokemonNameUltra + ")";
            }

            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(245, 218, 83);
                }
            }

            string tooltipText = tooltips.Find(x => x.Name == "Tooltip0").text;
            tooltipText = tooltipText.Replace("%PokemonName", PokemonNameUltra);

            tooltips.Find(x => x.Name == "Tooltip0").text = tooltipText;
            base.ModifyTooltips(tooltips);
        }

        public override TagCompound Save()
        {
            return new TagCompound
            {
                [nameof(PokemonNPCUltra)] = PokemonNPCUltra,
                [nameof(PokemonNameUltra)] = PokemonNameUltra,
                [nameof(SmallSpritePath)] = SmallSpritePath,
            };
        }
        public override void Load(TagCompound tag)
        {
            PokemonNPCUltra = tag.GetInt(nameof(PokemonNPCUltra));
            PokemonNameUltra = tag.GetString(nameof(PokemonNameUltra));
            SmallSpritePath = tag.GetString(nameof(SmallSpritePath));
        }

        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(PokemonNPCUltra);
            writer.Write(PokemonNameUltra);
            writer.Write(SmallSpritePath);
        }

        public override void NetRecieve(BinaryReader reader)
        {
            PokemonNPCUltra = reader.ReadInt32();
            PokemonNameUltra = reader.ReadString();
            SmallSpritePath = reader.ReadString();
        }
    }
}
