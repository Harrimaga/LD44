using CottonCandy.Cards;
using CottonCandy.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System;

namespace CottonCandy
{
    public class CottonCandy : Game
    {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        protected Matrix spriteScale;
        public int screenW = 1920, screenH = 1080;
        public int resolutionWidth = 1920 * 2, resolutionHeight = 1080 * 2;
        public double scalex, scaley;

        private List<Button> ButtonList;

        private MouseState previousState, newState;

        private readonly Hotkey camL = new Hotkey(false).add(Keys.Left).add(Keys.A);
        private readonly Hotkey camR = new Hotkey(false).add(Keys.Right).add(Keys.D);
        private readonly Hotkey camD = new Hotkey(false).add(Keys.Down).add(Keys.S);
        private readonly Hotkey camU = new Hotkey(false).add(Keys.Up).add(Keys.W);
        private readonly Hotkey discard = new Hotkey(true).add(Keys.X);

        public CottonCandy()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = resolutionWidth,
                PreferredBackBufferHeight = resolutionHeight
            };
            Content.RootDirectory = "Content";
            Globals.contentmanager = Content;

            scalex = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / screenW;
            scaley = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / screenH;
            Globals.screenW = screenW;
            Globals.screenH = screenH;
            graphics.HardwareModeSwitch = false;
            graphics.IsFullScreen = true;

        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            newState = Mouse.GetState();

            ButtonList = new List<Button>()
            {
                new LevelSelectButton(screenW / 2 - 132, 256, 265, 65, "UIElements/LevelsButton"),
                new DeckBuilderButton("Deck builder", screenW / 2 - 177, 500, 265, 65),
                new OutDeckBuilderButton("Back", screenW / 2 - 100, 5, 200, 65),
                new DiscardButton("Discard(x)", screenW-Globals.tileW*2, screenH - Globals.tileH*2 - 220, Globals.tileW*2, 65),
                new OpenShopButton("SHOP", screenW / 2 - 100, 300, 265, 65, GameStates.WON),
                new OpenShopButton("SHOP", screenW / 2 - 100, 300, 265, 65, GameStates.GAMEOVER),
                new OutShopButton("Back", 5, screenH - 65, 265, 65),
                new OutShopButton("Back", 5, screenH - 65, 265, 65, GameStates.GAMEOVER),
                new OutShopButton("Back", screenW / 2 - 100, 200, 265, 65, GameStates.WON),
                new OutShopButton("Back", screenW / 2 - 100, screenH - 105, 265, 65, GameStates.LEVELSELECT),
                new OutShopButton("Back", screenW / 2 - 100, screenH - 105, 265, 65, GameStates.TURORIAL),
                new TutorialButton("TUTORIAL", screenW / 2 - 160, 700, 265, 65)

            };
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.font = Content.Load<SpriteFont>("font");
            Globals.bigfont = Content.Load<SpriteFont>("BigFont");
            SoundManager.StartBackgroundMusic("DeckStackMusicTrack");
            //scaling shit
            Viewport viewport = new Viewport
            {
                Width = resolutionWidth,
                Height = resolutionHeight
            };
            GraphicsDevice.Viewport = viewport;
            spriteScale = Matrix.CreateScale(resolutionWidth / (float)screenW, resolutionHeight / (float)screenH, 1);

            Globals.sb = spriteBatch;

            LoadFileContent();
            if (Globals.gameState == GameStates.EDITING)
            {
                Globals.grid = new Tilegrid(20, 20);
                LevelEditor.Init();
            }
            else if (Globals.gameState == GameStates.PLAYING)
            {
                LevelEditor.LoadLevel("Level1");
            }

