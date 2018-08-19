using DungeonCrawler.Components;
using DungeonCrawler.Entities;
using DungeonCrawler.UI;
using DungeonCrawler.World;
using DungeonCrawler.World.TerrainGeneration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace DungeonCrawler
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Map CurrentMap;
        Player player = new Player();
        Camera _camera = new Camera();

        MovementManager _movementManager = new MovementManager();
        TurnManager _turnManager = TurnManager.Instance;
        NPCManager _npcManager = NPCManager.Instance;
        PlayerUI ui = new PlayerUI();

        public static int ScreenWidth = 800;
        public static int ScreenHeight = 600;

        public enum GameState {MenuOpen, Playing, MainScreen }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            graphics.ApplyChanges();
            IsMouseVisible = true;

            _npcManager.EnemyDied += HandleEnemyDeath;
            ui.ConnectToEvents(player);
        }

        private void HandleEnemyDeath(object sender, EventArgs e)
        {
            _npcManager.HandleEnemyDeath(CurrentMap, player, (BaseNPC)sender);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            CurrentMap = new DungeonGenerator().Generate();
            Tileset.SetTexture(Content, "RPGTileset(48)");

            _npcManager.SpawnEnemies(Content, CurrentMap);

            CurrentMap.LoadContent(Content);
            player.LoadContent();
            player.SetPosition(CurrentMap.mainRoom.GetCenter());
            CurrentMap[CurrentMap.mainRoom.GetCenter()].Occupant = player;
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (_turnManager.Update(player, _npcManager.Enemies))
            {
                _movementManager.Update(CurrentMap, player, _npcManager.Enemies);
            }
            _camera.Follow(player);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(transformMatrix: _camera.Transform);

            CurrentMap.Draw(spriteBatch, new Vector2(player.x, player.y), player.VisionRadius);
            //player.Draw(spriteBatch);
            //_npcManager.DrawEnemies(spriteBatch); each cell decides if it should draw its occupant

            ui.Draw(spriteBatch, Tileset.Width*player.x - ScreenWidth/2, Tileset.Height*player.y - ScreenHeight/2);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
