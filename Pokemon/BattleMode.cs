using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Razorwing.Framework.Localisation;
using Terramon.Players;
using Terramon.Pokemon.Moves;
using Terramon.UI;
using Terramon.UI.Moveset;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;

namespace Terramon.Pokemon
{
    /// <summary>
    /// Class what handles turn based battles between players and bots
    /// </summary>
    public class BattleMode
    {
        internal static BattleUI UI;// We will have a singleton battle UI. Move it to TerramonMod later...
        public BattleState State;
        public TerramonPlayer player1, player2;
        public PokemonData Wild;
        public ParentPokemon WildNPC;
        public int wildID;
        protected BaseMove pMove, oMove;
        protected int atackTimeout;

        protected ILocalisedBindableString playerChallenge =
            TerramonMod.Localisation.GetLocalisedString(new LocalisedString(("playerChallenge", "{0} is challenging you!")));
        protected ILocalisedBindableString wildChallenge =
            TerramonMod.Localisation.GetLocalisedString(new LocalisedString(("wildChallenge", "Wild {0} was appeared!")));

        protected ILocalisedBindableString pokeName1, pokeName2;


        public BattleMode(TerramonPlayer fpl, BattleState state, PokemonData second = null, ParentPokemonNPC npc = null, TerramonPlayer spl = null)
        {
#if !DEBUG
            State = BattleState.None;
#endif

            if (Main.netMode != NetmodeID.Server)
            {
                if (UI == null)
                    UI = new BattleUI();
                else
                {
                    UI.ResetData();
                }

                BattleUI.Visible = true;
            }

            State = state;
            player1 = fpl;
            player2 = spl;
            Wild = second;
            if (npc != null)
            {
                //Wild = new PokemonData()
                //{
                //    Pokemon = npc.HomeClass().Name,
                //};
                wildID = Projectile.NewProjectile(npc.npc.position, Vector2.Zero,
                    TerramonMod.Instance.ProjectileType(npc.HomeClass().Name), 0, 0, fpl.whoAmI);
                npc.npc.active = false;
                WildNPC = (ParentPokemon)Main.projectile[wildID].modProjectile;
                WildNPC.Wild = true;
               
            }
            pokeName1 = TerramonMod.Localisation.GetLocalisedString(fpl.ActivePetName);

            switch (State)
            {
                case BattleState.BattleWithPlayer:
                    pokeName2 = TerramonMod.Localisation.GetLocalisedString(spl?.ActivePetName);
                    UI.HP1.PokeData = fpl.ActivePet;
                    UI.HP2.PokeData = spl?.ActivePet;
                    UI.MovesPanel.PokeData = fpl.ActivePet;
                    playerChallenge.Args = new object[] {spl?.player.name};
                    Main.NewText(playerChallenge.Value);
                    break;
                case BattleState.BattleWithWild:
                    pokeName2 = TerramonMod.Localisation.GetLocalisedString(second?.Pokemon);
                    UI.HP1.PokeData = fpl.ActivePet;
                    UI.HP2.PokeData = Wild;
                    UI.MovesPanel.PokeData = fpl.ActivePet;
                    wildChallenge.Args = new object[] { second?.Pokemon };
                    Main.NewText(playerChallenge.Value);
                    break;
            }

            UI.MovePresed = (move) => { pMove = move; };
        }

        private int escapeCountdown = 0;
        private int animWindow = 0;
        private byte animMode = 0;//0 Idle, 1 playerMonAnim, 2 enemyMonAnim, 3 enemyMonContinue, 4 playerMonContinue

