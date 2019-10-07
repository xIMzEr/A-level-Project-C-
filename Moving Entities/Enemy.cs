using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Game1.Core;

namespace Game1
{
    public class Enemy : Entity
    {
        public bool initialized = false;
        // Animation representing the player
        public Texture2D EnemyTexture;

        // Position of the Player relative to the upper left side of the screen
        //public Random rN = new Random(50);
        public Vector2 Position = new Vector2(0,0);
        public int boxHeight = 35;
        public int boxWidth = 35;
        public int height = 40;
        public int width = 40;
        // State of the player
        public bool Active;

        // Amount of hit points that player has
        public int Health;

        //Enemy Velocity
        public Vector2 eVelocity = new Vector2(-5, -1);

        //Enemy deeath state
        public bool death = false;

        public double lastSpawn = 0;
        public double rateOfFire = 1;

        //Creates a hitbox
        public Rectangle hitBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, boxWidth, boxHeight);
            }
        }



        public Enemy(int x, int y, Texture2D texture)
        {
            Position.X = x;
            Position.Y = y;

            EnemyTexture = texture;

            // Set the player to be active
            Active = true;

            // Set the player health
            Health = 100;

            //Identifies that the enemy is alive
            death = false;

            initialized = true;
        }

        public void Update(GameTime gameTime)
        {
            Position -= eVelocity;

            if (Position.X >= Game1.GAME.GraphicsDevice.Viewport.Width - 40)
            {
                eVelocity.X *= -1;
            }
            if (Position.Y >= Game1.GAME.GraphicsDevice.Viewport.Height - 60)
            {
                eVelocity.Y *= -1;
            }
            if (Position.X <= Game1.GAME.GraphicsDevice.Viewport.X)
            {
                eVelocity.X *= -1;
            }
            if (Position.Y <= Game1.GAME.GraphicsDevice.Viewport.Y)
            {
                eVelocity.Y *= -1;
            }
            
            //Checks enemy to see if they interset with any tears
            foreach (Entity e in Game1.GAME.GameDisplay.entities)
            {
                if (e is Tears) {
                    if (hitBox.Intersects((e as Tears).hitBox))
                    {
                        Game1.GAME.GameDisplay.player.TLanded++;
                        Health -= Game1.GAME.GameDisplay.player.playerDamage;
                        (e as Tears).isDead = true; 
                    }
                }

            }

            if (Health <= 0)
            {
                Game1.GAME.GameDisplay.player.EKilled++;
                death = true;
            }

            //Checks to see when the last time an enemy tear has spawned added wtih the rate of fire is less
            //than the total game time elapsed in seconds
            if (lastSpawn + rateOfFire < gameTime.TotalGameTime.TotalSeconds)
            {
                //If so create a new tear into a list & update the time a tear has spawned
                ETears eT = new ETears(gameTime.TotalGameTime.TotalSeconds, new Vector2(this.Position.X, this.Position.Y), 2,  2);

                lastSpawn = gameTime.TotalGameTime.TotalSeconds;

                if (!eT.inititalised)
                {
                    eT.Initialize(Game1.GAME.Content.Load<Texture2D>(eT.TexturePath));

                }
                Game1.GAME.GameDisplay.entities.Add(eT);
            }

            // Make sure that the enemy does not go out of bounds
            Position.X = MathHelper.Clamp(Position.X, 0, Game1.GAME.GraphicsDevice.Viewport.Width - 40);
            Position.Y = MathHelper.Clamp(Position.Y, 0, Game1.GAME.GraphicsDevice.Viewport.Height - 60);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(EnemyTexture, destinationRectangle: new Rectangle((int)Position.X, (int)Position.Y, width, height), sourceRectangle: null, color: Color.White, rotation: 0f, origin: Vector2.Zero, effects: SpriteEffects.None, layerDepth: 0f);
            spriteBatch.End();
        }


        public string TexturePath
        {
            get { return "Assets\\isaac"; }
        }


        public bool IsInitialized()
        {
            return initialized;
        }


        public bool IsDead()
        {
            return death;
        }


        public void Initialize(Texture2D Texture)
        {
            
        }
    }

    public class CopyOfEnemy : Entity
    {
        public bool initialized = false;
        // Animation representing the player
        public Texture2D EnemyTexture;

        // Position of the Player relative to the upper left side of the screen
        //public Random rN = new Random(50);
        public Vector2 Position = new Vector2(0, 0);
        public int boxHeight = 35;
        public int boxWidth = 35;
        public int height = 40;
        public int width = 40;
        // State of the player
        public bool Active;

        // Amount of hit points that player has
        public int Health;

        //CopyOfEnemy Velocity
        public Vector2 eVelocity = new Vector2(-5, -1);

        //CopyOfEnemy deeath state
        public bool death = false;

        public double lastSpawn = 0;
        public double rateOfFire = 1;

        //Creates a hitbox
        public Rectangle hitBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, boxWidth, boxHeight);
            }
        }



        public CopyOfEnemy(int x, int y, Texture2D texture)
        {
            Position.X = x;
            Position.Y = y;

            EnemyTexture = texture;

            // Set the player to be active
            Active = true;

            // Set the player health
            Health = 100;

            //Identifies that the enemy is alive
            death = false;

            initialized = true;
        }

        public void Update(GameTime gameTime)
        {
            Position -= eVelocity;

            if (Position.X >= Game1.GAME.GraphicsDevice.Viewport.Width - 40)
            {
                eVelocity.X *= -1;
            }
            if (Position.Y >= Game1.GAME.GraphicsDevice.Viewport.Height - 60)
            {
                eVelocity.Y *= -1;
            }
            if (Position.X <= Game1.GAME.GraphicsDevice.Viewport.X)
            {
                eVelocity.X *= -1;
            }
            if (Position.Y <= Game1.GAME.GraphicsDevice.Viewport.Y)
            {
                eVelocity.Y *= -1;
            }

            //Checks enemy to see if they interset with any tears
            foreach (Entity e in Game1.GAME.GameDisplay.entities)
            {
                if (e is Tears)
                {
                    if (hitBox.Intersects((e as Tears).hitBox))
                    {
                        Game1.GAME.GameDisplay.player.TLanded++;
                        Health -= Game1.GAME.GameDisplay.player.playerDamage;
                        (e as Tears).isDead = true;
                    }
                }

            }

            if (Health <= 0)
            {
                Game1.GAME.GameDisplay.player.EKilled++;
                death = true;
            }

            //Checks to see when the last time an enemy tear has spawned added wtih the rate of fire is less
            //than the total game time elapsed in seconds
            if (lastSpawn + rateOfFire < gameTime.TotalGameTime.TotalSeconds)
            {
                //If so create a new tear into a list & update the time a tear has spawned
                ETears eT = new ETears(gameTime.TotalGameTime.TotalSeconds, new Vector2(this.Position.X, this.Position.Y), 2, 2);

                lastSpawn = gameTime.TotalGameTime.TotalSeconds;

                if (!eT.inititalised)
                {
                    eT.Initialize(Game1.GAME.Content.Load<Texture2D>(eT.TexturePath));

                }
                Game1.GAME.GameDisplay.entities.Add(eT);
            }

            // Make sure that the enemy does not go out of bounds
            Position.X = MathHelper.Clamp(Position.X, 0, Game1.GAME.GraphicsDevice.Viewport.Width - 40);
            Position.Y = MathHelper.Clamp(Position.Y, 0, Game1.GAME.GraphicsDevice.Viewport.Height - 60);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(EnemyTexture, destinationRectangle: new Rectangle((int)Position.X, (int)Position.Y, width, height), sourceRectangle: null, color: Color.White, rotation: 0f, origin: Vector2.Zero, effects: SpriteEffects.None, layerDepth: 0f);
            spriteBatch.End();
        }


        public string TexturePath
        {
            get { return "Assets\\isaac"; }
        }


        public bool IsInitialized()
        {
            return initialized;
        }


        public bool IsDead()
        {
            return death;
        }


        public void Initialize(Texture2D Texture)
        {

        }
    }

}
