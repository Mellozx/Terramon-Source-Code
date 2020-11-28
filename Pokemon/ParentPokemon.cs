using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using On.Terraria.Achievements;
using Terramon.Players;
using Terramon.Pokemon.FirstGeneration.Normal.Bulbasaur;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
// ReSharper disable CompareOfFloatsByEqualityOperator
// ReSharper disable PossibleLossOfFraction

namespace Terramon.Pokemon
{
    public abstract class ParentPokemon : ModProjectile
    {
        public override string Texture => "Terramon/Pokemon/Empty";

        /// <summary>
        ///     Next stage pokemon to evolve.
        ///     If value == null => mon can't evolve
        /// </summary>
        public virtual Type EvolveTo { get; } = null;

        /// <summary>
        ///     How much candies need to evolve.
        /// </summary>
        public virtual int EvolveCost { get; } = 0;

        /// <summary>
        ///     Item what used for evolution
        /// </summary>
        public virtual EvolveItem EvolveItem => EvolveItem.RareCandy;

        /// <summary>
        ///     Just for checking if this mon can evolve or not
        /// </summary>
        public bool CanEvolve => EvolveTo != null && EvolveCost != 0;

        public virtual PokemonType[] PokemonTypes => new[] {PokemonType.Normal};

#if DEBUG
        public virtual string[] DefaultMove => new[] {nameof(ShootMove), nameof(HealMove), "", ""};
#else
        public virtual string[] DefaultMove => new[] {"", "", "", ""};
#endif
        public virtual int MaxHP => 45;
        public virtual int PhysicalDamage => 50;
        public virtual int PhysicalDefence => 50;
        public virtual int SpecialDamage => 50;
        public virtual int SpecialDefence => 50;
        public virtual int Speed => 45;

        public int TotalPoints => MaxHP + PhysicalDamage + PhysicalDefence + SpecialDamage + SpecialDefence + Speed;

        internal bool Wild;

        private string iconName;

        public int SpawnTime = 0;
        public virtual string IconName => iconName ?? (iconName = $"Terramon/Minisprites/Regular/mini{GetType().Name}");

        public int AttackDuration;

        public bool shiny = false;

        public int frame;
        public int frameCounter;


        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 2;
            Main.projPet[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Puppy);
            aiType = ProjectileID.Puppy;
            projectile.owner = Main.myPlayer;
            drawOffsetX = 100;
            if (Main.dedServ)
            {
                Wild = det_Wild;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            var arr = GetType().Namespace.Split('.');
            string path = String.Empty;
            for (int i = 1; i < arr.Length && i < 4; i++) // We skip "Terramon" at 0 pos
            {
                path += path.Length > 0 ? $"/{arr[i]}" : arr[i];
            }

            path += $"/{projectile.Name}/{projectile.Name}";
            if (shiny)
            {
                path += "_Shiny";
            }

            SpriteEffects effects =
                projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Texture2D pkmnTexture = mod.GetTexture(path);
            int frameHeight = pkmnTexture.Height / Main.projFrames[projectile.type];
            spriteBatch.Draw(pkmnTexture, projectile.position - Main.screenPosition + new Vector2(14, 0),
                new Rectangle(0, frameHeight * frame, pkmnTexture.Width, frameHeight), drawColor, projectile.rotation,
                new Vector2(pkmnTexture.Width / 2f, frameHeight / 2), projectile.scale, effects, 0f);
            return true;
        }

        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            player.zephyrfish = false; // Relic from aiType
            if (aiType != 0)
                mainAi = aiType;
            return true;
        }

        private int mainAi = ProjectileID.Puppy;

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            TerramonPlayer modPlayer = player.GetModPlayer<TerramonPlayer>();

            if (modPlayer.ActivePetShiny)
            {
                shiny = true;
            }
            else
            {
                shiny = false;
            }

            //Animations

            projectile.spriteDirection = projectile.velocity.X > 0
                ? -1
                : (projectile.velocity.X < 0 ? 1 : projectile.spriteDirection);

            if (projectile.velocity.X != 0 || projectile.velocity.Y > 1)
            {
                frameCounter++;
                if (frameCounter > 15)
                {
                    frame += 1;
                    frameCounter = 0;
                    if (frame >= Main.projFrames[projectile.type])
                    {
                        frame = 0;
                    }
                }
            }
            else
            {
                frame = 1;
                frameCounter = 0;
            }