        public void Update()
        {
            if (Main.keyState.IsKeyDown(Keys.Escape))
            {
                if (escapeCountdown == 0)
                    escapeCountdown = 5*60;
                if (escapeCountdown == 3*60)
                    Main.NewText("Trying to escape...",200,50,70);
                escapeCountdown -= 1;

                if (escapeCountdown == 0)
                {
                    Main.NewText("Escaped!", 200, 50, 70);
                    State = BattleState.None;
                }
            }
            if(State == BattleState.None)
            {
                BattleUI.Visible = false;
                return;
            }

            if (State == BattleState.BattleWithWild && !WildNPC.projectile.active)
            {
                Main.NewText($"Pokemon disappear!");
                State = BattleState.None;
            }

            atackTimeout = atackTimeout > 0 ? atackTimeout - 1 : 0;
            animWindow = animWindow > 0 ? animWindow - 1 : 0;

            if (pMove == null && atackTimeout <= 0)
            {
                UI.Turn = true;
            }

            if (State != BattleState.BattleWithPlayer && oMove == null) // If this is single player
            {
                //Need an advanced AI for trainers
                oMove = new ShootMove();
            }

            if (animWindow > 0)
            {
                if (player1.ActivePet.Fainted ||
                    ((State == BattleState.BattleWithTrainer || State == BattleState.BattleWithWild) && Wild.Fainted) ||
                    (player2?.ActivePet?.Fainted ?? false))
                {
                    animMode = 5;
                    animWindow = 0;
                    pMove = null;
                    oMove = null;
                }
                else
                if (animMode == 1 || animMode == 4)
                {
                    pMove?.AnimateTurn((ParentPokemon)Main.projectile[player1.ActivePetId].modProjectile, WildNPC, player1, player1.ActivePet, Wild);
                }
                else if(animMode == 2 || animMode == 3)
                {
                    oMove?.AnimateTurn(WildNPC, (ParentPokemon)(Main.projectile[player1.ActivePetId].modProjectile), null, Wild,
                        player1.ActivePet);
                }
            }else if (animMode != 0 && animMode < 3)
            {
                animMode += 2;//Shift to second casts
                animWindow = 120;
                switch (animMode)
                {
                    case 3:
                        switch (State)
                        {
                            case BattleState.BattleWithWild:
                            case BattleState.BattleWithTrainer:
                                if(!Wild.Fainted)
                                    oMove?.PerformInBattle(WildNPC, (ParentPokemon)(Main.projectile[player1.ActivePetId].modProjectile), null, Wild,
                                        player1.ActivePet);
                                break;
                            case BattleState.BattleWithPlayer:
                                if(!player2.ActivePet.Fainted)
                                    oMove?.PerformInBattle((ParentPokemon)Main.projectile[player2.ActivePetId].modProjectile
                                        , (ParentPokemon)Main.projectile[player2.ActivePetId].modProjectile, player2,
                                        player2.ActivePet, player1.ActivePet);
                                break;
                        }
                        
                        break;
                    case 4:
                        if(!player1.ActivePet.Fainted)
                            pMove?.PerformInBattle((ParentPokemon)Main.projectile[player1.ActivePetId].modProjectile
                                , WildNPC, player1,
                                player1.ActivePet, Wild);
                        break;
                }
            }
            else
            if (pMove != null && oMove != null)
            {
                //Calculate speed here
                bool useCheck = ((pMove.Speed /100f) * player1.ActivePet.Speed) < ((oMove.Speed / 100f) * Wild.Speed);

                if (!useCheck)
                {
                    pMove?.PerformInBattle((ParentPokemon)Main.projectile[player1.ActivePetId].modProjectile, WildNPC,
                        player1, player1.ActivePet, Wild);
                    animMode = 1;
                }
                else
                {
                    oMove?.PerformInBattle(WildNPC, (ParentPokemon)(Main.projectile[player1.ActivePetId].modProjectile),
                        null, Wild, player1.ActivePet);
                    animMode = 2;
                }
                animWindow = 120;

                //if (!useCheck)
                //{
                //    pMove?.PerformInBattle((ParentPokemon)Main.projectile[player1.ActivePetId].modProjectile
                //        , WildNPC, player1,
                //        player1.ActivePet, Wild);

                //    pMove = null;


                //    if (!Wild.Fainted)
                //    {
                //        oMove?.PerformInBattle(WildNPC, (ParentPokemon)(Main.projectile[player1.ActivePetId].modProjectile), null, Wild,
                //            player1.ActivePet);
                //        oMove = null;
                //    }
                //}
                //else
                //{
                //    oMove?.PerformInBattle(WildNPC, (ParentPokemon)(Main.projectile[player1.ActivePetId].modProjectile), null, Wild,
                //        player1.ActivePet);
                //    oMove = null;

                //    if (!player1.ActivePet.Fainted)
                //    {
                //        pMove?.PerformInBattle((ParentPokemon)Main.projectile[player1.ActivePetId].modProjectile
                //            , WildNPC, player1,
                //            player1.ActivePet, Wild);

                //        pMove = null;
                //    }
                //}

                atackTimeout = 260;
            }

            if (player1.ActivePet.Fainted)
            {
                if ((player1.PartySlot1 != null && !player1.PartySlot1.Fainted) || (player1.PartySlot2 != null && !player1.PartySlot2.Fainted) ||
                    (player1.PartySlot3 != null && !player1.PartySlot3.Fainted) || (player1.PartySlot4 != null && !player1.PartySlot4.Fainted) ||
                    (player1.PartySlot5 != null && !player1.PartySlot5.Fainted) || (player1.PartySlot6 != null && !player1.PartySlot6.Fainted))
                    Main.NewText($"Your {player1.ActivePet.PokemonName} was fainted! Please, send out another pokemon!"); // TODO: Change this placeholders to LocalisedString
                else
                {
                    Main.NewText($"Your {player1.ActivePet.PokemonName} was fainted! You loose this battle!");
                    State = BattleState.None;
                }
            }

            switch (State)
            {
                case BattleState.BattleWithWild:
                    if (Wild.Fainted)
                    {
                        Main.NewText($"Wild {Wild.PokemonName} was fainted! [PH] Your {player1.ActivePet?.PokemonName} received 50 XP!");
                        player1.ActivePet.Exp += 50;
                        State = BattleState.None;
                    }

                    break;
                case BattleState.BattleWithPlayer:
                    Main.NewText("PvP not supported rn");
                    State = BattleState.None;
                    break;
            }

        }

