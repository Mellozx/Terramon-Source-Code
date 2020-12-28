using System.Collections.Generic;
using System.ComponentModel;
using Terramon.UI.SidebarParty;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace Terramon
{
    public class Config : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("Customize Starters")]

        [DefaultValue("Bulbasaur")]
        [Label("Kanto Starter Pokémon #1")]
        [Tooltip("You can change this to any Pokémon in the mod. Make sure the name is written exactly how it appears in-game!\nIf it isn't spelled correctly or not yet implemented in the mod, it will resort to default (Bulbasaur).")]
        public string kantoStarterPokemon1;

        [DefaultValue("Charmander")]
        [Label("Kanto Starter Pokémon #2")]
        [Tooltip("You can change this to any Pokémon in the mod. Make sure the name is written exactly how it appears in-game!\nIf it isn't spelled correctly or not yet implemented in the mod, it will resort to default (Charmander).")]
        public string kantoStarterPokemon2;

        [DefaultValue("Squirtle")]
        [Label("Kanto Starter Pokémon #3")]
        [Tooltip("You can change this to any Pokémon in the mod. Make sure the name is written exactly how it appears in-game!\nIf it isn't spelled correctly or not yet implemented in the mod, it will resort to default (Squirtle).")]
        public string kantoStarterPokemon3;

        public override void OnChanged()
        {
            UISidebar uISidebar = ModContent.GetInstance<TerramonMod>().UISidebar;
            if (uISidebar != null)
            {
                if (!TerramonMod.ShowHelpButton)
                    uISidebar.RemoveChild(uISidebar.choose);
                else
                    uISidebar.Append(uISidebar.choose);
            }
        }
    }
}