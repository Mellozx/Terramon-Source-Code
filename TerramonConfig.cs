using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Terramon.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;
using Terraria.UI;

namespace Terramon
{  
    public class TerramonConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(1)]
        [Label("Item Name Colors")]
        [Tooltip("Set to 1 for default (two colors in one, colored based on sprite base color)\nSet to 2 for no multicolored item names\nSet to 3 for blank white item names")]
        public int ItemNameColors;

        [DefaultValue(1)]
        [Label("Language Configuration")]
        [Tooltip("1 = English (Default)")]
        public int Language;
    }
}
