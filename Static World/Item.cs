using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Item
    {
        // Texture of the Item
        public Texture2D itemT;

        // Position of the Item relative to the upper left side of the screen
        public Vector2 Position;

        //Item W&H
        public int itemWidth = 40;
        public int itemHeight = 40;

        //Item hitbox
        public Rectangle hitBoxR;


        //Item state
        public bool isPicked = false;

        public Item(int x, int y)
        {
            Position.X = x;
            Position.Y = y;
        }

        public void Initialize(Texture2D texture)
        {
            itemT = texture;
            hitBoxR = new Rectangle((int)Position.X, (int)Position.Y, itemWidth, itemHeight);
        }

        public void LoadContent()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(itemT, destinationRectangle: new Rectangle((int)Position.X, (int)Position.Y, itemWidth, itemHeight), color: Color.White, rotation: 0f, origin: Vector2.Zero, effects: SpriteEffects.None, layerDepth: 0f);
            spriteBatch.End();
        }
    }
}
