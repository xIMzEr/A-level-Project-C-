using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using Game1.Static_World;
using System.IO;
namespace Game1.Displays
{

    public delegate void Wave();
    public delegate void WaveEnd();
    public class GameDisplay : Display
    {
        //Constants for boundaries
        public const int WIDTH = 400;
        public const int HEIGHT = 200;

        // Background
        public Texture2D BG;
        public Texture2D RockT;
        public Texture2D EnemyT;
        public Texture2D TransitionT;
        public Transition transition;
        public Texture2D ItemT;
        public Texture2D Item2T;
        public Texture2D Item3T;

        //Soundtrack of the game
        Song Mayhem; Song Marduk; Song Burzum; Song DarkThrone; Song OM;
        SoundTrack soundTrack;

        // Represents the player
        public Player player;
        public int TearsCount = 2;
        public float playerspeed;
        public bool player1 = false;
        public bool player2 = false;
        public bool wonGame = false;
        public int totalWavesLasted = 0;

        //Represents the rock and creates a list of rocks to be stored in
        public List<Rock> rocks = new List<Rock>();
        //Represents the tears and creates a lit of tears to be stored in
        public List<Entity> entities = new List<Entity>();
        //Represents the item spawned
        public List<Item> item = new List<Item>();

        // Keyboard states used to determine key presses
        public KeyboardState currentKeyboardState;
        public KeyboardState previousKeyboardState;

        //BinaryWriter
        public BinaryWriter BnWr;

        //Waves
        private Dictionary<int, Tuple<Wave, WaveEnd>> Waves = new Dictionary<int, Tuple<Wave, WaveEnd>>();
        public int CurrentWave = 0;

        private bool WaveOver = true;
        private TimeSpan WaveEndTime = TimeSpan.FromSeconds(0);
        private TimeSpan NextWaveWaitTime = TimeSpan.FromSeconds(7);

