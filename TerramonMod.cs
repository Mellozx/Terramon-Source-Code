using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Terramon.UI;
using Terraria;
// using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace Terramon
{
    public class TerramonMod : Mod
    {
        internal ChooseStarter ChooseStarter;
        internal PokegearUI PokegearUI;
        internal PokegearUIEvents PokegearUIEvents;
        internal evolveUI evolveUI;
        private UserInterface _exampleUserInterface; // Choose Starter
        private UserInterface _exampleUserInterfaceNew; // Pokegear Main Menu
        private UserInterface PokegearUserInterfaceNew;
        private UserInterface evolveUserInterfaceNew;// Pokegear Events Menu


        public TerramonMod()
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
                                                   "DuskBall",
                                                    "PremierBall"};

        // catch chance of the ball refers to the same index as the ball
        private readonly static float[][] catchChances = {
            new float[] { .1190f },
            new float[] { .1785f },
            new float[] { .2380f },
            new float[] { .2380f,
                          .1190f },
            new float[] { .1190f }
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

            if (Main.dedServ)
                return;

            FirstPKMAbility = this.RegisterHotKey("First Pokémon Ability", Keys.Z.ToString());
            SecondPKMAbility = this.RegisterHotKey("Second Pokémon Ability", Keys.X.ToString());
            ThirdPKMAbility = this.RegisterHotKey("Third Pokémon Ability", Keys.C.ToString());
            FourthPKMAbility = this.RegisterHotKey("Fourth Pokémon Ability", Keys.V.ToString());
        }

        public override void Unload()
        {
            Instance = null;
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
                    "Terramon: Pokemon Interfaces",
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


        #region HotKeys

        public ModHotKey FirstPKMAbility { get; private set; }
        public ModHotKey SecondPKMAbility { get; private set; }
        public ModHotKey ThirdPKMAbility { get; private set; }
        public ModHotKey FourthPKMAbility { get; private set; }

        #endregion


        public static TerramonMod Instance { get; private set; }
    }
}
