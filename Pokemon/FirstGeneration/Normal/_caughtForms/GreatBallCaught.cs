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
    public class GreatBallCaught : BasePokeballCaught
    {
        public int PokemonNPCGreat;
        public string PokemonNameGreat;
        public string SmallSpritePath;
        public override bool CloneNewInstances => true;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Great Ball");
            Tooltip.SetDefault("Contains %PokemonName"
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
                nameLine.text = "Great Ball (" + PokemonNameGreat + ")";
            }
        }

        public override TagCompound Save()
        {
            return new TagCompound
            {
                [nameof(PokemonNPCGreat)] = PokemonNPCGreat,
                [nameof(PokemonNameGreat)] = PokemonNameGreat,
                [nameof(SmallSpritePath)] = SmallSpritePath,
            };
        }
        public override void Load(TagCompound tag)
        {
            PokemonNPCGreat = tag.GetInt(nameof(PokemonNPCGreat));
            PokemonNameGreat = tag.GetString(nameof(PokemonNameGreat));
            SmallSpritePath = tag.GetString(nameof(SmallSpritePath));
        }

        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(PokemonNPCGreat);
            writer.Write(PokemonNameGreat);
            writer.Write(SmallSpritePath);
        }

        public override void NetRecieve(BinaryReader reader)
        {
            PokemonNPCGreat = reader.ReadInt32();
            PokemonNameGreat = reader.ReadString();
            SmallSpritePath = reader.ReadString();
        }
    }
}