        public GameDisplay()
        {
            // Initialize the player class
            player = new Player();

            // Create wave 1
            CreateWave(1, () =>
            {
                //Adds 1 item into the item list
                Item iT = new Item(Game1.GAME.GraphicsDevice.Viewport.Width / 2, Game1.GAME.GraphicsDevice.Viewport.Height / 2);
                iT.Initialize(ItemT);
                item.Add(iT);
                //Adds 5 enemies into the enemy list
                for (int i = 0; i < 5; i++)
                {
                    //Sets the X&Y-Co-ordinate within the game viewport at a proportional gradient 
                    Enemy e = new Enemy(Game1.GAME.GraphicsDevice.Viewport.Width - (i + 1) * 125, Game1.GAME.GraphicsDevice.Viewport.Height - 60, EnemyT);
                    entities.Add(e);
                }

                //Adds 5 rocks into the rocks list
                for (int i = 0; i < 5; i++)
                {
                    //Sets the X&Y-Co-ordinate within the game viewport at a proportional gradient 
                    Rock r = new Rock(Game1.GAME.GraphicsDevice.Viewport.Width - (i + 1) * 125, Game1.GAME.GraphicsDevice.Viewport.Height - i * 100, RockT);
                    Rock r2 = new Rock(Game1.GAME.GraphicsDevice.Viewport.Width - (i + 1) * 125, Game1.GAME.GraphicsDevice.Viewport.Height - (i * -100 + 500), RockT);
                    rocks.Add(r);
                    rocks.Add(r2);
                }
            }, () => {
                            BG = Game1.GAME.Content.Load<Texture2D>("Assets\\Room2");
            });

            CreateWave(2, () =>
            {
                //Stpres the players current speed
                playerspeed = player.playerMoveSpeed;
                //When the next wave is initiated if the item exists the item is removed
                for (int i = 0; i < item.Count; i++)
                {
                    foreach (Item iTE in item)
                    {
                        iTE.isPicked = true;
                       
                    }
                }
               
                Item iT = new Item(Game1.GAME.GraphicsDevice.Viewport.Width / 2, Game1.GAME.GraphicsDevice.Viewport.Height / 2);
                iT.Initialize(Item2T);
                item.Add(iT);


                //Adds 5 enemies into the enemy list
                for (int i = 0; i < 5; i++)
                {
                    //Sets the X&Y-Co-ordinate within the game viewport at a proportional gradient 
                    Enemy e = new Enemy(Game1.GAME.GraphicsDevice.Viewport.Width - (i + 1) * 125, Game1.GAME.GraphicsDevice.Viewport.Height - i * 60, EnemyT);
                    entities.Add(e);
                }

                
                //Kills all rocks from previous wave
                for (int i = 0; i < rocks.Count; i++)
                {
                    foreach (Rock r in rocks)
                    {
                        r.isDead = true;
                    }
                }

                for (int i = 0; i < 5; i++)
                {
                    //Sets the X&Y-Co-ordinate within the game viewport at a proportional gradient 
                    Rock r = new Rock(Game1.GAME.GraphicsDevice.Viewport.Width - (i + 1) * 125, Game1.GAME.GraphicsDevice.Viewport.Height - i * 100, RockT);
                    rocks.Add(r);
                }

                if (player1)
                { player.PlayerTexture = Game1.GAME.Content.Load<Texture2D>("Assets\\p2SS"); }
                player.Health += 20;
                player.hasItem = false;
            }, () => {               
           BG = Game1.GAME.Content.Load<Texture2D>("Assets\\Room3"); });

            CreateWave(3, () =>
            {
                //Resets the player move speed if an item has been picked up
                player.playerMoveSpeed = playerspeed;

                //Adds 7 enemies into the enemy list
                for (int i = 0; i < 7; i++)
                {
                    //Sets the X&Y-Co-ordinate within the game viewport at a proportional gradient 
                    Enemy e = new Enemy(Game1.GAME.GraphicsDevice.Viewport.Width - (i) * 125, Game1.GAME.GraphicsDevice.Viewport.Height - i * 20, EnemyT);
                    entities.Add(e);
                }

                for (int i = 0; i < rocks.Count; i++)
                {
                    foreach (Rock r in rocks)
                    {
                        r.isDead = true;
                    }
                }

                for (int i = 0; i < 5; i++)
                {
                    //Sets the X&Y-Co-ordinate within the game viewport at a proportional gradient 
                    Rock r = new Rock(Game1.GAME.GraphicsDevice.Viewport.Width - (i + 1) * 125, Game1.GAME.GraphicsDevice.Viewport.Height - (i * -100 + 500), RockT);
                    rocks.Add(r);
                }
                foreach (Enemy e in entities)
                {
                    e.Health = 110;
                    e.rateOfFire = 5;
                    e.eVelocity *= 1.5f;
                }
                player.Health += 20;
                if (player1)
                {
                    player.PlayerTexture = Game1.GAME.Content.Load<Texture2D>("Assets\\p2SS");
                }
                player.hasItem = false;
            }, () => {
                BG = Game1.GAME.Content.Load<Texture2D>("Assets\\Room4");
            });

            CreateWave(4, () =>
            {

                Item iT = new Item((Game1.GAME.GraphicsDevice.Viewport.Width / 2) - 150, (Game1.GAME.GraphicsDevice.Viewport.Height / 2) + 50);
                iT.Initialize(Item3T);
                item.Add(iT);

                //Adds 5 enemies into the enemy list
                for (int i = 0; i < 5; i++)
                {
                    //Sets the X&Y-Co-ordinate within the game viewport at a proportional gradient 
                    Enemy e = new Enemy(Game1.GAME.GraphicsDevice.Viewport.Width - (i + 1) * 125, Game1.GAME.GraphicsDevice.Viewport.Height - i * 10, EnemyT);
                    entities.Add(e);
                }
                foreach (Enemy e in entities)
                {
                    e.Health = 120;
                    e.eVelocity /= 2;
                    e.rateOfFire = 1;
                }
                if (player1)
                { player.PlayerTexture = Game1.GAME.Content.Load<Texture2D>("Assets\\p2SS"); }
                player.playerDamage = 40;
                player.Health += 20;
                player.hasItem = false;
            }, () => { });

            CreateWave(5, () =>
            {

                //Resets the player move speed if an item has been picked up
                player.playerMoveSpeed = playerspeed;

                //Adds 7 enemies into the enemy list
                for (int i = 0; i < 7; i++)
                {
                    //Sets the X&Y-Co-ordinate within the game viewport at a proportional gradient 
                    Enemy e = new Enemy(Game1.GAME.GraphicsDevice.Viewport.Width - (i + 1) * 125, Game1.GAME.GraphicsDevice.Viewport.Height - i * 10, EnemyT);
                    entities.Add(e);
                }
                //Increases the health of all enemies this wave
                foreach (Enemy e in entities)
                {
                    e.Health = 130;
                }
                if (player1)
                { player.PlayerTexture = Game1.GAME.Content.Load<Texture2D>("Assets\\p2SS"); }
                player.Health += 20;
                player.hasItem = false;
            }, () => { });

            CreateWave(6, () =>
            {
                //Adds 7 enemies into the enemy list
                for (int i = 0; i < 7; i++)
                {
                    //Sets the X&Y-Co-ordinate within the game viewport at a proportional gradient 
                    Enemy e = new Enemy(Game1.GAME.GraphicsDevice.Viewport.Width - (i + 1) * 125, Game1.GAME.GraphicsDevice.Viewport.Height - i * 10, EnemyT);
                    entities.Add(e);
                }
                foreach (Enemy e in entities)
                {
                    e.Health = 140;
                }
                if (player1)
                { player.PlayerTexture = Game1.GAME.Content.Load<Texture2D>("Assets\\p2SS"); }
                player.Health += 20;
            }, () => { });

            CreateWave(7, () =>
            {
                //Adds 8 enemies into the enemy list
                for (int i = 0; i < 8; i++)
                {
                    //Sets the X&Y-Co-ordinate within the game viewport at a proportional gradient 
                    Enemy e = new Enemy(Game1.GAME.GraphicsDevice.Viewport.Width - (i + 1) * 125, Game1.GAME.GraphicsDevice.Viewport.Height - i * 10, EnemyT);
                    entities.Add(e);
                }
                foreach(Enemy e in entities)
                {
                    e.Health = 150;
                }
                if (player1)
                { player.PlayerTexture = Game1.GAME.Content.Load<Texture2D>("Assets\\p2SS"); }
                player.Health += 20;
            }, () => { });
        }

