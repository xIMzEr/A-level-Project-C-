using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Threading;
using Game1.Displays;
using Game1.Core;

namespace Game1
{
    /// <summary>
    /// This is the main type for the game.
    /// </summary>
    public class Game1 : Game
    {
        public static Game1 GAME;
        public bool Loading = false;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Display CurrentDisplay; 
        public GameDisplay GameDisplay;
        public MainMenuDisplay MainMenuDisplay;
        public LoadingScreenDisplay LoadingScreenDisplay;
        public CharacterSelectionDisplay CharacterSelectionDisplay;
        public Tutorial Tutorial;
        public Leaderboards Leaderboard;

       

        public Game1()
        {
            Game1.GAME = this;
            graphics = new GraphicsDeviceManager(this);
            //graphics.ToggleFullScreen();
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
            GameDisplay = new GameDisplay();
            MainMenuDisplay = new MainMenuDisplay();
            LoadingScreenDisplay = new LoadingScreenDisplay();
            CharacterSelectionDisplay = new CharacterSelectionDisplay();
            Tutorial = new Tutorial();
            Leaderboard = new Leaderboards();


            this.Window.Title = "Volniir";
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

            GameDisplay.LoadContent(Content);
            MainMenuDisplay.LoadContent(Content);
            LoadingScreenDisplay.LoadContent(Content);
            CharacterSelectionDisplay.LoadContent(Content);
            Tutorial.LoadContent(Content);
            Leaderboard.LoadContent(Content);
            
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
            // Sets default display
            if (CurrentDisplay == null)
            {
                CurrentDisplay = MainMenuDisplay;
            }
     
            CurrentDisplay.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        protected override void Draw(GameTime gameTime)
        {
            this.IsMouseVisible = true;
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (CurrentDisplay != null)
            {
                CurrentDisplay.Draw(gameTime, spriteBatch);
            }

            base.Draw(gameTime);
        }
    }
}
