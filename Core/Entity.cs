using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Core
{
    public interface Entity
    {
        string TexturePath { get; }

        bool IsInitialized();

        bool IsDead();

        void Initialize(Texture2D Texture);

        void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        void Update(GameTime gameTime);
    }
}
