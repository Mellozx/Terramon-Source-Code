using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terramon.Pokemon
{
    [AutoloadHead]
    public class PokemonTrainer : ModNPC
    {
        public override string Texture
        {
            get
            {
                return "Terramon/Pokemon/PokemonTrainer";
            }
        }

        public override bool Autoload(ref string name)
        {
            name = "Pokémon Trainer";
            return mod.Properties.Autoload;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pokémon Trainer");
            Main.npcFrameCount[npc.type] = 26;
            NPCID.Sets.ExtraFramesCount[npc.type] = 9;
            NPCID.Sets.AttackFrameCount[npc.type] = 4;
            NPCID.Sets.DangerDetectRange[npc.type] = 700;
            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackTime[npc.type] = 90;
            NPCID.Sets.AttackAverageChance[npc.type] = 30;
            NPCID.Sets.HatOffsetY[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 26;
            npc.height = 44;
            npc.aiStyle = 7;
            npc.damage = 10;
            npc.defense = 15;
            npc.lifeMax = 250;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            animationType = NPCID.Guide;
        }

        public override string TownNPCName()
        {
            switch (WorldGen.genRand.Next(4))
            {
                case 0:
                    return "Red";
                case 1:
                    return "Red";
                case 2:
                    return "Red";
                default:
                    return "Red";
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frame.Width = 40;
                npc.frame.X = 0;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            return true;
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            shop.item[nextSlot].SetDefaults(mod.ItemType("Pokeball"));
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("GreatBall"));
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("UltraBall"));
            nextSlot++;
            if (!Main.dayTime)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("DuskBall"));
                nextSlot++;
            }
            if (NPC.downedBoss1)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("GameBoy"));
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType("GameBoyBlue"));
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType("GameBoyPink"));
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType("GameBoyPurple"));
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType("GameBoyTurquoise"));
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType("GameBoyYellow"));
                nextSlot++;
            }
            //gray blue pink purple turquoise yellow
        }

        public override string GetChat()
        {
                switch (Main.rand.Next(8))
                {
                    case 0:
                        return "There's a lot of Pokemon out in the world, but you need Pokeballs to catch 'em!";
                    case 1:
                        return "I just got back from my Alola vacation. See my tan lines?";
                    case 2:
                        return "Pokemon are sorted into different tiers. If you trigger a tier upgrade, Pokemon in a higher tier may start appearing!";
                    case 3:
                        return "While carrying your Pokedex, '/pokedex' will open it up!";
                    case 4:
                        return "Pokemon will appear in their respective biomes. To encounter rock types, go to the Desert.";
                    case 5:
                        return "Gotta Catch Em' All!";
                    case 6:
                        return "As your journey progresses, I'll be selling new things. Check back here every so often.";
                    case 7:
                        return "Rare Candies can level up your Pokemon. They live up to their name by being quite challenging to find!";
                    default:
                        return "Gotta Catch Em' All!";
                }
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
            //button2 = "Evolve";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                shop = true;
            }
            //else 
            //{
                //Main.playerInventory = true;
                //Main.npcChatText = "";
                //evolveUI.Visible = true;
            //}
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 5;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 30;
            randExtraCooldown = 30;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = mod.ProjectileType("RedBall");
            attackDelay = 2;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 12f;
            randomOffset = 2f;
        }
    }
}
