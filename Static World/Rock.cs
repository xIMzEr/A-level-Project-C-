using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Rock
    {
        // Texture of the rock
        public Texture2D RockT;

        // Position of the rock relative to the upper left side of the screen
        public Vector2 Position;

        //Rock W&H
        public int rockWidth = 40;
        public int rockHeight = 40;
        public int boxWidth;
        public int boxHeight;
        //Rock hitbox
        public Rectangle hitBoxR;

        //Rock health
        public int health = 100;

        //Rock death state
        public bool isDead = false;

        public Rock(int x, int y, Texture2D texture)
        {
            Position.X = x;
            Position.Y = y;

            boxWidth = rockWidth - 10;
            boxHeight = rockHeight - 10;
            RockT = texture;
            hitBoxR = new Rectangle((int)Position.X, (int)Position.Y, boxWidth, boxHeight);
        }

        public void LoadContent()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(RockT, destinationRectangle: new Rectangle((int)Position.X, (int)Position.Y, rockWidth, rockHeight), color: Color.White, rotation: 0f, origin: Vector2.Zero, effects: SpriteEffects.None, layerDepth: 0f);
            spriteBatch.End();
        }
    }
}