            Globals.CurrentPlayer.Shuffle();
            // TODO: use this.Content to load your game content here
        }

        private void LoadFileContent()
        {
            if (FileLoader.FileExists("Collection.bin"))
            {
                Globals.Collection = FileLoader.Read<List<Card>>("Collection.bin");
            }
            else
            {
                CreateBaseCollection();
                try
                {
                    FileLoader.Write(Globals.Collection, "Collection.bin");
                }
                catch (IOException e)
                {
                    FileLoader.WriteText("[ERROR " + DateTime.Now.ToLongTimeString() + " at CottonCandy.cs->LoadFileContent()] IOException:" + e.Message, "log.txt");
                }
            }

            if (FileLoader.FileExists("Deck.bin"))
            {
                Globals.LastDeck = FileLoader.Read<List<Card>>("Deck.bin");
            }
            else
            {
                //There was no last deck or standard deck found, so we make a new one here!
                CreateBaseDeck();
                try
                {
                    FileLoader.Write(Globals.LastDeck, "Deck.bin");
                }
                catch (IOException e)
                {
                    FileLoader.WriteText("[ERROR " + DateTime.Now.ToLongTimeString() + " at CottonCandy.cs->LoadFileContent()] IOException:" + e.Message, "log.txt");
                }

            }

            if (FileLoader.FileExists("Player.bin"))
            {
                Globals.CurrentPlayer = FileLoader.Read<Player>("Player.bin");
            }
            else
            {
                Globals.CurrentPlayer = new Player(35, Globals.LastDeck);
                try
                {
                    FileLoader.Write(Globals.CurrentPlayer, "Player.bin");
                }
                catch (IOException e)
                {
                    FileLoader.WriteText("[ERROR " + DateTime.Now.ToLongTimeString() + " at CottonCandy.cs->LoadFileContent()] IOException:" + e.Message, "log.txt");
                }
            }
        }

        protected override void UnloadContent()
        {

        }



        protected void EnemyTurn()
        {
            Globals.grid.ResetDamaged();
            for (int i = 0; i < Globals.grid.units.Count; i++)
            {
                Globals.grid.units[i].TakeTurn();
            }
            CombatLog.AddLine("End of turn");
            Globals.hasMoved = false;
        }

        protected override void Update(GameTime gameTime)
        {
            previousState = newState;
            newState = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            Globals.mouse = new Point((int)(newState.Position.X / scalex), (int)(newState.Position.Y / scaley));

            Globals.time = gameTime;
            if (camD.IsDown()) Camera.moveCamD();
            if (camU.IsDown()) Camera.moveCamU();
            if (camL.IsDown()) Camera.moveCamL();
            if (camR.IsDown()) Camera.moveCamR();

            if (Globals.timer > 0)
            {
                Globals.timer -= gameTime.ElapsedGameTime.Milliseconds;
                if (Globals.timer <= 0)
                {
                    Globals.timer = 0;
                    EnemyTurn();
                }
                return;
            }


            if (Globals.gameState == GameStates.EDITING)
            {
                LevelEditor.Update(previousState.LeftButton == ButtonState.Released && newState.LeftButton == ButtonState.Pressed, previousState.RightButton == ButtonState.Released && newState.RightButton == ButtonState.Pressed);
            }
            else if (Globals.gameState == GameStates.PLAYING)
            {

                if (discard.IsDown())
                {
                    Globals.CurrentPlayer.PutCardBack();
                    Globals.TakeTurn();
                }
                Level.Update();

                if (previousState.LeftButton == ButtonState.Released && newState.LeftButton == ButtonState.Pressed)
                {
                    // Selfcast Card
                    Rectangle r = new Rectangle(Globals.screenW - Globals.tileW * 2, Globals.screenH - Globals.tileH * 2, Globals.tileW * 2, Globals.tileH * 2);

                    if (r.Contains(Globals.mouse))
                    {
                        if (Globals.CurrentPlayer.PlayCard())
                        {
                            Globals.TakeTurn();
                        }
                    }
                }

                if (previousState.RightButton == ButtonState.Released && newState.RightButton == ButtonState.Pressed)
                {
                    // Cast on enemy
                    int x = (Globals.mouse.X + Camera.camX) / Globals.tileW;
                    int y = (Globals.mouse.Y + Camera.camY) / Globals.tileH;

                    if (Globals.CurrentPlayer.PlayCard(x, y))
                    {
                        Globals.TakeTurn();
                    }
                }

                if (previousState.LeftButton == ButtonState.Released && newState.LeftButton == ButtonState.Pressed)
                {
                    Vector2 mousePos = new Vector2(Globals.mouse.X + Camera.camX, Globals.mouse.Y + Camera.camY);
                    int x = (int)(mousePos.X / Globals.tileW);
                    int y = (int)(mousePos.Y / Globals.tileH);
                    int px = (int)(Globals.CurrentPlayer.x / Globals.tileW);
                    int py = (int)(Globals.CurrentPlayer.y / Globals.tileH);
                    if ((px - x) * (px - x) + (py - y) * (py - y) == 1)
                    {
                        Globals.grid.MoveUnit(px, py, x, y);
                    }
                }
            }
            else if (Globals.gameState == GameStates.DECKBUIDING)
            {
                CollectionUI.Update();
                if (previousState.LeftButton == ButtonState.Pressed && newState.LeftButton == ButtonState.Released)
                {
                    CollectionUI.OnLeftClick();
                }
                if (previousState.RightButton == ButtonState.Pressed && newState.RightButton == ButtonState.Released)
                {
                    CollectionUI.OnRightClick();
                }
            }
            else if (Globals.gameState == GameStates.SHOP)
            {
                if (previousState.LeftButton == ButtonState.Pressed && newState.LeftButton == ButtonState.Released)
                {
                    Globals.currentShop.OnClick();
                }
            }
            else if (Globals.gameState == GameStates.LEVELSELECT)
            {
                if (previousState.LeftButton == ButtonState.Pressed && newState.LeftButton == ButtonState.Released)
                {
                    LevelSelect.OnClick();
                }
            }

            if (previousState.LeftButton == ButtonState.Pressed && newState.LeftButton == ButtonState.Released)
            {
                foreach (var b in ButtonList)
                {
                    b.Update();
                    if (b.MouseOver())
                    {
                        b.Click();
                    }
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, spriteScale);
            GraphicsDevice.Clear(Color.CornflowerBlue);


            if (Globals.gameState == GameStates.EDITING)
            {
                Globals.grid.Draw();
                LevelEditor.Draw();
            }
            else if (Globals.gameState == GameStates.PLAYING)
            {
                Globals.grid.Draw();
                Globals.sb.Draw(LoadTex.LoadTexture("UIElements/LevelEditorBaseUI"), new Vector2(166, 0), Color.White);
                Level.Draw();
                Globals.CurrentPlayer.DrawCardUI();
                CombatLog.Draw();
                foreach (var b in ButtonList)
                {
                    b.Draw();
                }
            }
            else if (Globals.gameState == GameStates.MAINMENU)
            {
                Globals.sb.Draw(LoadTex.LoadTexture("UIElements/MenuBack"), new Rectangle(0, 0, screenW, screenH), Color.White);

                foreach (var b in ButtonList)
                {
                    b.Draw();
                }
            }
            else if (Globals.gameState == GameStates.DECKBUIDING)
            {
                Globals.sb.Draw(LoadTex.LoadTexture("UIElements/MenuBack"), new Rectangle(0, 0, screenW, screenH), Color.White);
                foreach (var b in ButtonList)
                {
                    b.Draw();
                }
                CollectionUI.Draw();
            }
            else if (Globals.gameState == GameStates.SHOP)
            {
                Globals.sb.Draw(LoadTex.LoadTexture("ShopBack"), new Rectangle(0, 0, screenW, screenH), Color.White);
                Globals.currentShop.Draw();
                foreach (var b in ButtonList)
                {
                    b.Draw();
                }
            }
            else if (Globals.gameState == GameStates.GAMEOVER)
            {
                Globals.sb.DrawString(Globals.bigfont, "Oh no, you died!", new Vector2(5, 5), Color.White);
                foreach (var b in ButtonList)
                {
                    b.Draw();
                }
                CombatLog.Draw();
            }
            else if (Globals.gameState == GameStates.WON)
            {
                Globals.sb.DrawString(Globals.bigfont, "Level cleared", new Vector2(5, 5), Color.White);
                foreach (var b in ButtonList)
                {
                    b.Draw();
                }
                CombatLog.Draw();
            }
            else if (Globals.gameState == GameStates.LEVELSELECT)
            {
                Globals.sb.Draw(LoadTex.LoadTexture("UIElements/MenuBack"), new Rectangle(0, 0, screenW, screenH), Color.White);
                foreach (var b in ButtonList)
                {
                    b.Draw();
                }
                LevelSelect.Draw();
            }
            else if(Globals.gameState == GameStates.TURORIAL)
            {
                Globals.sb.DrawString(Globals.bigfont, Text.BreakUpString(Globals.bigfont, "Each turn you can move one square and play a card or put the top card on the bottom. Your turn ends when you play a card or when you put a card at the bottom of the deck. You can play a card, if it is a target card, by right clicking on the map at the target location, or when it is a non targeted card by clicking on the card. The goal is to move onto the red square. After you win or lose, you can go to the shop to add cards to your collection.", Globals.screenW-10), new Vector2(5, 5), Color.White);
                foreach (var b in ButtonList)
                {
                    b.Draw();
                }

            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void ChangeScale(int x, int y)
        {
            screenW = x;
            screenH = y;
            Globals.screenW = screenW;
            Globals.screenH = screenH;
            spriteScale = Matrix.CreateScale(resolutionWidth / (float)screenW, resolutionHeight / (float)screenH, 1);
        }

        private void CreateBaseCollection()
        {
            Globals.Collection.Add(new Strike());
            Globals.Collection.Add(new Strike());
            Globals.Collection.Add(new Strike());
            Globals.Collection.Add(new Strike());
            Globals.Collection.Add(new Strike());
            Globals.Collection.Add(new Strike());
            Globals.Collection.Add(new Strike());
            Globals.Collection.Add(new Strike());
            Globals.Collection.Add(new Strike());
            Globals.Collection.Add(new Strike());
            Globals.Collection.Add(new Strike());
            Globals.Collection.Add(new Strike());
            Globals.Collection.Add(new Strike());
            Globals.Collection.Add(new Strike());
            Globals.Collection.Add(new Strike());
            Globals.Collection.Add(new Surge());
            Globals.Collection.Add(new Surge());
            Globals.Collection.Add(new Surge());
            Globals.Collection.Add(new FireWave());
            Globals.Collection.Add(new FireWave());
            Globals.Collection.Add(new FireWave());
            Globals.Collection.Add(new HealMinorWounds());
            Globals.Collection.Add(new HealMinorWounds());
            Globals.Collection.Add(new HealMinorWounds());
            Globals.Collection.Add(new Reflex());
            Globals.Collection.Add(new Reflex());
            Globals.Collection.Add(new Fireball());
            Globals.Collection.Add(new Assassinate());
        }

        private void CreateBaseDeck()
        {
            Globals.LastDeck.Add(new Strike());
            Globals.LastDeck.Add(new Strike());
            Globals.LastDeck.Add(new Strike());
            Globals.LastDeck.Add(new Strike());
            Globals.LastDeck.Add(new Strike());
            Globals.LastDeck.Add(new Strike());
            Globals.LastDeck.Add(new Strike());
            Globals.LastDeck.Add(new Strike());
            Globals.LastDeck.Add(new Strike());
            Globals.LastDeck.Add(new Strike());
            Globals.LastDeck.Add(new Surge());
            Globals.LastDeck.Add(new Surge());
            Globals.LastDeck.Add(new Surge());
            Globals.LastDeck.Add(new FireWave());
            Globals.LastDeck.Add(new FireWave());
            Globals.LastDeck.Add(new HealMinorWounds());
            Globals.LastDeck.Add(new Fireball());
            Globals.LastDeck.Add(new Assassinate());
        }

    }


}
