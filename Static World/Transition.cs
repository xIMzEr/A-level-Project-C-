using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Static_World
{
    public class Transition
    {
        //Tears texture
        public Texture2D TransitionT;

        //Tears Origin
        Vector2 origin = new Vector2(0, 0);

        //Tears Current Position
        public Vector2 position = new Vector2(0, 0);


        //Tear animation varaibles
        public int Rows;
        public int Columns;
        public int currentFrame;
        private int totalFrames;
        public bool fadeback = false;

        //5 second timer
        public double lastUpdateTime;
        public double updateTime = 1;


        public Transition(int rows, int columns, Texture2D texture)
        {
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            TransitionT = texture;
        }


        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalSeconds > updateTime + lastUpdateTime)
            {
                
                if (currentFrame == totalFrames)
                {
                    fadeback = true;
                    Game1.GAME.GameDisplay.EndWave(Game1.GAME.GameDisplay.CurrentWave);
                }                    
                if (fadeback)
                {
                        currentFrame--;
                }
                else
                {
                    currentFrame++;
                }
                lastUpdateTime = gameTime.TotalGameTime.TotalSeconds;
            }

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            //spriteBatch.Draw(tearT, destinationRectangle: new Rectangle((int)position.X, (int)position.Y, 50, 50), sourceRectangle: null, color: Color.White, rotation: 1f, origin: Vector2.Zero, effects: SpriteEffects.None, layerDepth: 1f);

            int width = TransitionT.Width / Columns;
            int height = TransitionT.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle(0, 0, width, height);


            spriteBatch.Draw(TransitionT, destinationRectangle, sourceRectangle, Color.White);

            spriteBatch.End();
        }

    }
}