        public BaseMove AIOverride(ParentPokemon mon)
        {
            switch (animMode)
            {
                case 1 when pMove != null && animWindow > 0 && (ParentPokemon)(Main.projectile[player1.ActivePetId].modProjectile) == mon:
                    return pMove;
                case 2 when oMove != null && animWindow > 0:
                    switch (State)
                    {
                        case BattleState.BattleWithTrainer:
                        case BattleState.BattleWithWild:
                            if (WildNPC == mon)
                                return oMove;
                            break;
                        case BattleState.BattleWithPlayer:
                            return player2?.ActiveMove;
                        default:
                            return null;
                    
                    }
                    break;
            }
            return null;
        }

        public void Cleanup()
        {
            if (Main.netMode != NetmodeID.Server)
                BattleUI.Visible = false;

            if (WildNPC != null)
            {
                WildNPC.Wild = false;
                WildNPC.projectile.timeLeft = 0;
            }

        }


    }

    public class BattleUI :UIState
    {
        public MovesPanel MovesPanel;
        public HPPanel HP1, HP2;
        public bool Turn;
        public Action<BaseMove> MovePresed;
        public static bool Visible = false; 

        public override void OnInitialize()
        {
            MovesPanel = new MovesPanel()
            {
                OnMoveClick = (x) =>
                {
                    if (Turn)
                    {
                        Turn = false;
                        MovePresed?.Invoke(x);
                    }
                }
            };
            Append(MovesPanel);

            HP1 = new HPPanel();
            HP1.Left.Set(100, 0);
            HP1.Top.Set(0, 0.7f);
            Append(HP1);

            HP2 = new HPPanel();
            HP2.Left.Set(-200, 1);
            HP2.Top.Set(0, 0.7f);
            Append(HP2);

            base.OnInitialize();
        }

