using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terramon.UI;
using Terraria;
// using Terraria.ID;
using Terraria.Achievements;
using Terraria.ModLoader;
using Terraria.UI;

namespace Terramon
{
    class Terramon : Mod
    {
        internal ChooseStarter ChooseStarter;
        internal PokegearUI PokegearUI;
        internal PokegearUIEvents PokegearUIEvents;
        internal evolveUI evolveUI;
        private UserInterface _exampleUserInterface; // Choose Starter
        private UserInterface _exampleUserInterfaceNew; // Pokegear Main Menu
        private UserInterface PokegearUserInterfaceNew;
        private UserInterface evolveUserInterfaceNew;// Pokegear Events Menu


        internal static Terramon Instance;

        public Terramon()
        {
            Instance = this;
            // By default, all Autoload properties are True. You only need to change this if you know what you are doing.
            //Properties = new ModProperties()
            //{
            //	Autoload = true,
            //	AutoloadGores = true,
            //	AutoloadSounds = true,
            //	AutoloadBackgrounds = true
            //};
        }

        private readonly static string[] balls = { "Pokeball",
                                                   "GreatBall",
                                                   "UltraBall",
                                                   "DuskBall" };

        // catch chance of the ball refers to the same index as the ball
        private readonly static float[][] catchChances = {
            new float[] { .1190f },
            new float[] { .1785f },
            new float[] { .2380f },
            new float[] { .2380f,
                          .1190f }
        };

        public static string[] GetBalls() => balls;

        public static string[] GetBallProjectiles()
        {
            string[] ballProjectiles = new string[balls.Length];
            for (int i = 0; i < balls.Length; i++)
            {
                ballProjectiles[i] = balls[i] + "Projectile";
            }

            return ballProjectiles;
        }

        public override void Load()
        {
            Mod achLib = ModLoader.GetMod("AchievementLib");
            if (achLib != null)
            {
                achLib.Call("AddAchievement", this, "Just the Beginning", "Choose your starter Pokémon.", ModContent.GetTexture("Terramon/Achievements/ach01-locked"), ModContent.GetTexture("Terramon/Achievements/ach01-unlocked"), AchievementCategory.Collector);
                achLib.Call("AddAchievement", this, "First Toss", "Throw your first Poké Ball.", ModContent.GetTexture("Terramon/Achievements/ach04-locked"), ModContent.GetTexture("Terramon/Achievements/ach04-unlocked"), AchievementCategory.Collector);
                achLib.Call("AddAchievement", this, "A Lot of Tosses", "Throw 25 Poké Balls.", ModContent.GetTexture("Terramon/Achievements/ach05-locked"), ModContent.GetTexture("Terramon/Achievements/ach05-unlocked"), AchievementCategory.Collector);
                achLib.Call("AddAchievement", this, "Great Toss", "Throw your first Great Ball.", ModContent.GetTexture("Terramon/Achievements/ach06-locked"), ModContent.GetTexture("Terramon/Achievements/ach06-unlocked"), AchievementCategory.Collector);
                achLib.Call("AddAchievement", this, "A Lot of Great Tosses", "Throw 25 Great Balls.", ModContent.GetTexture("Terramon/Achievements/ach07-locked"), ModContent.GetTexture("Terramon/Achievements/ach07-unlocked"), AchievementCategory.Collector);
                achLib.Call("AddAchievement", this, "Ultra Toss", "Throw your first Ultra Ball.", ModContent.GetTexture("Terramon/Achievements/ach08-locked"), ModContent.GetTexture("Terramon/Achievements/ach08-unlocked"), AchievementCategory.Collector);
                achLib.Call("AddAchievement", this, "A Lot of Ultra Tosses", "Throw 25 Ultra Balls.", ModContent.GetTexture("Terramon/Achievements/ach09-locked"), ModContent.GetTexture("Terramon/Achievements/ach09-unlocked"), AchievementCategory.Collector);
                achLib.Call("AddAchievement", this, "The New Arrival", "Get the Pokémon Trainer to move in.", ModContent.GetTexture("Terramon/Achievements/ach10-locked"), ModContent.GetTexture("Terramon/Achievements/ach10-unlocked"), AchievementCategory.Collector);
                achLib.Call("AddAchievement", this, "Retro Collector", "Purchase all 6 Game Boys.", ModContent.GetTexture("Terramon/Achievements/ach11-locked"), ModContent.GetTexture("Terramon/Achievements/ach11-unlocked"), AchievementCategory.Collector);
                achLib.Call("AddAchievement", this, "Beginner Trainer", "Catch 5 different Pokémon.", ModContent.GetTexture("Terramon/Achievements/ach12-locked"), ModContent.GetTexture("Terramon/Achievements/ach12-unlocked"), AchievementCategory.Collector);
            }
            ChooseStarter = new ChooseStarter();
            ChooseStarter.Activate();
            PokegearUI = new PokegearUI();
            PokegearUI.Activate();
            PokegearUIEvents = new PokegearUIEvents();
            PokegearUIEvents.Activate();
            evolveUI = new evolveUI();
            evolveUI.Activate();
            _exampleUserInterface = new UserInterface();
            _exampleUserInterfaceNew = new UserInterface();
            PokegearUserInterfaceNew = new UserInterface();
            evolveUserInterfaceNew = new UserInterface();
            _exampleUserInterface.SetState(ChooseStarter); // Choose Starter
            _exampleUserInterfaceNew.SetState(PokegearUI); // Pokegear Main Menu
            PokegearUserInterfaceNew.SetState(PokegearUIEvents); // Pokegear Events Menu
            evolveUserInterfaceNew.SetState(evolveUI); // evolve lmao menu lmao


        }

        public static float[][] GetCatchChances() => catchChances;

        /* public override void Load()
		{
			Main.music[MusicID.OverworldDay] = GetMusic("Sounds/Music/overworldnew");
			Main.music[MusicID.Night] = GetMusic("Sounds/Music/nighttime");
			Main.music[MusicID.AltOverworldDay] = GetMusic("Sounds/Music/overworldnew");
		} */

        // UI STUFF DOWN HERE

        public override void UpdateUI(GameTime gameTime)
        {
            if (ChooseStarter.Visible)
            {
                _exampleUserInterface?.Update(gameTime);
            }
            if (PokegearUI.Visible)
            {
                _exampleUserInterfaceNew?.Update(gameTime);
            }
            if (PokegearUIEvents.Visible)
            {
                PokegearUserInterfaceNew?.Update(gameTime);
            }
            if (evolveUI.Visible)
            {
                evolveUserInterfaceNew?.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ExampleMod: Coins Per Minute",
                    delegate
                    {
                        if (ChooseStarter.Visible)
                        {
                            _exampleUserInterface.Draw(Main.spriteBatch, new GameTime());
                        }
                        if (PokegearUI.Visible)
                        {
                            _exampleUserInterfaceNew.Draw(Main.spriteBatch, new GameTime());
                        }
                        if (PokegearUIEvents.Visible)
                        {
                            PokegearUserInterfaceNew.Draw(Main.spriteBatch, new GameTime());
                        }
                        if (evolveUI.Visible)
                        {
                            evolveUserInterfaceNew.Draw(Main.spriteBatch, new GameTime());
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }



        // END UI STUFF
    }
}
