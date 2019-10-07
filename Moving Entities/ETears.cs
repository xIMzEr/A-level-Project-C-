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
    class ETears : Entity
    {
        //Tears texture
        public Texture2D tearET;

        //Tears Velocity
        public Vector2 eTVelocity = new Vector2(-1,-3);

        //Tears Origin
        Vector2 origin = new Vector2(0,0);

        //Tears Current Position
        public Vector2 position = new Vector2(0, 0);

        public bool inititalised = false;

        //Creates a retangle around each tear
        public Rectangle hitBox{ get {
                return new Rectangle((int)position.X, (int)position.Y, 50, 50);}
        }


        //5 second timer
        public double creationTime;
        public double lifeTime = 2;

        //Visibility
        public bool isDead;

        //Tear animation varaibles
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;

        public ETears(double creationTime, Vector2 Position, int rows, int columns)
        {
            //Reverses the direction of the projectiles if within waves 3-5
            if(Game1.GAME.GameDisplay.CurrentWave >= 3 && Game1.GAME.GameDisplay.CurrentWave <= 5)
            { eTVelocity = new Vector2(1, 2); }
            //Default projectile velocity
            else
            { eTVelocity = new Vector2(-1, -2); }
                this.creationTime = creationTime;
                this.position = Position;
            //Sets local variables to parameters
                Rows = rows;
                Columns = columns;
                currentFrame = 0;
                totalFrames = Rows * Columns;
        }

        public void Initialize(Texture2D texture)
        {
            //Initial states of the enemy tears
            tearET = texture;
            isDead = false;
            inititalised = true;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();
            //Animation
            //Calculates the area of each sprite in the spritesheet & how many sprites there are
            int width = tearET.Height / Columns;
            int height = tearET.Width / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            //Selects which sprite is shown each second in gametime
            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);

            //Draws the sprite
            spriteBatch.Draw(tearET, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }


        public string TexturePath
        {
            //Gets the enemy tears spritesheet
            get { return "Assets\\EnemyTSS"; }
        }

        public void Update(GameTime gameTime)
        {

            position -= eTVelocity;

            //Frame Cycle by adding one to the current frame
            currentFrame++;
            if (currentFrame == totalFrames)
            {
                currentFrame = 0;
            }

            //When a player's tear hits the boundaries its velocity is reversed
            if (position.X >= Game1.GAME.GraphicsDevice.Viewport.Width - 40)
            {
                eTVelocity *= -1;
            }
            if (position.Y >= Game1.GAME.GraphicsDevice.Viewport.Height - 60)
            {
                eTVelocity.Y *= -1;
            }
            if (position.X <= Game1.GAME.GraphicsDevice.Viewport.X)
            {
                eTVelocity.X *= -1;
            }
            if (position.Y <= Game1.GAME.GraphicsDevice.Viewport.Y)
            {
                eTVelocity.Y *= -1;
            }

            //Checks to see if any of the enemy tears hit the player if so deduct health from the player
            if (hitBox.Intersects(Game1.GAME.GameDisplay.player.hitBox))
            {
                Game1.GAME.GameDisplay.player.Health -= 40;
                isDead = true;
            }

            if (gameTime.TotalGameTime.TotalSeconds > creationTime + lifeTime)
            { isDead = true; }
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