        public void ResetData()
        {
            HP1.PokeData = null;
            HP2.PokeData = null;
            MovesPanel.PokeData = null;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(!Visible)
                return;
            base.Draw(spriteBatch);
        }
    }

    public class HPPanel : UIPanel
    {
        private UIText HPText, PokeName;
        private UIImage HP, HPBack, Party, PartyExh;

        private ILocalisedBindableString LocPokemon;

        private PokemonData pokeData;

        //private Texture HPTexture, HPBackTexture, PartyTexture, PartyExhausted;
        public PokemonData PokeData
        {
            get => pokeData;
            set
            {
                if(value == pokeData)
                    return;
                pokeData = value;
                LocPokemon = TerramonMod.Localisation.GetLocalisedString(pokeData?.Pokemon ?? "MissingNO");
                PokeName?.SetText(LocPokemon.Value);
            }
        }
        //public int PartySize;

        public Vector2 Position
        {
            get => new Vector2(Left.Percent, Top.Percent);
            set
            {
                Left.Set(0, value.X);
                Top.Set(-55, value.Y);
            }
        }


        public override void OnInitialize()
        {
            this.BackgroundColor = Color.Azure * 0.7f;
            this.Width.Set(100, 0f);
            this.Height.Set(75, 0f);

            PokeName = new UIText(LocPokemon?.Value ?? string.Empty);
            PokeName.Left.Set(5, 0f);
            PokeName.Top.Set(5, 0f);
            Append(PokeName);

            HPText = new UIText("HP: 0/0");
            HPText.Left.Set(5, 0f);
            HPText.Top.Set(25, 0f);
            Append(HPText);

            base.OnInitialize();
        }

        public override void Update(GameTime gameTime)
        {
            HPText?.SetText($"HP: {pokeData?.HP ?? 0}/{pokeData?.MaxHP ?? 0}");

            //TODO: Make an HP bar

            base.Update(gameTime);
        }

    }

    public class MovesPanel : UIPanel
    {
        private BattleMoveButton Move1, Move2, Move3, Move4;

        public PokemonData PokeData
        {
            get => pokeData;
            set
            {
                if(pokeData == value)
                    return;

                needUpdate = true;
                pokeData = value;
            }
        }
        private bool needUpdate;
        public Action<BaseMove> OnMoveClick;
        private PokemonData pokeData;

        public MovesPanel()
        {
        }


        public override void OnInitialize()
        {
            this.Width.Set(400, 0f);
            this.Height.Set(100, 0f);
            this.Top.Set(-400, 1f);
            this.Left.Set(-200, 0.5f);
            this.BackgroundColor = Color.Brown * 0.6f; 
            var size = new Vector2(170, 40);

            Move1 = new BattleMoveButton(PokeData?.Moves[0], new Vector2(10, 10), size, true)
            {
                OnClick = (x) => OnMoveClick?.Invoke(x),
            };
            Append(Move1);
            Move3 = new BattleMoveButton(PokeData?.Moves[2], new Vector2(10, 60), size, true)
            {
                OnClick = (x) => OnMoveClick?.Invoke(x),
            };
            Append(Move3);
            Move2 = new BattleMoveButton(PokeData?.Moves[1], new Vector2(220, 10), size, false)
            {
                OnClick = (x) => OnMoveClick?.Invoke(x),
            };
            Append(Move2);
            Move4 = new BattleMoveButton(PokeData?.Moves[3], new Vector2(220, 60), size, false)
            {
                OnClick = (x) => OnMoveClick?.Invoke(x),
            };
            Append(Move4);

            base.OnInitialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (needUpdate)
            {
                Move1.Move = pokeData.Moves[0];
                Move2.Move = pokeData.Moves[1];
                Move3.Move = pokeData.Moves[2];
                Move4.Move = pokeData.Moves[3];
            }


            base.Update(gameTime);
        }
    }

    public enum BattleState
    {
        None = 0,
        BattleWithWild,
        BattleWithTrainer,
        BattleWithPlayer,//Should use networking
    }
}
