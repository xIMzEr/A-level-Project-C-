using Game1.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Tears : Entity
    {
        //Tears texture
        public Texture2D tearT;

        //Tears Velocity
        public Vector2 tvelocity;

        //Tears Origin
        Vector2 origin = new Vector2(0, 0);

        //Tears Current Position
        public Vector2 position = new Vector2(0, 0);

        public bool inititalised = false;

        //Sets tear death state
        public bool isDead = false;

        //2 second timer
        public double creationTime;
        public double lifeTime = 2;

        //Tear animation varaibles
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;



        //Creates a retangle around each tear
        public Rectangle hitBox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, 50, 50);
            }
        }

        public Tears(Keys direction, double creationTime, int rows, int columns, Vector2 Position)
        {
            if (direction == Keys.W)
            {
                tvelocity = new Vector2(1, 5);
            }
            if (direction == Keys.A)
            {
                tvelocity = new Vector2(5, 1);
            }
            if (direction == Keys.S)
            {
                tvelocity = new Vector2(-1, -5);
            }
            if (direction == Keys.D)
            {
                tvelocity = new Vector2(-5, -1);
            }

            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            this.creationTime = creationTime;
            this.position = Position;
        }

        public void Initialize(Texture2D texture)
        {
            tearT = texture;
            isDead = false;
            inititalised = true;
        }

        public void Update(GameTime gameTime)
        {
            position -= tvelocity;

            //When a player's tear hits the boundaries its velocity is reversed
            if (position.X >= Game1.GAME.GraphicsDevice.Viewport.Width - 40)
            {
                tvelocity.X *= -1;
            }
            if (position.Y >= Game1.GAME.GraphicsDevice.Viewport.Height - 60)
            {
                tvelocity.Y *= -1;
            }
            if (position.X <= Game1.GAME.GraphicsDevice.Viewport.X)
            {
                tvelocity.X *= -1;
            }
            if (position.Y <= Game1.GAME.GraphicsDevice.Viewport.Y)
            {
                tvelocity.Y *= -1;
            }

            //Adds one to the currentFrame 
            currentFrame++;
            if (currentFrame == totalFrames)
            {
                currentFrame = 0;
            }

            foreach (Rock r in Game1.GAME.GameDisplay.rocks)
            {

                //If the rock's health is less than or equal to zero the rock is concidered dead
                if (r.health <= 0)
                {
                    r.isDead = true;
                }

                if (hitBox.Intersects(r.hitBoxR))
                {
                    //If the tear hits the rock then the velocity is reversed
                    tvelocity *= -1;

                    if (Game1.GAME.GameDisplay.player.hasItem)
                    {
                        r.health -= 50;
                        isDead = true;
                    }
                }

            }

            if (gameTime.TotalGameTime.TotalSeconds > creationTime + lifeTime)
            { isDead = true; }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
           

            int width = tearT.Height / Columns;
            int height = tearT.Width / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);

            
            spriteBatch.Draw(tearT, destinationRectangle, sourceRectangle, Color.White);

            spriteBatch.End();
        }


        public string TexturePath
        {
            get { return "Assets\\TearsSS"; }
        }

        public bool IsInitialized()
        {
            return inititalised;
        }


        public bool IsDead()
        {
            return isDead;
        }
    }
}