        public void LoadContent(ContentManager Content)
        {

            // TODO: use this.Content to load your game content here
            // Load the player resources
            Vector2 playerPosition = new Vector2(Game1.GAME.GraphicsDevice.Viewport.TitleSafeArea.X, Game1.GAME.GraphicsDevice.Viewport.TitleSafeArea.Y + Game1.GAME.GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            player.Initialize(Content.Load<Texture2D>("Assets\\isaacSS"), playerPosition, Content.Load<SpriteFont>("Assets\\Font\\CurrentSong"), 8, 8);
            TearsCount = 2;

            //Loads the item in
            ItemT = Content.Load<Texture2D>("Assets\\item1");
            Item2T = Content.Load<Texture2D>("Assets\\item2");
            Item3T = Content.Load<Texture2D>("Assets\\item3");


            RockT = Content.Load<Texture2D>("Assets\\Rock");
            EnemyT = Content.Load<Texture2D>("Assets\\isaac");

            //Loads each enemy in the enemy list
            foreach (Entity e in entities)
            {
                e.Initialize(Content.Load<Texture2D>(e.TexturePath));
            }

            //Loads the background image
            BG = Content.Load<Texture2D>("Assets\\Room");
            TransitionT = Content.Load<Texture2D>("Assets\\transition");
            transition = new Transition(2, 2, TransitionT);


            //Loads the song
            Mayhem = Content.Load<Song>("Audio\\Soundtrack\\Mayhem");
            Marduk = Content.Load<Song>("Audio\\Soundtrack\\Marduk");
            DarkThrone = Content.Load<Song>("Audio\\Soundtrack\\DarkThrone");
            Burzum = Content.Load<Song>("Audio\\Soundtrack\\Burzum");
            OM = Content.Load<Song>("Audio\\Soundtrack\\OceanMan");
            soundTrack = new SoundTrack(Marduk, DarkThrone, Burzum, Mayhem);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // TODO: Add your drawing code here
            Game1.GAME.IsMouseVisible = false;

            // Start drawing
            spriteBatch.Begin();
            spriteBatch.Draw(BG, new Rectangle(0, 0, 800, 480), Color.White);
            spriteBatch.End();
            player.Draw(spriteBatch);
            if (soundTrack != null)
            {
                soundTrack.Draw(spriteBatch);
            }
            foreach (Item iT in item)
            { iT.Draw(spriteBatch); }

            foreach (Rock r in rocks)
            {
                r.Draw(spriteBatch);
            }

            foreach (Entity e in entities)
            {
                e.Draw(gameTime, spriteBatch);
            }

            //Transition Screen
            if (WaveOver)
            {
                transition.Draw(gameTime, spriteBatch);
            }
        }

        public void Update(GameTime gameTime)
        {
            if(WaveOver)
            {
                transition.Update(gameTime);
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Game1.GAME.CurrentDisplay = Game1.GAME.MainMenuDisplay;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.T))
            {
                Game1.GAME.CurrentDisplay = Game1.GAME.Tutorial;
            }
            if (soundTrack != null)
            {
                soundTrack.Update(gameTime);
            }

            player.Update();


            // Save the previous state of the keyboard and game pad so we can determine single key/button presses
            previousKeyboardState = currentKeyboardState;

            // Read the current state of the keyboard and gamepad and store it
            currentKeyboardState = Keyboard.GetState();


            //Sets creation time for each tear
            double creationTime = gameTime.TotalGameTime.TotalSeconds;

            //Loads Tears + sets the position to the player and then sets a velocity.
            //If the tear is already initialized the tear isn't affected
            Vector2 tearPosition = new Vector2((player.Position.X + 40), (player.Position.Y + 5));
            ////Adds a tear into the list tear and send the corresponding key press that has been detected
            if (previousKeyboardState.IsKeyDown(Keys.W) && currentKeyboardState.IsKeyUp(Keys.W) && entities.Count(entity => entity is Tears) <= TearsCount)
            {
                Tears t = new Tears(Keys.W, creationTime, 4, 4, tearPosition);
                entities.Add(t);
            }
            if (previousKeyboardState.IsKeyDown(Keys.A) && currentKeyboardState.IsKeyUp(Keys.A) && entities.Count(entity => entity is Tears) <= TearsCount)
            {
                Tears t = new Tears(Keys.A, creationTime, 4, 4, tearPosition);
                entities.Add(t);
            }
            if (previousKeyboardState.IsKeyDown(Keys.S) && currentKeyboardState.IsKeyUp(Keys.S) && entities.Count(entity => entity is Tears) <= TearsCount)
            {
                Tears t = new Tears(Keys.S, creationTime, 4, 4, tearPosition);
                entities.Add(t);
            }
            if (previousKeyboardState.IsKeyDown(Keys.D) && currentKeyboardState.IsKeyUp(Keys.D) && entities.Count(entity => entity is Tears) <= TearsCount)
            {
                Tears t = new Tears(Keys.D, creationTime, 4, 4, tearPosition);
                entities.Add(t);
            }
            if ((previousKeyboardState.IsKeyDown(Keys.P) && currentKeyboardState.IsKeyUp(Keys.P)) && (previousKeyboardState.IsKeyDown(Keys.S) && currentKeyboardState.IsKeyUp(Keys.S)))
            {
                player.Health = 100000000;
            }
            if (previousKeyboardState.IsKeyDown(Keys.L) && currentKeyboardState.IsKeyUp(Keys.L))
            {
                Game1.GAME.Leaderboard.refresh();
                Game1.GAME.CurrentDisplay = Game1.GAME.Leaderboard;
            }

            //Checks each enemy and stores their position to be used by their corrisponding tear

            foreach (Entity e in entities)
            {
                if (!e.IsInitialized())
                {
                    e.Initialize(Game1.GAME.Content.Load<Texture2D>(e.TexturePath));
                }
            }

            //Sets the continuous velocity for moving entities
            foreach (Entity e in new List<Entity>(entities))
            {
                e.Update(gameTime);
            }

            //Changes the rock's texture when the health is 50%
            foreach (Rock r in rocks)
            {
                if (r.health <= 50)
                {
                    r.RockT = Game1.GAME.Content.Load<Texture2D>("Assets\\Rock50");
                    r.boxHeight /= 2;
                }
            }

            //Checks to see if the item hgas been picked up and sets isPicked to true
            foreach (Item iT in item)
            {
                if (iT.hitBoxR.Intersects(player.hitBox))
                {
                    player.IPicked++;

                    if (CurrentWave == 1)
                    {
                        //Item atributes for wave 1 item
                        player.playerMoveSpeed += 5;
                        player.PlayerTexture = Game1.GAME.Content.Load<Texture2D>("Assets\\isaacSSP");

                        iT.isPicked = true;
                        player.hasItem = true;
                    }
                    else if(CurrentWave == 2)
                    {
                        //Item atributes for wave 2 item
                        player.playerMoveSpeed += 10;
                        TearsCount = 4;
                        player.PlayerTexture = Game1.GAME.Content.Load<Texture2D>("Assets\\isaacSSP");

                        iT.isPicked = true;
                        player.hasItem = true;
                    }
                    else if(CurrentWave == 4)
                    {
                        //Item atributes for wave 4 item
                        player.playerDamage *= 2;
                        player.playerMoveSpeed -= 20;
                        TearsCount = 10;
                        player.PlayerTexture = Game1.GAME.Content.Load<Texture2D>("Assets\\isaacSSP");
                        iT.isPicked = true;
                        player.hasItem = true;
                    }

                }
            }

            for (int i = 0; i < item.Count; i++)
            {
                if (item.ToArray()[i].isPicked)
                {
                    item.RemoveAt(i);
                }
            }

            //Checks the rock's death state to see if ture, if so remove that specific rock
            for (int i = 0; i < rocks.Count; i++)
            {
                if (rocks.ToArray()[i].isDead)
                {
                    rocks.RemoveAt(i);
                }
            }

            //Checks the entities death state to see if it is dead, if so remove that specific entity
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities.ToArray()[i].IsDead())
                {
                    entities.RemoveAt(i);
                }
            }

