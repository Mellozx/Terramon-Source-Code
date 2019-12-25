﻿using Microsoft.Xna.Framework;
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
    public class PremierBallCaught : ModItem
    {
        public int PokemonNPCPremier;
        public string PokemonNamePremier;
        public string SmallSpritePath;
        public override bool CloneNewInstances => true;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Premier Ball");
            Tooltip.SetDefault("Contains " + PokemonNamePremier
                + "\nLeft click to send out this Pokémon."
                + "\nRight click to add to your party.");
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

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine nameLine = tooltips.FirstOrDefault(t => t.Name == "ItemName" && t.mod == "Terraria");
            if (nameLine != null)
            {
                nameLine.text = "Premier Ball (" + PokemonNamePremier + ")";
            }

            string tooltipText = tooltips.Find(x => x.Name == "Tooltip0").text;
            tooltipText = tooltipText.Replace("%PokemonName", PokemonNamePremier);

            tooltips.Find(x => x.Name == "Tooltip0").text = tooltipText;
            base.ModifyTooltips(tooltips);
        }

        public override TagCompound Save()
        {
            return new TagCompound
            {
                [nameof(PokemonNPCPremier)] = PokemonNPCPremier,
                [nameof(PokemonNamePremier)] = PokemonNamePremier,
                [nameof(SmallSpritePath)] = SmallSpritePath,
            };
        }
        public override void Load(TagCompound tag)
        {
            PokemonNPCPremier = tag.GetInt(nameof(PokemonNPCPremier));
            PokemonNamePremier = tag.GetString(nameof(PokemonNamePremier));
            SmallSpritePath = tag.GetString(nameof(SmallSpritePath));
        }

        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(PokemonNPCPremier);
            writer.Write(PokemonNamePremier);
            writer.Write(SmallSpritePath);
        }

        public override void NetRecieve(BinaryReader reader)
        {
            PokemonNPCPremier = reader.ReadInt32();
            PokemonNamePremier = reader.ReadString();
            SmallSpritePath = reader.ReadString();
        }
    }
}