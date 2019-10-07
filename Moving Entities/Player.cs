using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace Game1
{
    public class Player
    {
        // Animation representing the player
        public Texture2D PlayerTexture;

        // Position of the Player relative to the upper left side of the screen
        public Vector2 Position = new Vector2(0, 0);

        // State of the player
        public bool Active;

        //Leaderboard stats
        public int EKilled;
        public int TLanded;
        public int IPicked;

        // Amount of hit points that player has
        public int Health;

        //Scale of the character
        Vector2 scale;

        //Sets the state of the player Dead or Alive
        public bool death;

        //Text for player health
        public SpriteFont HP;

        //States if the player has an item
        public bool hasItem = false;
        public bool hasItem2 = false;

        // A movement speed for the player
        public float playerMoveSpeed = 6.0f;

        ///Damage value for the player's tears
        public int playerDamage = 20;

        //BinaryWriter
        BinaryWriter BnWr;

        //Item Value
        public string destroyR = "False";
        bool damageX = false;

        //Creates a hitbox
        public Rectangle hitBox { get {
            return new Rectangle((int)Position.X, (int)Position.Y, 50, 50);
        } }

        //Target of player
        float targetX = 128;
        float targetY;

        //Tear animation varaibles
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;

        // Get the width of the player
        public int Width   
        { get { return PlayerTexture.Width; } }

        // Get the height of the player
        public int Height
        { get { return PlayerTexture.Height; } }

        public void Initialize(Texture2D texture, Vector2 position, SpriteFont font, int rows, int columns)
        {
            PlayerTexture = texture;
            HP = font;
            // Set the starting position of the player around the middle of the screen and to the back
            Position = position;

            // Set the player to be active
            Active = true;

            // Set the player health
            Health = 100;

            EKilled += Game1.GAME.Leaderboard.enemiesKilled;
            TLanded += Game1.GAME.Leaderboard.tearsLanded;
            IPicked += Game1.GAME.Leaderboard.itemsPicked;
            Game1.GAME.GameDisplay.totalWavesLasted += Game1.GAME.Leaderboard.wavesLasted;

            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
        }

        public void Update()
        {
            currentFrame ++;
            if (currentFrame == totalFrames)
            {
                currentFrame = 0;
            }

            // Use the Keyboard / Dpad
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Position.X -= playerMoveSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Position.X += playerMoveSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                Position.Y -= playerMoveSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                Position.Y += playerMoveSpeed;
            }

            //Checks through each rock every frame and the player position
            foreach (Rock rock in Game1.GAME.GameDisplay.rocks)
            {
                //Checks to see if the hitbox of the player intersets any of the rocks
                if (hitBox.Intersects(rock.hitBoxR))
                {
                    // Right side collision checks
                    if (hitBox.Right > rock.hitBoxR.Left && Position.X < rock.Position.X)
                    {
                        Position.X -= playerMoveSpeed;
                    }

                    // Bottom side collision checks
                    if (hitBox.Bottom > rock.hitBoxR.Top && Position.Y < rock.Position.Y)
                    {
                        Position.Y -= playerMoveSpeed;
                    }

                    // Left side collision checks
                    if (hitBox.Left < rock.hitBoxR.Right && Position.X > rock.Position.X)
                    {
                        Position.X += playerMoveSpeed;
                    }

                    if (hitBox.Top < rock.hitBoxR.Bottom && Position.Y > rock.Position.Y)
                    {
                        Position.Y += playerMoveSpeed;
                    }
                   }
            }

            // Make sure that the player does not go out of bounds
            Position.X = MathHelper.Clamp(Position.X, 0, Game1.GAME.GraphicsDevice.Viewport.Width - 40);
            Position.Y = MathHelper.Clamp(Position.Y, 0, Game1.GAME.GraphicsDevice.Viewport.Height - 60);

            if(hasItem2)
            {
                playerMoveSpeed += 3;
                playerDamage += 15;
            }
            else if(hasItem)
            {
                if(!damageX)
                {
                    playerDamage *= 2;
                    damageX = true;
                }
                destroyR = "True";
            }

            //Exits the game when the player health is equal to 0
            if (Health <= 0)
            {           
                //Adds to previous game
                EKilled += Game1.GAME.Leaderboard.enemiesKilled;
                TLanded += Game1.GAME.Leaderboard.tearsLanded;
                IPicked += Game1.GAME.Leaderboard.itemsPicked;
                Game1.GAME.GameDisplay.totalWavesLasted += Game1.GAME.Leaderboard.wavesLasted;
                //Creates the leaderboards file
                BnWr = new BinaryWriter(new FileStream("LeaderBoards", FileMode.Create));
                BnWr.Write(Game1.GAME.GameDisplay.totalWavesLasted + Game1.GAME.GameDisplay.CurrentWave);
                BnWr.Write(TLanded);
                BnWr.Write(EKilled);
                BnWr.Write(IPicked);
                BnWr.Write(Game1.GAME.GameDisplay.wonGame);
                BnWr.Close();
                Game1.GAME.Exit();
            }
        }

        public void LoadContent()
        {
            scale = new Vector2(targetX / (float)PlayerTexture.Width, targetY / (float)PlayerTexture.Width);
            targetY = PlayerTexture.Height * scale.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            //spriteBatch.Draw(PlayerTexture, destinationRectangle: new Rectangle((int)Position.X, (int)Position.Y, 50, 50), sourceRectangle: null, color: Color.White, rotation: 0f, origin: Vector2.Zero, effects: SpriteEffects.None, layerDepth: 0f);

            int width = PlayerTexture.Height / Columns;
            int height = PlayerTexture.Width / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, width, height);


            spriteBatch.Draw(PlayerTexture, destinationRectangle, sourceRectangle, Color.White);


            spriteBatch.DrawString(HP, "Health: " + Health, new Vector2(40, 40), Color.White);
            spriteBatch.DrawString(HP, "Damage: " + playerDamage, new Vector2(40, 80), Color.White);
            spriteBatch.DrawString(HP, "Levolution: " + destroyR, new Vector2(40, 120), Color.White);
            spriteBatch.DrawString(HP, "Wave: " + Game1.GAME.GameDisplay.CurrentWave, new Vector2(350, 450), Color.Red);

            spriteBatch.End();
        }

    }

}