            SpawnTime++;
            if (SpawnTime == 1 && player.active)
            {
                if (player.direction == -1) // direction right
                {
                    projectile.direction = -1;
                }
                else
                {
                    projectile.direction = 1;
                }

                if (!Main.dedServ)
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>()
                        .GetLegacySoundSlot(SoundType.Custom, "Sounds/Cries/cry" + projectile.Name).WithVolume(0.55f));

                for (int i = 0; i < 18; i++)
                {
                    Dust.NewDust(projectile.position, projectile.width, projectile.height,
                        mod.DustType("SmokeTransformDust"));
                }
            }

            if (Wild)
            {
                projectile.timeLeft = 5;
                aiType = 0;
                PuppyAI();
                //projectile.tileCollide = false;
                projectile.direction = projectile.position.X > player.position.X ? -1 : 1;
                return;
            }

            if (!player.active)
            {
                projectile.timeLeft = 0;
            }


            if (player.dead)
            {
                modPlayer.ResetEffects();
                modPlayer.ActivePetId = -1;
            }

            if (modPlayer.IsPetActive(GetType().Name))
            {
                projectile.timeLeft = 2;
            }
            else if ((modPlayer.Battle?.awaitSync ?? false) || modPlayer.Battle?.WildNPC == this)
            {
                projectile.timeLeft = 2;
                Wild = true;
            }

            if (modPlayer.ActiveMove != null)
            {
                if (modPlayer.ActiveMove.OverrideAI(this, modPlayer))
                    aiType = 0;
            }
            else if (modPlayer.Battle?.AIOverride(this) != null) //If used inside battle
            {
                var t = modPlayer.Battle?.AIOverride(this);
                t.TurnAnimation = true;
                if (t.OverrideAI(this, modPlayer))
                {
                    aiType = 0;
                }

                t.TurnAnimation = false;
            }
            else if (aiType == 0)
            {
                aiType = mainAi;
            }

            if (modPlayer.Attacking)
            {
                AttackDuration++;
                if (AttackDuration < 60)
                {
                    if (projectile.type == ModContent.ProjectileType<Bulbasaur>())
                    {
                        Projectile.NewProjectile(projectile.position.X + 23, projectile.position.Y + 8, 0f, 0f,
                            ModContent.ProjectileType<AngerOverlay>(), 0, 0, Main.myPlayer);
                    }

                    AttackDuration = 0;
                }
                else
                {
                    // dont make any more.
                }
            }
            else
            {

            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Player player = Main.player[projectile.owner];
            if (SpawnTime <= 6)
            {
                projectile.position = player.position;
            }

            return true;
        }


        public static bool det_Wild;

        private void PuppyAI() //334
        {
            if (!Main.player[projectile.owner].active)
            {
                projectile.active = false;
                return;
            }

            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            bool flag5 = false;
            int num;
            if (projectile.lavaWet)
            {
                projectile.ai[0] = 1f;
                projectile.ai[1] = 0f;
            }

            num = 60 + 30 * projectile.minionPos;
            if (Main.player[projectile.owner].dead)
            {
                Main.player[projectile.owner].puppy = false;
            }
            else if (Main.player[projectile.owner].puppy)
            {
                projectile.timeLeft = 2;
            }

            if (Main.player[projectile.owner].position.X + (float) (Main.player[projectile.owner].width / 2) <
                projectile.position.X + (float) (projectile.width / 2) - (float) num)
            {
                flag = true;
            }
            else if (Main.player[projectile.owner].position.X + (float) (Main.player[projectile.owner].width / 2) >
                     projectile.position.X + (float) (projectile.width / 2) + (float) num)
            {
                flag2 = true;
            }


            if (projectile.ai[1] == 0f && !Wild)
            {
                int num36 = 500;
                if (Main.player[projectile.owner].rocketDelay2 > 0)
                {
                    projectile.ai[0] = 1f;
                }

                Vector2 vector6 = new Vector2(projectile.position.X + (float) projectile.width * 0.5f,
                    projectile.position.Y + (float) projectile.height * 0.5f);
                float num37 = Main.player[projectile.owner].position.X +
                              (float) (Main.player[projectile.owner].width / 2) - vector6.X;
                float num38 = Main.player[projectile.owner].position.Y +
                              (float) (Main.player[projectile.owner].height / 2) - vector6.Y;
                float num39 = (float) Math.Sqrt((double) (num37 * num37 + num38 * num38));
                if (num39 > 2000f)
                {
                    projectile.position.X = Main.player[projectile.owner].position.X +
                                            (float) (Main.player[projectile.owner].width / 2) -
                                            (float) (projectile.width / 2);
                    projectile.position.Y = Main.player[projectile.owner].position.Y +
                                            (float) (Main.player[projectile.owner].height / 2) -
                                            (float) (projectile.height / 2);
                }
                else if (num39 > (float) num36 || (Math.Abs(num38) > 300f))
                {
                    if (num38 > 0f && projectile.velocity.Y < 0f)
                    {
                        projectile.velocity.Y = 0f;
                    }

                    if (num38 < 0f && projectile.velocity.Y > 0f)
                    {
                        projectile.velocity.Y = 0f;
                    }

                    projectile.ai[0] = 1f;
                }
            }

            if (projectile.ai[0] != 0f)
            {
                float num40 = 0.2f;
                int num41 = 200;
                projectile.tileCollide = false;
                Vector2 vector7 = new Vector2(projectile.position.X + (float) projectile.width * 0.5f,
                    projectile.position.Y + (float) projectile.height * 0.5f);
                float num42 = Main.player[projectile.owner].position.X +
                              (float) (Main.player[projectile.owner].width / 2) - vector7.X;
                float num48 = Main.player[projectile.owner].position.Y +
                              (float) (Main.player[projectile.owner].height / 2) - vector7.Y;
                float num49 = (float) Math.Sqrt((double) (num42 * num42 + num48 * num48));
                float num50 = 10f;
                float num51 = num49;
                if (num49 < (float) num41 && Main.player[projectile.owner].velocity.Y == 0f &&
                    projectile.position.Y + (float) projectile.height <= Main.player[projectile.owner].position.Y +
                    (float) Main.player[projectile.owner].height &&
                    !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
                {
                    projectile.ai[0] = 0f;
                    if (projectile.velocity.Y < -6f)
                    {
                        projectile.velocity.Y = -6f;
                    }
                }

                if (num49 < 60f)
                {
                    num42 = projectile.velocity.X;
                    num48 = projectile.velocity.Y;
                }
                else
                {
                    num49 = num50 / num49;
                    num42 *= num49;
                    num48 *= num49;
                }

                if (projectile.velocity.X < num42)
                {
                    projectile.velocity.X = projectile.velocity.X + num40;
                    if (projectile.velocity.X < 0f)
                    {
                        projectile.velocity.X = projectile.velocity.X + num40 * 1.5f;
                    }
                }

                if (projectile.velocity.X > num42)
                {
                    projectile.velocity.X = projectile.velocity.X - num40;
                    if (projectile.velocity.X > 0f)
                    {
                        projectile.velocity.X = projectile.velocity.X - num40 * 1.5f;
                    }
                }

                if (projectile.velocity.Y < num48)
                {
                    projectile.velocity.Y = projectile.velocity.Y + num40;
                    if (projectile.velocity.Y < 0f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y + num40 * 1.5f;
                    }
                }

                if (projectile.velocity.Y > num48)
                {
                    projectile.velocity.Y = projectile.velocity.Y - num40;
                    if (projectile.velocity.Y > 0f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - num40 * 1.5f;
                    }
                }

                if ((double) projectile.velocity.X > 0.5)
                {
                    projectile.spriteDirection = -1;
                }
                else if ((double) projectile.velocity.X < -0.5)
                {
                    projectile.spriteDirection = 1;
                }

                projectile.frameCounter++;
                if (projectile.frameCounter > 1)
                {
                    projectile.frame++;
                    projectile.frameCounter = 0;
                }

                if (projectile.frame < 7 || projectile.frame > 10)
                {
                    projectile.frame = 7;
                }

                projectile.rotation = projectile.velocity.X * 0.1f;
            }
            else
            {
                bool flag7 = false;
                Vector2 vector9 = Vector2.Zero;
                bool flag8 = false;
                if (projectile.ai[1] != 0f)
                {
                    flag = false;
                    flag2 = false;
                }

                projectile.rotation = 0f;
                projectile.tileCollide = true;

                float num103 = 0.08f;
                float num104 = 8f;
                if (flag)
                {
                    if ((double) projectile.velocity.X > -3.5)
                    {
                        projectile.velocity.X = projectile.velocity.X - num103;
                    }
                    else
                    {
                        projectile.velocity.X = projectile.velocity.X - num103 * 0.25f;
                    }
                }
                else if (flag2)
                {
                    if ((double) projectile.velocity.X < 3.5)
                    {
                        projectile.velocity.X = projectile.velocity.X + num103;
                    }
                    else
                    {
                        projectile.velocity.X = projectile.velocity.X + num103 * 0.25f;
                    }
                }
                else
                {
                    projectile.velocity.X = projectile.velocity.X * 0.9f;
                    if (projectile.velocity.X >= -num103 && projectile.velocity.X <= num103)
                    {
                        projectile.velocity.X = 0f;
                    }
                }

                if (flag || flag2)
                {
                    int num105 = (int) (projectile.position.X + (float) (projectile.width / 2)) / 16;
                    int j2 = (int) (projectile.position.Y + (float) (projectile.height / 2)) / 16;
                    if (flag)
                    {
                        num105--;
                    }

                    if (flag2)
                    {
                        num105++;
                    }

                    num105 += (int) projectile.velocity.X;
                    if (WorldGen.SolidTile(num105, j2))
                    {
                        flag4 = true;
                    }
                }

                if (Main.player[projectile.owner].position.Y + (float) Main.player[projectile.owner].height - 8f >
                    projectile.position.Y + (float) projectile.height)
                {
                    flag3 = true;
                }

                Collision.StepUp(ref projectile.position, ref projectile.velocity, projectile.width,
                    projectile.height, ref projectile.stepSpeed, ref projectile.gfxOffY, 1, false, 0);
                if (projectile.velocity.Y == 0f)
                {
                    if (!flag3 && (projectile.velocity.X < 0f || projectile.velocity.X > 0f))
                    {
                        int num106 = (int) (projectile.position.X + (float) (projectile.width / 2)) / 16;
                        int j3 = (int) (projectile.position.Y + (float) (projectile.height / 2)) / 16 + 1;
                        if (flag)
                        {
                            num106--;
                        }

                        if (flag2)
                        {
                            num106++;
                        }

                        WorldGen.SolidTile(num106, j3);
                    }

                    if (flag4)
                    {
                        int num107 = (int) (projectile.position.X + (float) (projectile.width / 2)) / 16;
                        int num108 = (int) (projectile.position.Y + (float) projectile.height) / 16 + 1;
                        if (WorldGen.SolidTile(num107, num108) || Main.tile[num107, num108].halfBrick() ||
                            Main.tile[num107, num108].slope() > 0 || ProjectileID.Puppy == 200)
                        {
                            {
                                try
                                {
                                    num107 = (int) (projectile.position.X + (float) (projectile.width / 2)) / 16;
                                    num108 = (int) (projectile.position.Y + (float) (projectile.height / 2)) / 16;
                                    if (flag)
                                    {
                                        num107--;
                                    }

                                    if (flag2)
                                    {
                                        num107++;
                                    }

                                    num107 += (int) projectile.velocity.X;
                                    if (!WorldGen.SolidTile(num107, num108 - 1) &&
                                        !WorldGen.SolidTile(num107, num108 - 2))
                                    {
                                        projectile.velocity.Y = -5.1f;
                                    }
                                    else if (!WorldGen.SolidTile(num107, num108 - 2))
                                    {
                                        projectile.velocity.Y = -7.1f;
                                    }
                                    else if (WorldGen.SolidTile(num107, num108 - 5))
                                    {
                                        projectile.velocity.Y = -11.1f;
                                    }
                                    else if (WorldGen.SolidTile(num107, num108 - 4))
                                    {
                                        projectile.velocity.Y = -10.1f;
                                    }
                                    else
                                    {
                                        projectile.velocity.Y = -9.1f;
                                    }
                                }
                                catch
                                {
                                    projectile.velocity.Y = -9.1f;
                                }
                            }
                        }
                    }
                }

                if (projectile.velocity.X > num104)
                {
                    projectile.velocity.X = num104;
                }

                if (projectile.velocity.X < -num104)
                {
                    projectile.velocity.X = -num104;
                }

                if (projectile.velocity.X < 0f)
                {
                    projectile.direction = -1;
                }

                if (projectile.velocity.X > 0f)
                {
                    projectile.direction = 1;
                }

                if (projectile.velocity.X > num103 && flag2)
                {
                    projectile.direction = 1;
                }

                if (projectile.velocity.X < -num103 && flag)
                {
                    projectile.direction = -1;
                }

                if (projectile.direction == -1)
                {
                    projectile.spriteDirection = 1;
                }

                if (projectile.direction == 1)
                {
                    projectile.spriteDirection = -1;
                }

                if (flag5)
                {
                    if (projectile.ai[1] > 0f)
                    {
                        if (projectile.localAI[1] == 0f)
                        {
                            projectile.localAI[1] = 1f;
                            projectile.frame = 1;
                        }

                        if (projectile.frame != 0)
                        {
                            projectile.frameCounter++;
                            if (projectile.frameCounter > 4)
                            {
                                projectile.frame++;
                                projectile.frameCounter = 0;
                            }

                            if (projectile.frame == 4)
                            {
                                projectile.frame = 0;
                            }
                        }
                    }
                    else if (projectile.velocity.Y == 0f)
                    {
                        projectile.localAI[1] = 0f;
                        if (projectile.velocity.X == 0f)
                        {
                            projectile.frame = 0;
                            projectile.frameCounter = 0;
                        }
                        else if ((double) projectile.velocity.X < -0.8 || (double) projectile.velocity.X > 0.8)
                        {
                            projectile.frameCounter += (int) Math.Abs(projectile.velocity.X);
                            projectile.frameCounter++;
                            if (projectile.frameCounter > 6)
                            {
                                projectile.frame++;
                                projectile.frameCounter = 0;
                            }

                            if (projectile.frame < 5)
                            {
                                projectile.frame = 5;
                            }

                            if (projectile.frame >= 11)
                            {
                                projectile.frame = 5;
                            }
                        }
                        else
                        {
                            projectile.frame = 0;
                            projectile.frameCounter = 0;
                        }
                    }
                    else if (projectile.velocity.Y < 0f)
                    {
                        projectile.frameCounter = 0;
                        projectile.frame = 4;
                    }
                    else if (projectile.velocity.Y > 0f)
                    {
                        projectile.frameCounter = 0;
                        projectile.frame = 4;
                    }

                    projectile.velocity.Y = projectile.velocity.Y + 0.4f;
                    if (projectile.velocity.Y > 10f)
                    {
                        projectile.velocity.Y = 10f;
                    }

                    float arg_5B67_0 = projectile.velocity.Y;
                    return;
                }

                if (projectile.velocity.Y == 0f)
                {
                    if (projectile.velocity.X == 0f)
                    {
                        if (projectile.frame > 0)
                        {
                            projectile.frameCounter += 2;
                            if (projectile.frameCounter > 6)
                            {
                                projectile.frame++;
                                projectile.frameCounter = 0;
                            }

                            if (projectile.frame >= 7)
                            {
                                projectile.frame = 0;
                            }
                        }
                        else
                        {
                            projectile.frame = 0;
                            projectile.frameCounter = 0;
                        }
                    }
                    else if ((double) projectile.velocity.X < -0.8 || (double) projectile.velocity.X > 0.8)
                    {
                        projectile.frameCounter += (int) Math.Abs((double) projectile.velocity.X * 0.75);
                        projectile.frameCounter++;
                        if (projectile.frameCounter > 6)
                        {
                            projectile.frame++;
                            projectile.frameCounter = 0;
                        }

                        if (projectile.frame >= 7 || projectile.frame < 1)
                        {
                            projectile.frame = 1;
                        }
                    }
                    else if (projectile.frame > 0)
                    {
                        projectile.frameCounter += 2;
                        if (projectile.frameCounter > 6)
                        {
                            projectile.frame++;
                            projectile.frameCounter = 0;
                        }

                        if (projectile.frame >= 7)
                        {
                            projectile.frame = 0;
                        }
                    }
                    else
                    {
                        projectile.frame = 0;
                        projectile.frameCounter = 0;
                    }
                }
                else if (projectile.velocity.Y < 0f)
                {
                    projectile.frameCounter = 0;
                    projectile.frame = 2;
                }
                else if (projectile.velocity.Y > 0f)
                {
                    projectile.frameCounter = 0;
                    projectile.frame = 4;
                }

                projectile.velocity.Y = projectile.velocity.Y + 0.4f;
                if (projectile.velocity.Y > 10f)
                {
                    projectile.velocity.Y = 10f;
                    return;
                }
            }


        }
    }


    public enum EvolveItem
    {
        RareCandy,
        LinkCable,
        FireStone,
        ThunderStone,
        WaterStone,
        LeafStone,
        MoonStone,
        Eeveelution
    }
}