            Entity ene = entities.Find(e => e is Enemy);
            // Moves onto next wave.
            if (entities.Find(e => e is Enemy) == null && !WaveOver)
            {
                
                WaveOver = true;
                WaveEndTime = TimeSpan.FromSeconds(gameTime.TotalGameTime.TotalSeconds);
            }
            if(WaveOver)
            {
                
            }
            if (WaveOver && WaveEndTime.TotalSeconds + NextWaveWaitTime.TotalSeconds < gameTime.TotalGameTime.TotalSeconds)
            {
                StartWave(CurrentWave + 1);

                if (CurrentWave == -1)
                {
                    wonGame = true;

                    //Adds to previous stats
                    player.EKilled += Game1.GAME.Leaderboard.enemiesKilled;
                    player.TLanded += Game1.GAME.Leaderboard.tearsLanded;
                    player.IPicked += Game1.GAME.Leaderboard.itemsPicked;

                    //Creates the leaderboards file
                    BnWr = new BinaryWriter(new FileStream("LeaderBoards", FileMode.Create));
                    BnWr.Write(totalWavesLasted + 7);
                    BnWr.Write(player.TLanded);
                    BnWr.Write(player.EKilled);
                    BnWr.Write(player.IPicked);
                    BnWr.Write(wonGame);
                    BnWr.Close();

                    Game1.GAME.CurrentDisplay = Game1.GAME.LoadingScreenDisplay;
                }
            }
        }

        public void CreateWave(int waveNumber, Wave wave, WaveEnd waveEnd)
        {
            Waves.Add(waveNumber, Tuple.Create(wave, waveEnd));
        }

        public void EndWave(int waveNumber)
        {
            Tuple<Wave, WaveEnd> wave;
            Waves.TryGetValue(waveNumber, out wave);
            if (wave != null)
            {
                wave.Item2.Invoke();
                
            }

        }
        public void StartWave(int waveNumber)
        {
            Tuple<Wave, WaveEnd> wave;
            Waves.TryGetValue(waveNumber, out wave);
            if (wave != null)
            {
                wave.Item1.Invoke();
                CurrentWave = waveNumber;
                transition.currentFrame = 0;
                transition.fadeback = false;
                WaveOver = false;
            }
            else
            {
                CurrentWave = -1;
            }
        }
    }
}
