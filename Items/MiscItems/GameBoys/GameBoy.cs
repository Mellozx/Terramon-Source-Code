using Terraria;
using Terraria.ModLoader;

namespace Terramon.Items.MiscItems.GameBoys
{
    public abstract class GameBoy : TerramonItem
    {
        /// <summary></summary>
        /// <param name="color"></param>
        /// <param name="tooltip"></param>
        /// <param name="value"></param>
        /// <param name="rarity"></param>
        /// <param name="musicPath">The right-most part of the music file, i.e. TitleScreen for GB_TitleScreen.</param>
        protected GameBoy(string color, string tooltip, int value, int rarity, string musicPath) : base(
            $"Game Boy ({color})", tooltip, 22, 32, value, 0, rarity)
        {
            MusicPath = musicPath;
        }


        public override void SetDefaults()
        {
            base.SetDefaults();

            item.maxStack = 1;

            item.useTime = 34;
            item.useAnimation = 34;
            item.useStyle = 4;

            item.accessory = true;
            item.autoReuse = false;
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            Main.musicBox2 = mod.GetSoundSlot(SoundType.Music, $"Sounds/Music/GB_{MusicPath}");
        }


        public string MusicPath { get; }
    }
}